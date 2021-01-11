using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using zakajo.Mobile.Models;
using zakajo.Mobile.ViewModels;

namespace zakajo.Mobile.Views
{
    public partial class NewNotePage : ContentPage
    {
        public Item Item { get; set; }

        public NewNotePage()
        {
            InitializeComponent();
            BindingContext = new NewNoteViewModel();
        }
    }
}