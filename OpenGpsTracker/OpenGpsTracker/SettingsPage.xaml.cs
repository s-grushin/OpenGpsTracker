using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using OpenGpsTracker.Model;
using OpenGpsTracker.ViewModel;

namespace OpenGpsTracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{

        public SettingsPage ()
		{
			InitializeComponent ();
            this.BindingContext = new SettingsViewModel();
          
		}

    }
}