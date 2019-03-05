using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OpenGpsTracker
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            Detail = new NavigationPage(new MapPage());


        }

        private void BtnSettings_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new SettingsPage());
            IsPresented = false;
        }

        private void BtnShowMap_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new MapPage());
            IsPresented = false;
        }
    }
}
