using System;
using Xamarin.Forms;

namespace Echolalia
{
	public class TopBarControl: StackLayout
	{
		public TopBarControl()
		{
            Padding = (Device.RuntimePlatform == Device.iOS) ? new Thickness(30, 40, 30, 5) : new Thickness(30, 0, 30, 5);
			Orientation = StackOrientation.Horizontal;
			BackgroundColor = Color.FromHex("#5B5F97");
			VerticalOptions = LayoutOptions.StartAndExpand;

			this.Children.Add(new Image {
				Source = "IconSettings.png",
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Scale = 0.9,
				Margin = new Thickness(0, 20, 0, 0)
            });

            this.Children.Add(new Label
            {
                Text = "Echolalia",
                TextColor = Color.White,
                FontFamily = "Exo-Bold",
                FontSize = 25,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0),
            });

            this.Children.Add(new Image
            {
                Source = "IconStats.png",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Scale = 0.8,
                Margin = new Thickness(0, 20, 0, 0),
            });
        }
    }
}

