using System.Data;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Obilet.Shared.Common;
using Obilet.Shared.Extensions;
using Tradeport.Shared.Common;
using Microsoft.AspNetCore.Http;

namespace Obilet.Shared.Helpers
{
    public class ApiHelper
    {
        public static Response<T> GetApiResponse<T>(ApiModel api)
        {
            var response = new Response<T>();

            try
            {
                using var client = new HttpClient();

                int retryCount = 0;

                while (true)
                {
                    var task = client.GetAsync(api.Url)
                        .ContinueWith((taskwithresponse) =>
                        {
                            var apiResponse = taskwithresponse.Result;
                            var exceptionMessage = taskwithresponse.Exception != null
                                ? CommonExtension.GetInnerException(taskwithresponse.Exception).Message
                                : "";
                            var jsonString = apiResponse.Content.ReadAsStringAsync();
                            jsonString.Wait();
                            if (apiResponse.IsSuccessStatusCode)
                            {
                                response.Success(JsonConvert.DeserializeObject<T>(jsonString.Result),
                                    StatusCodes.Status200OK);
                            }
                            else
                            {
                                response.AddMessage("ApiError_ExceptionMessage", exceptionMessage);
                                response.AddMessage("ApiError_StatusCode", apiResponse.StatusCode.ToString());
                                response.AddMessage("ApiError_Content", jsonString.Result);
                                response.AddMessage("ApiError", apiResponse.ToString());
                                response.Status = ServiceResponseStatuses.Error;
                                response.StatusCode = StatusCodes.Status400BadRequest;
                            }
                        });
                    task.Wait();

                    if (response.IsSuccessful())
                        break;

                    if (retryCount >= api.RetryCount)
                        break;

                    retryCount++;

                    Thread.Sleep(api.WaitAndRetry * 1000);
                }
            }
            catch (System.Exception ex)
            {
                response.AddMessage("ApiError", CommonExtension.GetInnerException(ex).Message);
                response.AddMessage("ApiErrorTrace", ex.StackTrace);
                response.Status = ServiceResponseStatuses.Error;
                response.StatusCode = StatusCodes.Status400BadRequest;
            }

            return response;
        }

        /// <summary>
        /// Belirtilen adrese http POST yapar ve sonucu belirtilen tipte(TResponse) geri döner
        /// </summary>
        /// <typeparam name="TResponse">Response Type</typeparam>
        /// <typeparam name="TRequest">Request Type</typeparam>
        /// <param name="request">TRequest typed request object</param>
        /// <param name="api">ApiModel</param>
        /// <returns></returns>
        public static Response<TResponse> PostApiResponse<TResponse, TRequest>(TRequest request, ApiModel api)
        {
            var response = new Response<TResponse>();

            try
            {
                using var client = new HttpClient();
                
                HttpClientHeader(client, api);
                
                HttpContent content = api.HttpContentType switch
                {
                    HttpContentType.JsonContent => new StringContent(JsonConvert.SerializeObject(request),
                        Encoding.UTF8, "application/json"),
                    _ => new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
                };

                var contentString = JsonConvert.SerializeObject(request);

                int retryCount = 0;
                while (true)
                {
                    var task = client.PostAsync(api.Url, content)
                        .ContinueWith(postResponse =>
                        {
                            var apiResponse = postResponse.Result;
                            var exceptionMessage = postResponse.Exception != null
                                ? CommonExtension.GetInnerException(postResponse.Exception).Message
                                : "";
                            var jsonString = apiResponse.Content.ReadAsStringAsync();
                            jsonString.Wait();
                            if (apiResponse.IsSuccessStatusCode)
                            {
                                response.Success(JsonConvert.DeserializeObject<TResponse>(jsonString.Result),
                                    StatusCodes.Status200OK);
                            }
                            else
                            {
                                response.AddMessage("ApiError_ExceptionMessage", exceptionMessage);
                                response.AddMessage("ApiError_StatusCode", apiResponse.StatusCode.ToString());
                                response.AddMessage("ApiError_Content", jsonString.Result);
                                response.AddMessage("ApiError", apiResponse.ToString());
                                response.Status = ServiceResponseStatuses.Error;
                                response.StatusCode = StatusCodes.Status400BadRequest;
                            }
                        });

                    task.Wait();

                    if (response.IsSuccessful())
                        break;

                    if (retryCount >= api.RetryCount)
                        break;

                    retryCount++;

                    Thread.Sleep(api.WaitAndRetry * 1000);
                }
            }
            catch (System.Exception ex)
            {
                response.AddMessage("ApiErrorMessage", CommonExtension.GetInnerException(ex).Message);
                response.AddMessage("ApiErrorTrace", ex.StackTrace);
                response.Status = ServiceResponseStatuses.Error;
                response.StatusCode = StatusCodes.Status400BadRequest;
            }

            return response;
        }
        
        private static void HttpClientHeader(HttpClient client, ApiModel api)
        {
            if (api.AuthenticationHeaderValue is not null)
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = api.AuthenticationHeaderValue;
            }

            if (api.CustomerHeaders is not null && api.CustomerHeaders.Any())
            {
                foreach (var customerHeader in api.CustomerHeaders)
                {
                    client.DefaultRequestHeaders.Add(customerHeader.Key, customerHeader.Value);
                }
            }
        }
    }
}