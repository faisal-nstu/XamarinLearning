using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABBLUWPApp.Services
{
    public interface INetworkOperation
    {
        Task<TResponse> ExceutePostOperation<TRequest, TResponse>(Uri uri, TRequest requestObject);
        Task<TResponse> ExceutePostOperationAuthless<TRequest, TResponse>(Uri uri, TRequest requestObject);
        Task<TResponse> ExecuteGetOperation<TResponse>(Uri uri);
        Task<TResponse> ExecuteGetOperationAuthless<TResponse>(Uri uri);
        Task<string> UploadFile(Uri uri, Dictionary<string, string> headers, byte[] fileBytes);
        Task<TResponse> ExceutePostOperation<TRequest, TResponse>(Uri uri, TRequest requestObject, CancellationToken token);
        Task<byte[]> ExecuteFileGetOperation(Uri uri);
    }
}
