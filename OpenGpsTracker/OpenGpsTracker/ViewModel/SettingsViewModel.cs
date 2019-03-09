using OpenGpsTracker.Model;
using OpenGpsTracker.Repository;
using OpenGpsTracker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using System.Xml;
using Xamarin.Forms;

namespace OpenGpsTracker.ViewModel
{
    public class SettingsViewModel
    {
        public ICommand Save { get; }
        public ICommand Authentication { get; }

        public string Login { get; set; }
        public string Password { get; set; }

        public SettingsViewModel()
        {
            Save = new Command(SaveUserSettings);
            Authentication = new Command(AuthenticationProcess);
        }

        public async void AuthenticationProcess()
        {
            
            XmlDocument doc = new XmlDocument();
            Stream stream = this.GetType().Assembly.GetManifestResourceStream("OpenGpsTracker.TrackersRequest.xml");
            doc.Load(stream);

            HttpClient httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri(App.GPSSERVER_ADRESS);

            XmlElement xRoot = doc.DocumentElement;

            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Name == "Authorization")
                {
                    XmlNode attr1 = xnode.Attributes.GetNamedItem("account");
                    attr1.Value = Login;

                    XmlNode attr2 = xnode.Attributes.GetNamedItem("password");
                    attr2.Value = Password;
                }

                if (xnode.Name == "Record")
                {
                    foreach (XmlNode fieldNode in xnode.ChildNodes)
                    {
                        foreach (XmlNode fieldAttr in fieldNode.Attributes)
                        {
                            if (fieldAttr.Name == "name" && fieldAttr.Value == "accountID")
                            {
                                //Установим текст элемента(не перепутать с атрибутом)
                                fieldNode.InnerText = Login;
                            }
                        }
                    }
                }

            }

            var content = new StringContent(doc.OuterXml, Encoding.UTF8, "application/xml");
            HttpResponseMessage response = await httpClient.PostAsync(App.GPSSERVER_ADRESS, content);

            var result = await response.Content.ReadAsStringAsync();

            var a = 1;





        }


        public void SaveUserSettings()
        {
            User user = new User();
            user.Login = this.Login;
            user.Password = this.Password;

            UserRepository userrep = new UserRepository(App.DATABASE_NAME);
            userrep.SaveUser(user);

        }

    }
}
