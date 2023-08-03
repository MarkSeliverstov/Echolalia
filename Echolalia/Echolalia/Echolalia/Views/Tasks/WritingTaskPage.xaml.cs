using System;
using System.Collections.Generic;
using Echolalia.ViewModels.Tasks;
using Echolalia.ViewModels.Tasks.Questions;
using Xamarin.Forms;

namespace Echolalia.Views.Tasks
{
    public partial class WritingTaskPage : ContentPage
    {
        public WritingTaskPage(BaseQuestionViewModel questionViewModel)
        {
            InitializeComponent();
            BindingContext = new WritingTaskViewModel(questionViewModel);
        }
    }
}

