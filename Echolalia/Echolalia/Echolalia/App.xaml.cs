using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Echolalia.Data;

namespace Echolalia
{
    public partial class App : Application
    {

        static LibraryDB libraryDB;

        public static LibraryDB LibraryDB
        {
            get
            {
                if (libraryDB == null) {
                    libraryDB = new LibraryDB(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "LibraryDatabase.db3"));
                }
                return libraryDB;
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

