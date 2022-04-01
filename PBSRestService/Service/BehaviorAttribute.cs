using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace PBS.Service
{
    public class BehaviorAttribute : Attribute, IEndpointBehavior,
                                   IOperationBehavior
    {
        public void Validate(ServiceEndpoint endpoint) { }

        public void AddBindingParameters(ServiceEndpoint endpoint,
                                 BindingParameterCollection bindingParameters)
        { }

        /// <summary>
        /// This service modify or extend the service across an endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint that exposes the contract.</param>
        /// <param name="endpointDispatcher">The endpoint dispatcher to be
        /// modified or extended.</param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint,
                                          EndpointDispatcher endpointDispatcher)
        {
            // add inspector which detects cross origin requests
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(
                                                   new MessageInspector(endpoint));
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint,
                                        ClientRuntime clientRuntime)
        { }

        public void Validate(OperationDescription operationDescription) { }

        public void ApplyDispatchBehavior(OperationDescription operationDescription,
                                          DispatchOperation dispatchOperation)
        { }

        public void ApplyClientBehavior(OperationDescription operationDescription,
                                        ClientOperation clientOperation)
        { }

        public void AddBindingParameters(OperationDescription operationDescription,
                                  BindingParameterCollection bindingParameters)
        { }

    }
}
