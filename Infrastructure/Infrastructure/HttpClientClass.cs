namespace WebMVC.Infrastructure
{
    public class HttpClientClass : IHttpClient
    {
        //HttpClient is inbuilt class in c# that will do the magic of taking the link to microservice/api we wrote and bring back the results.
        private readonly HttpClient _httpclint;
        public HttpClientClass()
        {
            _httpclint = new HttpClient();
        }
        public Task<HttpResponseMessage> DeleteAsync(string Uri, string authorizationtoken = null, string authorizationmethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetStringAsync(string Uri, string authorizationtoken = null, string authorizationmethod = "Bearer")
        {
            //these 3 codes does exact same thing as postman but these are codes.
            //first line of code get the uri

            var request = new HttpRequestMessage(HttpMethod.Get, Uri);

            //second line send the link like we click send in postman

            var response = await _httpclint.SendAsync(request);

            //the third line get the result back but it will be in Json so we are converting them in string by readasstringasync.
            return await response.Content.ReadAsStringAsync();
        }

        

        public Task<HttpResponseMessage> PostAsync<t>(string Uri, t item, string authorizationtoken = null, string authorizationmethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PutAsync<t>(string Uri, t item, string authorizationtoken = null, string authorizationmethod = "Bearer")
        {
            throw new NotImplementedException();
        }
    }
}
