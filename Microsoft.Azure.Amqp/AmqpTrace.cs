// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.Amqp
{
    using System;

    class AmqpTrace
    {
        // Replace this with your own implementation to enable tracing.
        public static AmqpTrace Provider = new AmqpTrace();

        protected AmqpTrace()
        {
        }

        string ObjToStr(object obj) => obj?.ToString() ?? "<null>";

        public virtual void AmqpOpenConnection(object source, object connection)
        {
            Console.WriteLine($">>> AmqpOpenConnection=time: {DateTime.Now}, source: {ObjToStr(source)}, connection: {ObjToStr(connection)}");
        }

        public virtual void AmqpCloseConnection(object source, object connection, bool abort)
        {
            Console.WriteLine($">>> AmqpCloseConnection=time: {DateTime.Now}, source: {ObjToStr(source)}, connection: {ObjToStr(connection)}, abort: {abort}");
        }

        public virtual void AmqpAddSession(object source, object session, ushort localChannel, ushort remoteChannel)
        {
            Console.WriteLine($">>> AmqpAddSession=time: {DateTime.Now}, source: {ObjToStr(source)}, session: {ObjToStr(session)}, localChannel: {localChannel}, remoteChannel: {remoteChannel}");
        }

        public virtual void AmqpAttachLink(object connection, object session, object link, uint localHandle, uint remoteHandle, string linkName, string role, object source, object target)
        {
            Console.WriteLine($">>> AmqpAttachLink=time: {DateTime.Now}, connection: {ObjToStr(connection)}, session: {ObjToStr(session)}, link: {ObjToStr(link)}, localHandle: {localHandle}, remoteHandle: {remoteHandle}, linkName: {linkName}, role: {role}, source: {ObjToStr(source)}, target: {ObjToStr(target)}");
        }

        public virtual void AmqpDeliveryNotFound(object source, string deliveryTag)
        {
            Console.WriteLine($">>> AmqpDeliveryNotFound=time: {DateTime.Now}, source: {ObjToStr(source)}, deliveryTag: {deliveryTag}");
        }

        public virtual void AmqpDispose(object source, uint deliveryId, bool settled, object state)
        {
            Console.WriteLine($">>> AmqpDispose=time: {DateTime.Now}, source: {ObjToStr(source)}, deliveryId: {deliveryId}, settled: {settled}, state: {ObjToStr(state)}");
        }

        public virtual void AmqpDynamicBufferSizeChange(object source, string type, int oldSize, int newSize)
        {
            Console.WriteLine($">>> AmqpDynamicBufferSizeChange=time: {DateTime.Now}, source: {ObjToStr(source)}, type: {type}, oldSize: {oldSize}, newSize: {newSize}");
        }

        public virtual void AmqpInsecureTransport(object source, object transport, bool isSecure, bool isAuthenticated)
        {
            Console.WriteLine($">>> AmqpInsecureTransport=time: {DateTime.Now}, source: {ObjToStr(source)}, transport: {ObjToStr(transport)}, isSecure: {isSecure}, isAuthenticated: {isAuthenticated}");
        }

        public virtual void AmqpLinkDetach(object source, string name, uint handle, string action, string error)
        {
            Console.WriteLine($">>> AmqpLinkDetach=time: {DateTime.Now}, source: {ObjToStr(source)}, name: {name}, handle: {handle}, action: {action}, error: {error}");
        }

        public virtual void AmqpListenSocketAcceptError(object source, bool willRetry, string error)
        {
            Console.WriteLine($">>> AmqpListenSocketAcceptError=time: {DateTime.Now}, source: {ObjToStr(source)}, willRetry: {willRetry}, error: {error}");
        }

        public virtual void AmqpLogError(object source, string operation, string message)
        {
            Console.WriteLine($">>> AmqpLogError=time: {DateTime.Now}, source: {ObjToStr(source)}, operation: {operation}, message: {message}");
        }

        public virtual void AmqpLogOperationInformational(object source, TraceOperation operation, object detail)
        {
            Console.WriteLine($">>> AmqpLogOperationInformational=time: {DateTime.Now}, source: {ObjToStr(source)}, operation: {operation}, detail: {ObjToStr(detail)}");
        }

        public virtual void AmqpLogOperationVerbose(object source, TraceOperation operation, object detail)
        {
            Console.WriteLine($">>> AmqpLogOperationVerbose=time: {DateTime.Now}, source: {ObjToStr(source)}, operation: {operation}, detail: {ObjToStr(detail)}");
        }

        public virtual void AmqpMissingHandle(object source, string type, uint handle)
        {
            Console.WriteLine($">>> AmqpMissingHandle=time: {DateTime.Now}, source: {ObjToStr(source)}, type: {type}, handle: {handle}");
        }

        public virtual void AmqpOpenEntityFailed(object source, object obj, string name, string entityName, string error)
        {
            Console.WriteLine($">>> AmqpOpenEntityFailed=time: {DateTime.Now}, source: {ObjToStr(source)}, obj: {ObjToStr(obj)}, name: {name}, entityName: {entityName}, error: {error}");
        }

        public virtual void AmqpOpenEntitySucceeded(object source, object obj, string name, string entityName)
        {
            Console.WriteLine($">>> AmqpOpenEntitySucceeded=time: {DateTime.Now}, source: {ObjToStr(source)}, obj: {ObjToStr(obj)}, name: {name}, entityName: {entityName}");
        }

        public virtual void AmqpReceiveMessage(object source, uint deliveryId, int transferCount)
        {
            Console.WriteLine($">>> AmqpReceiveMessage=time: {DateTime.Now}, source: {ObjToStr(source)}, deliveryId: {deliveryId}, transferCount: {transferCount}");
        }

        public virtual void AmqpRemoveLink(object connection, object session, object link, uint localHandle, uint remoteHandle, string linkName)
        {
            Console.WriteLine($">>> AmqpRemoveLink=time: {DateTime.Now}, connection: {ObjToStr(connection)}, session: {ObjToStr(session)}, link: {ObjToStr(link)}, localHandle: {localHandle}, remoteHandle: {remoteHandle}, linkName: {linkName}");
        }

        public virtual void AmqpRemoveSession(object source, object session, ushort localChannel, ushort remoteChannel)
        {
            Console.WriteLine($">>> AmqpRemoveSession=time: {DateTime.Now}, source: {ObjToStr(source)}, session: {ObjToStr(session)}, localChannel: {localChannel}, remoteChannel: {remoteChannel}");
        }

        public virtual void AmqpSessionWindowClosed(object source, int nextId)
        {
            Console.WriteLine($">>> AmqpSessionWindowClosed=time: {DateTime.Now}, source: {ObjToStr(source)}, nextId: {nextId}");
        }

        public virtual void AmqpStateTransition(object source, string operation, object fromState, object toState)
        {
            Console.WriteLine($">>> AmqpStateTransition=time: {DateTime.Now}, source: {ObjToStr(source)}, operation: {operation}, fromState: {ObjToStr(fromState)}, toState: {ObjToStr(toState)}");
        }

        public virtual void AmqpUpgradeTransport(object source, object from, object to)
        {
            Console.WriteLine($">>> AmqpUpgradeTransport=time: {DateTime.Now}, source: {ObjToStr(source)}, from: {ObjToStr(from)}, to: {ObjToStr(to)}");
        }

        public virtual void AmqpAbortThrowingException(string exception)
        {
            Console.WriteLine($">>> AmqpAbortThrowingException=time: {DateTime.Now}, exception: {exception}");
        }

        public virtual void AmqpCacheMessage(object source, uint deliveryId, int count, bool isPrefetchingBySize, long totalCacheSizeInBytes, uint totalLinkCredit, uint linkCredit)
        {
            Console.WriteLine($">>> AmqpCacheMessage=time: {DateTime.Now}, source: {ObjToStr(source)}, deliveryId: {deliveryId}, count: {count}, isPrefetchingBySize: {isPrefetchingBySize}, totalCacheSizeInBytes: {totalCacheSizeInBytes}, totalLinkCredit: {totalLinkCredit}, linkCredit: {linkCredit}");
        }

        public virtual void AmqpIoEvent(object source, int ioEvent, long queueSize)
        {
            Console.WriteLine($">>> AmqpIoEvent=time: {DateTime.Now}, source: {ObjToStr(source)}, ioEvent: {ioEvent}, queueSize: {queueSize}");
        }

        public virtual void AmqpHandleException(Exception exception, string traceInfo)
        {
            Console.WriteLine($">>> AmqpHandleException=time: {DateTime.Now}, exception: {ObjToStr(exception)}, traceInfo: {traceInfo}");
        }
    }
}
