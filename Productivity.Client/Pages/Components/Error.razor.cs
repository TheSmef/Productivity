using Microsoft.AspNetCore.Components;
using Productivity.Client.Constants;
using Productivity.Client.Exceptions;
using Radzen;

namespace Productivity.Client.Pages.Components
{
    public partial class Error
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Inject]
        private NotificationService? NotificationService { get; set; }

        public void CatchError(Exception ex)
        {
            (string title, string message) = ex switch
            {
                AppException => (title = (ex as AppException)!.Title, message = ex.Message),
                _ => (title = ExceptionMessages.TitleError, message = ExceptionMessages.DefaultError),
            };
            NotificationService!.Notify(NotificationSeverity.Error, title, message, 4000);
        }
    }
}