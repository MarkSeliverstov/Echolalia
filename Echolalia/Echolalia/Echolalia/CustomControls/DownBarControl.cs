using System;
using Xamarin.Forms;
using Echolalia.Views;

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

			var homeBtn = CreateIBtn("IconHome.png");
			homeBtn.Clicked += OnHomeButtonClicked;

			var addBtn = CreateIBtn("IconAdd.png");
			addBtn.Clicked += OnAddButtonClicked;

            var libraryBtn = CreateIBtn("IconLibrary.png");
            libraryBtn.Clicked += OnLibraryButtonClicked;

            stack.Children.Add(homeBtn);
            stack.Children.Add(addBtn);
            stack.Children.Add(libraryBtn);

            Content = stack;
		}

        private void OnLibraryButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LibraryPage());
        }

        private void OnAddButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddingPage());
        }

        private void OnHomeButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        private ImageButton CreateIBtn(string SourceOfImage)
		{
			return new ImageButton
            {
                Source = SourceOfImage,
                BackgroundColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(5, 0, 0, 20),
                Scale = 0.9,
            };
        }
	}
}

