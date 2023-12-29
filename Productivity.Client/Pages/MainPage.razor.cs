using Microsoft.AspNetCore.Components;
using Productivity.Client.Constants;
using Productivity.Client.Models;
using Productivity.Client.Pages.Components;
using Productivity.Client.Pages.Modal;
using Productivity.Client.Services.DataServices.Interfaces;
using Productivity.Client.Utilty;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.Utility;
using Radzen;
using Radzen.Blazor;

namespace Productivity.Client.Pages
{
    public partial class MainPage
    {
        [Inject]
        private IProductivityService? ProductivityService { get; set; }

        [Inject]
        private IRegionService? RegionService { get; set; }

        [Inject]
        private ICultureService? CultureService { get; set; }
        [CascadingParameter]
        public Error? Error { get; set; }

        private RadzenDataList<ProductivityDTO> prodList = new RadzenDataList<ProductivityDTO>();

        private bool sidebar = true;

        private QuerySupporter query = new QuerySupporter();
        private List<string> selectedRegions = [];

        private List<string> selectedCultures = [];

        private RadzenButton button = new RadzenButton();
        private Radzen.Blazor.Rendering.Popup popup = new Radzen.Blazor.Rendering.Popup();
        private RadzenButton insbtn = new RadzenButton();
        private Radzen.Blazor.Rendering.Popup popupins = new Radzen.Blazor.Rendering.Popup();

        private RangeModel years = new RangeModel() { Max = DateTime.Today.Year, Min = 1992 };
        private RangeModel productivitiesRange = new RangeModel();

        private CollectionDTO<ProductivityDTO> productivities = new CollectionDTO<ProductivityDTO>();
        private CollectionDTO<CultureDTO> cultures = new CollectionDTO<CultureDTO>();
        private CollectionDTO<RegionDTO> regions = new CollectionDTO<RegionDTO>();


        [Inject]
        private DialogService? DialogService { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await prodList.Reload();
                await RegionDataInit();
                await CultureDataInit();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task SetProductivityParams()
        {
            FilterHelper.SetFilterQuery(query, selectedRegions, selectedCultures, productivitiesRange, years);
            await prodList.FirstPage(true);
        }


        private async Task SetProductivityQuery(LoadDataArgs args)
        {
            try
            {
                query.Skip = args.Skip ?? 0;
                query.Top = args.Top ?? 10;
                productivities = await ProductivityService!.Get(query);
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


        private async Task SetOrder(string param)
        {
            query.OrderBy = param;
            await prodList.FirstPage(true);
            await popup.CloseAsync(button.Element);
        }

        private async Task OpenProductivityReportModal()
        {
            await DialogService!.OpenAsync<ProductivityReportModal>(PageConstants.ProductivityReportTitle, null, new DialogOptions()
            { CloseDialogOnOverlayClick = true });
            await popup.CloseAsync(insbtn.Element);
        }

        private async Task OpenCultureReportModal()
        {
            await DialogService!.OpenAsync<CultureReportModal>(PageConstants.CultureReportTitle, null, new DialogOptions()
            { CloseDialogOnOverlayClick = true });
            await popup.CloseAsync(insbtn.Element);
        }
    }
}