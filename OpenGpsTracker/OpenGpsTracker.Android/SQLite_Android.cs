using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OpenGpsTracker.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace OpenGpsTracker.Droid
{
    class SQLite_Android : ISQLite
    {
        public string GetDatabasePath(string sqlliteFilename)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqlliteFilename);
            return path;

        }
    }
}