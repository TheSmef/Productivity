using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.WebUtilities;
using Productivity.Client.Constants;
using Productivity.Client.Exceptions;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.ModelHelpers;
using System.Net;
using System.Net.Http.Json;

namespace Productivity.Client.Services.DataServices.Base
{
    public class BaseDataService<TDTO> : IDataService<TDTO>
    {
        private readonly IHttpClientFactory _factory;
        private readonly string _url;

        public BaseDataService(IHttpClientFactory factory, string url)
        {
            _factory = factory;
            _url = url;
        }

        public async Task<CollectionDTO<TDTO>> Get(QuerySupporter query)
        {
            try
            {
                HttpClient client = _factory.CreateClient("Main");
                var param = QueryMapper.MapToQuery(query);

                var url = QueryHelpers.AddQueryString(_url, param!);

                var response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    CollectionDTO<TDTO>? responce = await response.Content.ReadFromJsonAsync<CollectionDTO<TDTO>>();
                    if (responce == null)
                    {
                        throw new AppException(ExceptionMessages.TitleError, ExceptionMessages.DataRecivingError);
                    }
                    return responce;
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

        public async Task<TDTO> GetById(Guid id)
        {
            try
            {
                HttpClient client = _factory.CreateClient("Main");
                var query = new Dictionary<string, string>
                {
                        { "id", id.ToString() }
                };

                var url = QueryHelpers.AddQueryString(_url, query!);

                var response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    TDTO? responce = await response.Content.ReadFromJsonAsync<TDTO>();
                    if (responce == null)
                    {
                        throw new AppException(ExceptionMessages.TitleError, ExceptionMessages.DataRecivingError);
                    }
                    return responce;
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
