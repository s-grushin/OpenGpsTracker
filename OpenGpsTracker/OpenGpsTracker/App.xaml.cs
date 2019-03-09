using OpenGpsTracker.Model;
using OpenGpsTracker.Repository;
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

        public static User CurrentUser { get; set; }


        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {

            UserRepository userRepository = new UserRepository(DATABASE_NAME);
            App.CurrentUser = userRepository.GetCurrentUser();

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
