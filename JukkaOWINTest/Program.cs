using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JukkaOWINTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:50161";
            using (var client = new HttpClient())
            {
                var form = new Dictionary<string, string>
                {
                   {"grant_type", "password"},
                   {"username", "dstanford350"},
                   {"password", "123456"},
                };

                try
                {
                    var tokenResponse = client.PostAsync(baseAddress + "/oauth/token",
                                                         new FormUrlEncodedContent(form)).Result;
                    //var token = tokenResponse.Content.ReadAsStringAsync().Result;  
                    var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                    if (string.IsNullOrEmpty(token.Error))
                    {
                        Console.WriteLine("Token issued is: {0}\n", token.AccessToken);
                    }
                    else
                    {
                        Console.WriteLine("Error : {0}", token.Error);
                        return;
                    }

                    using (HttpClient httpClient1 = new HttpClient())
                    {
                        httpClient1.BaseAddress = new Uri(baseAddress);
                        httpClient1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.AccessToken);
                        HttpResponseMessage response = httpClient1.GetAsync("api/TestMethod").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            System.Console.WriteLine("Successfully receive API response");
                        }
                        string message = response.Content.ReadAsStringAsync().Result;
                        System.Console.WriteLine("API responese : " + message);
                    }
                }
                catch (AggregateException ex) 
                {
                    Console.WriteLine($"{ex.Message}");
                    return;
                }

                Console.Read();
            }
        }
    }
}
