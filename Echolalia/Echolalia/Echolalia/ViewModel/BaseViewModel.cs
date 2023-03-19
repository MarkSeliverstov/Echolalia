using System;
using Xamarin.Forms;

namespace Echolalia.ViewModel
{
	public class BaseViewModel
	{
		public BaseViewModel()
		{
            ToolbarItem item = new ToolbarItem
            {
                Text = "Example Item",
                IconImageSource = ImageSource.FromFile("example_icon.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };

            // "this" refers to a Page object
        }
	}
}

