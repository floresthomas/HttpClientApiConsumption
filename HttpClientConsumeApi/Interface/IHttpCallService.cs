namespace HttpClientConsumeApi.Interface
{
    public interface IHttpCallService
    {
        Task<T> GetData<T>();
    }
}
