using Caliburn.Micro;
using TaskTimer.Contracts;

namespace TaskTimer.WPF.DesignTime.Models
{
    public class DesignDialogViewModel : PropertyChangedBase
    {
        public string Text { get; set; }

        public string Title { get; set; }
        public DesignDialogViewModel()
        {
            Text = "The operation was completed successfully.";
            Title = "Confirmation";
        }
    }
}
