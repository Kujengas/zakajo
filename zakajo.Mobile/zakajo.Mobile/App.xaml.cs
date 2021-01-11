using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zakajo.Mobile.Services;
using zakajo.Mobile.Views;

namespace zakajo.Mobile
{
    public partial class App : Application
    {
        public string DeviceId;
        
        public App()
        {
            InitializeComponent();
           
            initDeviceId();

            DependencyService.Register<NoteDataStore>();
            DependencyService.Register<NoteTypeDataStore>();

            MainPage = new AppShell();
        }

        private void initDeviceId()
        {
           
            DeviceId = Guid.NewGuid().ToString();
            if (Xamarin.Essentials.Preferences.ContainsKey("DeviceId"))
            {
                DeviceId = Xamarin.Essentials.Preferences.Get("DeviceId", DeviceId);
            }
            else
            {
                Xamarin.Essentials.Preferences.Set("DeviceId", DeviceId);
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
