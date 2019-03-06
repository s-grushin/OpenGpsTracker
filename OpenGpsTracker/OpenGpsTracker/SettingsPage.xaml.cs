﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenGpsTracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
		}

        private void AutoUpdateEnable_Toggled(object sender, ToggledEventArgs e)
        {   

            LblInterval.IsEnabled = (sender as Switch).IsToggled;
            Interval.IsEnabled = (sender as Switch).IsToggled;
        }
    }
}