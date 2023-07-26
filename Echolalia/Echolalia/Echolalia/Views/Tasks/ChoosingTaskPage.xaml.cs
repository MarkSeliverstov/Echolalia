using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Echolalia.ViewModels.Tasks.Questions;
using Echolalia.ViewModels.Tasks;

namespace Echolalia.Views.Tasks
{	
	public partial class ChoosingTaskPage : ContentPage
	{	
		public ChoosingTaskPage (BaseQuestionViewModel questionViewModel)
		{
			InitializeComponent ();
            BindingContext = new ChoosingTaskViewModel(questionViewModel);
        }
	}
}

