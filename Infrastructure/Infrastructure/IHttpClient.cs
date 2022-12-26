namespace WebMVC.Infrastructure
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string Uri, string authorizationtoken = null, string authorizationmethod = "Bearer");
        
        //we are using Post method here to create a new item.like submiting a new form.
        Task<HttpResponseMessage> PostAsync<t>(string Uri, t item, string authorizationtoken = null, string authorizationmethod = "Bearer");
        
        //we are using put method here to edit an existing form.
        Task<HttpResponseMessage> PutAsync<t>(string Uri, t item, string authorizationtoken = null, string authorizationmethod = "Bearer");
        Task<HttpResponseMessage> DeleteAsync(string Uri, string authorizationtoken = null, string authorizationmethod = "Bearer");
    }
}
