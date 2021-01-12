using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zakajo.Mobile.Models;
using zakajo.Mobile.ViewModels;
using zakajo.Mobile.Views;
using zakajo.Model;

namespace zakajo.Mobile.Views
{  
    
   // [QueryProperty(nameof(NoteTypeId), "NoteTypeId")]
   // [QueryProperty(nameof(Title), nameof(Title))]
    public partial class NotesPage : ContentPage
    {
        NotesViewModel _viewModel;

        public string NoteTypeId { get; set; }

        public NotesPage()
        {
            InitializeComponent();
                    
            BindingContext = _viewModel = new NotesViewModel();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}