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
    public partial class CultureReportModal
    {
        private CultureReportModel model = new CultureReportModel() { Year = years.First() };

        [Inject]
        private NotificationService? NotificationService { get; set; }

        private static List<int> years = [DateTime.Today.Year, DateTime.Today.Year + 1];


        [Inject]
        private DialogService? DialogService { get; set; }

        [Inject]
        private ICultureReportService? CultureReportService { get; set; }

        [Inject]
        private IRegionService? RegionService { get; set; }


        private CollectionDTO<RegionDTO> regions = new CollectionDTO<RegionDTO>();

        [CascadingParameter]
        public Error? Error { get; set; }


        protected async override Task OnInitializedAsync()
        {
            await RegionDataInit();
            await base.OnInitializedAsync();
        }

        private async Task Handle()
        {
            try
            {
                await CultureReportService!.Send(model);
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