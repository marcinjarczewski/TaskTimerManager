using Caliburn.Micro;
using TaskTimer.Contracts.Bootstrappers;

namespace TaskTimer.Wpf.ViewModels
{
    public class DialogViewModel : Screen, IScreenViewModel
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

        private bool _isConfirm;

        public bool IsConfirm
        {
            get { return _isConfirm; }
            set { _isConfirm = value; }
        }

        private bool _result;

        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }


        public DialogViewModel(string title, string text)
        {
            Text = text;
            Title = title;
            IsConfirm = false;
        }

        public DialogViewModel(bool isConfirm, string title, string text)
        {
            Text = text;
            Title = title;
            IsConfirm = isConfirm;
        }

        public void Init()
        {
        }

        public void Confirm()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
