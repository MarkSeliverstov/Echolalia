using System.ComponentModel;
using Xamarin.Forms;
using Echolalia.ViewModels;

namespace Echolalia.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
