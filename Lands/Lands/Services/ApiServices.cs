namespace Lands.Services
{
    using System;
    using System.Net.Http;
    using Models;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Plugin.Connectivity;
    using System.Text;

    public class ApiServices
    {

        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet setting",
                    
                };
   
            }

            var IsReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!IsReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check you internet connection!"
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "OK"
            };
        }

        public async Task<Response> GetList<T>(string urlBase, string servicePrefix, string controller)
        {

            try
            {

                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = result

                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "OK",
                    Result = list
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }

        }

        public async Task<TokenResponse> GetToken(string urlBase, string username, string password)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var response = await client.PostAsync("Token",
                    new StringContent(string.Format(
                    "grant_type=password&username={0}&password={1}",
                    username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(
                    resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}


