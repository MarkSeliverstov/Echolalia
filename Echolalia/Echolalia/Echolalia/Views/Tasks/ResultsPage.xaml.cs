using Xamarin.Forms;
using Echolalia.ViewModels.Tasks;

namespace Echolalia.Views.Tasks
{	
	public partial class ResultsPage : ContentPage
	{	
		public ResultsPage (int rightAnswersCount, int questionCount)
		{
			InitializeComponent ();
			this.BindingContext = new ResultsPageViewModel(rightAnswersCount, questionCount);
		}
	}
}

