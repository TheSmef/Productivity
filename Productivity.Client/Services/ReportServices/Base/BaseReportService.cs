using Microsoft.AspNetCore.WebUtilities;
using Productivity.Client.Constants;
using Productivity.Client.Exceptions;
using Productivity.Shared.Models.DTO.BrokerModels.Base;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.ModelHelpers;
using System.Net;
using System.Net.Http.Json;

namespace Productivity.Client.Services.ReportServices.Base
{
    public class BaseReportService<T> : IReportService<T>
        where T : BaseReportModel
    {
        private readonly IHttpClientFactory _factory;
        private readonly string _url;

        public BaseReportService(IHttpClientFactory factory, string url)
        {
            _factory = factory;
            _url = url;
        }

        public async Task Send(T model)
        {
            try
            {
                HttpClient client = _factory.CreateClient("Main");

                var response = await client.PostAsJsonAsync(_url, model);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    ErrorModel? error = await response.Content.ReadFromJsonAsync<ErrorModel>();
                    if (error == null)
                    {
                        throw new AppException(ExceptionMessages.TitleError, ExceptionMessages.DataRecivingError);
                    }
                    throw new AppException(ExceptionMessages.TitleError, error.Message);
                }
                else
                {
                    throw new AppException(ExceptionMessages.TitleError, ExceptionMessages.DataRecivingError);
                }

            }
            catch (AppException)
            {
                throw;
            }
            catch
            {
                throw new AppException(ExceptionMessages.TitleError, ExceptionMessages.DataRecivingError);
            }
        }
    }
}
