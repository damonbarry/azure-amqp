﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.Amqp
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// This API supports the Azure infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public abstract class Singleton<TValue> : IDisposable where TValue : class
    {
        readonly object syncLock;

        TaskCompletionSource<TValue> taskCompletionSource;
        volatile bool disposed;

        public Singleton()
        {
            this.syncLock = new object();
        }

        protected TaskCompletionSource<TValue> TaskCompletionSource
        {
            get
            {
                return this.taskCompletionSource;
            }
        }

        // Test verification only
        internal TValue Value
        {
            get
            {
                var thisTaskCompletionSource = this.taskCompletionSource;
                return thisTaskCompletionSource != null && thisTaskCompletionSource.Task.Status == TaskStatus.RanToCompletion ? thisTaskCompletionSource.Task.Result : null;
            }
        }

        public Task OpenAsync(TimeSpan timeout)
        {
            return this.GetOrCreateAsync(timeout);
        }

        public Task CloseAsync()
        {
            this.Dispose();

            return TaskHelpers.CompletedTask;
        }

        public void Close()
        {
            this.Dispose();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.disposed = true;
                var thisTaskCompletionSource = this.taskCompletionSource;
                if (thisTaskCompletionSource != null && thisTaskCompletionSource.Task.Status == TaskStatus.RanToCompletion)
                {
                    OnSafeClose(thisTaskCompletionSource.Task.Result);
                }
            }
        }

        public async Task<TValue> GetOrCreateAsync(TimeSpan timeout)
        {
            var timeoutHelper = new TimeoutHelper(timeout);

            while (!this.disposed && timeoutHelper.RemainingTime() > TimeSpan.Zero)
            {
                TaskCompletionSource<TValue> tcs;

                if (this.TryGet(out tcs))
                {
                    TValue current = await tcs.Task.ConfigureAwait(false);
                    if (this.IsValid(current))
                    {
                        return current;
                    }

                    this.Invalidate(current);
                }

                tcs = new TaskCompletionSource<TValue>();
                if (this.TrySet(tcs))
                {
                    try
                    {
                        TValue value = await this.OnCreateAsync(timeout).ConfigureAwait(false);
                        tcs.SetResult(value);
                    }
                    catch (Exception ex) when (!Fx.IsFatal(ex))
                    {
                        this.TryRemove();
                        tcs.SetException(ex);
                    }

                    return await tcs.Task;
                }
            }

            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            throw new TimeoutException(string.Format(CultureInfo.InvariantCulture, "Creation of {0} did not complete in {1} milliseconds.", typeof(TValue).Name, timeout.TotalMilliseconds));
        }

        protected void Invalidate(TValue instance)
        {
            lock (this.syncLock)
            {
                if (this.taskCompletionSource != null &&
                    this.taskCompletionSource.Task.Status == TaskStatus.RanToCompletion &&
                    this.taskCompletionSource.Task.Result == instance)
                {
                    Volatile.Write<TaskCompletionSource<TValue>>(ref this.taskCompletionSource, null);
                }
            }
        }

        protected virtual bool IsValid(TValue value)
        {
            return true;
        }

        protected abstract Task<TValue> OnCreateAsync(TimeSpan timeout);

        protected abstract void OnSafeClose(TValue value);

        bool TryGet(out TaskCompletionSource<TValue> tcs)
        {
            tcs = Volatile.Read<TaskCompletionSource<TValue>>(ref this.taskCompletionSource);
            return tcs != null;
        }

        bool TrySet(TaskCompletionSource<TValue> tcs)
        {
            lock (this.syncLock)
            {
                if (this.taskCompletionSource == null)
                {
                    Volatile.Write<TaskCompletionSource<TValue>>(ref this.taskCompletionSource, tcs);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        bool TryRemove()
        {
            lock (this.syncLock)
            {
                if (this.taskCompletionSource != null)
                {
                    Volatile.Write<TaskCompletionSource<TValue>>(ref this.taskCompletionSource, null);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
