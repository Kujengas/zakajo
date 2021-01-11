using System.ComponentModel;
using Xamarin.Forms;
using zakajo.Mobile.ViewModels;
using zakajo.Model;

namespace zakajo.Mobile.Views
{
    public partial class NoteDetailPage : ContentPage
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command CommentCommand { get; }

        public NoteDetailPage()
        {
            InitializeComponent();
            BindingContext = new NoteDetailViewModel();
        }

       
    }
}