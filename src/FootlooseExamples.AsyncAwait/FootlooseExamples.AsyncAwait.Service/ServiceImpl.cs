using System;
using FootlooseExamples.AsyncAwait.Shared;

namespace FootlooseExamples.AsyncAwait.Service
{
    public class ServiceImpl : IServiceContract
    {
        public ResponseDto HandleRequest(RequestDto request)
        {
            Console.WriteLine("Received request with input value '{0}'.", request.InputValue);
            return new ResponseDto() {OutputValue = request.InputValue};
        }
    }
}