using OpenGpsTracker.Interfaces;
using OpenGpsTracker.Model;
using OpenGpsTracker.Repository;
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
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public ICommand Save { get; }
        public ICommand Authentication { get; }

        public string Login { get; set; }
        public string Password { get; set; }

        public List<Tracker> AvaliableTrackers { get; set; }

        public SettingsViewModel()
        {
            Save = new Command(SaveUserSettings);
            Authentication = new Command(AuthenticationProcess);

        }

        public event PropertyChangedEventHandler PropertyChanged;

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


            //RESPONSE EXAMPLE
            //<?xml version='1.0' encoding='UTF-8' standalone='no' ?>
            //<GTSResponse command="dbget" result="success">
            //    <Record table="Device" partial="true">
            //        <Field name="accountID" primaryKey="true"><![CDATA[tst]]></Field>
            //        <Field name="deviceID" primaryKey="true"><![CDATA[867273021022871]]></Field>
            //        <Field name="isActive">true</Field>
            //        <Field name="description"><![CDATA[Test 207 [Денис]]]></Field>
            //    </Record>
            //</GTSResponse>           

            //Reading result and create objects

            doc.LoadXml(result);
            xRoot = doc.DocumentElement;
            if (xRoot.Attributes.GetNamedItem("result").Value == "error")
            {
                //ERROR RESPONSE EXAMPLE
                //<?xml version='1.0' encoding='UTF-8' standalone='no' ?>
                //<GTSResponse command="dbget" result="error">
                //   <Message code="AU0010"><![CDATA[Authorization failed]]></Message>
                //</GTSResponse>

                XmlNode ElementMessage = xRoot.FirstChild;
                string errorCode = ElementMessage.Attributes.GetNamedItem("code").Value;
                string message = ElementMessage.InnerText;

                string messageForUser = string.Empty;

                if (message == "Authorization failed")
                {
                    messageForUser = "Не правильный логин или пароль!";
                }
                else
                {
                    messageForUser = string.Format("Ошибка связи с сервером. Message: {0}, ErrorCode: {1}", message, errorCode);
                }                 

                DependencyService.Get<IMessage>().ShortAlert(messageForUser);

                return;
            }

            AvaliableTrackers = new List<Tracker>();

            foreach (XmlNode device in xRoot)
            {
                Tracker tracker = new Tracker();
                tracker.Enabled = true;

                foreach (XmlNode field in device)
                {
                    string fieldName = field.Attributes.GetNamedItem("name").Value;

                    if (fieldName == "deviceID")
                        tracker.DeviceID = field.InnerText;

                    if (fieldName == "description")
                        tracker.Description = field.InnerText;

                }

                AvaliableTrackers.Add(tracker);

                OnPropertyChanged("AvaliableTrackers");
            }

            }


        public void SaveUserSettings()
        {
            User user = new User();
            user.Login = this.Login;
            user.Password = this.Password;
            user.Trackers = AvaliableTrackers;
            user.Current = true;

            UserRepository userrep = new UserRepository(App.DATABASE_NAME);
            userrep.SaveUser(user);

            App.CurrentUser = user;

        }


        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
