using System;
using Xamarin.Forms;

namespace Echolalia
{
	public class DownBarControl : Frame
	{
		public DownBarControl()
		{
			Margin = new Thickness(0, 0, 0, -60);
			Padding = new Thickness(0, 0, 0, 20);
			CornerRadius = 60;
			BackgroundColor = Color.FromHex("#5B5F97");

			var stack = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(60, 10, 60, 45)
			};

			stack.Children.Add(new Image
			{
				Source = "IconHomeActive.png",
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Margin = new Thickness(5, 0, 0, 20),
				Scale = 0.9
			});

			stack.Children.Add(new Image
			{
				Source = "IconAdd.png",
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Margin = new Thickness(40, 0, 0, 20),
				Scale = 0.9
			});

			stack.Children.Add(new Image
			{
				Source = "IconLibrary.png",
				HorizontalOptions = LayoutOptions.EndAndExpand,
				Margin = new Thickness(5, 0, 0, 20),
				Scale = 0.8
			});

			Content = stack;
		}
	}
}

