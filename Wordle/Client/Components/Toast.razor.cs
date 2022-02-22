using Microsoft.AspNetCore.Components;

using Wordle.Client.Services;
using Wordle.Client.Services.ToastService;
using Wordle.Shared;

namespace Wordle.Client.Components
{
    public class ToastBase : ComponentBase, IDisposable
    {
        [Inject] ToastService ToastService { get; set; }

        public List<ToastModel> toastModels { get; set; } = new List<ToastModel>();


        protected override void OnInitialized()
        {
            ToastService.OnShow += CreateToast;
            ToastService.OnHide += HideToast;
        }

        private void CreateToast(string message, ToastLevel level, int ToastId)
        {
            ToastModel toastModel = BuildToastSettings(level, message);
            toastModel.Id = ToastId;
            toastModels.Add(toastModel);
            toastModel.IsVisible = true;
            StateHasChanged();
        }

        private void HideToast(int ToastId)
        {
            toastModels.Find(t => t.Id == ToastId).IsVisible = false;
            StateHasChanged();
        }

        private ToastModel BuildToastSettings(ToastLevel level, string message)
        {
            ToastModel toastModel = new ToastModel();
            switch (level)
            {
                case ToastLevel.Info:
                    toastModel.BackgroundCssClass = "backgroundcolor";
                    toastModel.IconCssClass = "info";
                    toastModel.Heading = "Info";
                    break;
                case ToastLevel.Success:
                    toastModel.BackgroundCssClass = "bg-success";
                    toastModel.IconCssClass = "check";
                    toastModel.Heading = "Success";
                    break;
                case ToastLevel.Warning:
                    toastModel.BackgroundCssClass = "bg-warning";
                    toastModel.IconCssClass = "exclamation";
                    toastModel.Heading = "Warning";
                    break;
                case ToastLevel.Error:
                    toastModel.BackgroundCssClass = "bg-danger";
                    toastModel.IconCssClass = "times";
                    toastModel.Heading = "Error";
                    break;
            }

            toastModel.Message = message;
            return toastModel;
        }

        public void Dispose()
        {
            ToastService.OnShow -= CreateToast;
        }
    }
}
