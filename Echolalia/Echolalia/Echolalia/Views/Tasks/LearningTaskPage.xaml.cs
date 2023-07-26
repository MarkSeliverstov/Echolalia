using Xamarin.Forms;
using Echolalia.ViewModels.Tasks.Questions;
using Echolalia.ViewModels.Tasks;

namespace Echolalia.Views.Tasks
{	
	public partial class LearningTaskPage : ContentPage
	{
        public LearningTaskPage(BaseQuestionViewModel questionViewModel)
        {
            InitializeComponent();
            BindingContext = new LearningTaskViewModel(questionViewModel);
        }
    }
}

