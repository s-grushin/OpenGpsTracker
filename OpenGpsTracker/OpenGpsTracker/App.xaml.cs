using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OpenGpsTracker
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "opengps.db";
        public const string GPSSERVER_ADRESS = "http://private.opengts.com.ua:8080/track/Service";
        

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
