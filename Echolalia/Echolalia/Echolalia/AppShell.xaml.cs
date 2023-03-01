using System;
using System.Collections.Generic;
using Echolalia.ViewModels;
using Echolalia.Views;
using Xamarin.Forms;

namespace Echolalia
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}

