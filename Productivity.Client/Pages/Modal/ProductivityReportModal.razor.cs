using Microsoft.AspNetCore.Components;
using Productivity.Client.Constants;
using Productivity.Client.Pages.Components;
using Productivity.Client.Services.DataServices.Interfaces;
using Productivity.Client.Services.ReportServices.Interfaces;
using Productivity.Client.Utilty;
using Productivity.Shared.Models.DTO.BrokerModels;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.Utility;
using Radzen;

namespace Productivity.Client.Pages.Modal
{
    public partial class ProductivityReportModal
    {
        private ProductivityReportModel model = new ProductivityReportModel() { Year = years.First() };

        [Inject]
        private NotificationService? NotificationService { get; set; }

        private static List<int> years = [DateTime.Today.Year, DateTime.Today.Year + 1];


        [Inject]
        private DialogService? DialogService { get; set; }

        [Inject]
        private IProductivityReportService? ProductivityReportService { get; set; }

        [Inject]
        private IRegionService? RegionService { get; set; }

        [Inject]
        private ICultureService? CultureService { get; set; }

        private CollectionDTO<CultureDTO> cultures = new CollectionDTO<CultureDTO>();
        private CollectionDTO<RegionDTO> regions = new CollectionDTO<RegionDTO>();

        [CascadingParameter]
        public Error? Error { get; set; }


        protected async override Task OnInitializedAsync()
        {
            await RegionDataInit();
            await CultureDataInit();
            await base.OnInitializedAsync();
        }

        private async Task Handle()
        {
            try
            {
                await ProductivityReportService!.Send(model);
                NotificationService!.Notify(NotificationSeverity.Success, PageConstants.Success, PageConstants.ReportResponce, 4000);
                Close();
            }
            catch (Exception ex)
            {
                Error!.CatchError(ex);
            }
        }

        private async Task RegionDataInit()
        {
            try
            {
                QuerySupporter regionquery = new QuerySupporter();
                regionquery.Skip = 0;
                regionquery.Top = 10;
                regions = await RegionService!.Get(regionquery);
            }
            catch (Exception ex)
            {
                Error!.CatchError(ex);
            }
        }

        private async Task CultureDataInit()
        {
            try
            {
                QuerySupporter culturequery = new QuerySupporter();
                culturequery.Skip = 0;
                culturequery.Top = 10;
                cultures = await CultureService!.Get(culturequery);
            }
            catch (Exception ex)
            {
                Error!.CatchError(ex);
            }
        }


        private async Task SetCultureQuery(LoadDataArgs args)
        {
            try
            {
                QuerySupporter culturequery = new QuerySupporter();
                culturequery.Skip = args.Skip ?? 0;
                culturequery.Top = args.Top ?? 10;
                FilterHelper.SetFilterQuery(culturequery, args.Filter ?? string.Empty);
                cultures = await CultureService!.Get(culturequery);
            }
            catch (Exception ex)
            {
                Error!.CatchError(ex);
            }
        }



        private async Task SetRegionQuery(LoadDataArgs args)
        {
            try
            {
                QuerySupporter regionquery = new QuerySupporter();
                regionquery.Skip = args.Skip ?? 0;
                regionquery.Top = args.Top ?? 10;
                FilterHelper.SetFilterQuery(regionquery, args.Filter ?? string.Empty);
                regions = await RegionService!.Get(regionquery);
            }
            catch (Exception ex)
            {
                Error!.CatchError(ex);
            }
        }



        protected void Close()
        {
            DialogService!.Close(null);
        }
    }
}