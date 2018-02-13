

namespace Lands.Services
{

    using System;
    using System.Net.Http;
    using Models;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ApiServices
    {

        public async Task<Response> GetList<T>(string urlBase, string servicePrefix,string controller)
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

    }
}
