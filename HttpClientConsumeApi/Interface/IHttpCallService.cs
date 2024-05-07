using Microsoft.AspNetCore.Mvc;

namespace HttpClientConsumeApi.Interface
{
    public interface IHttpCallService
    {
        Task<T> GetDataFromApi<T>();
    }
}
