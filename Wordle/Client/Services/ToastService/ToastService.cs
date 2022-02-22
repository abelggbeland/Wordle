using Wordle.Client.Components;
using Wordle.Shared;

namespace Wordle.Client.Services.ToastService
{
    public class ToastService
    {
        public event Action<string, ToastLevel, int> OnShow;
        public event Action<int> OnHide;
        private List<TimerCountdown> Countdowns = new List<TimerCountdown>();
        private int IdValue = 0;

        public void ShowToast(string message, ToastLevel level)
        {

            OnShow?.Invoke(message, level, IdValue);
            StartCountdown(IdValue);
            IdValue++;

        }

        private void StartCountdown(int ToastId)
        {
            SetCountdown(ToastId);

            if (Countdowns.Find(c => c.Id == ToastId).timer.Enabled)
            {
                Countdowns.Find(c => c.Id == ToastId).timer.Stop();
                Countdowns.Find(c => c.Id == ToastId).timer.Start();
            }
            else
            {
                Countdowns.Find(c => c.Id == ToastId).timer.Start();
            }
        }

        private void SetCountdown(int ToastId)
        {
            if (Countdowns.Find(c => c.Id == ToastId) == null)
            {
                Countdowns.Add(new TimerCountdown
                {
                    Id = ToastId,
                    timer = new System.Timers.Timer(1500)
                });
                Countdowns.Find(c => c.Id == ToastId).timer.Elapsed += (sender, e) => HideToast(sender, e, ToastId); ;
                Countdowns.Find(c => c.Id == ToastId).timer.AutoReset = false;
            }
        }

        private void HideToast(object source, System.Timers.ElapsedEventArgs args, int ToastId)
        {
            OnHide?.Invoke(ToastId);
        }
    }
}
