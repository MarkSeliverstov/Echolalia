using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Echolalia.Views;
using Echolalia.Data;
using System.IO;

namespace Echolalia
{
    public partial class App : Application
    {
        static localDB db;

        public static localDB localDB
        {
            get
            {
                if (db == null)
                {
                    db = new localDB(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "local.db3"));
                }

                return db;
            }
        }

        public App ()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

