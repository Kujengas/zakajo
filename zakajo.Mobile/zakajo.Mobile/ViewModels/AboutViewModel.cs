using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace zakajo.Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Zakajo Notes";
            Shell.Current.Title = Title;
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.linkedin.com/in/rico-ross/"));
        }

        public ICommand OpenWebCommand { get; }
    }
}