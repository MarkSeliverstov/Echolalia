using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Echolalia.Views
{	
	public partial class AddingPage : ContentPage
	{
        ToolbarItem item = new ToolbarItem
        {
            Text = "Example Item",
            IconImageSource = ImageSource.FromFile("example_icon.png"),
            Order = ToolbarItemOrder.Primary,
            Priority = 0
        };

        
    }
}

