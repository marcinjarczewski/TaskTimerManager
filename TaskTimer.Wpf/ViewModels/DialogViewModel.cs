using Caliburn.Micro;
using TaskTimer.Contracts.Bootstrappers;

namespace TaskTimer.Wpf.ViewModels
{
    public class DialogViewModel : PropertyChangedBase, IScreenViewModel
    {
        private string _Title;

        public string Title 
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Text;

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        public DialogViewModel(string title, string text)
        {
            Text = text;
            Title = title;
        }

        public void Init()
        {
        }
    }
}
