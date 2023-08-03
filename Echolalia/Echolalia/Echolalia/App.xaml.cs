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
        // SQLite database connection: local.db3
        static LocalDB db;

        public static LocalDB localDB
        {
            get
            {
                if (db == null)
                {
                    db = new LocalDB(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "locall.db3"));
                }

                return db;
            }
        }

        public static PreferencesDB preferencesDB = new PreferencesDB();

        public App ()
        {
            InitializeComponent();
            localDB.Init();
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

