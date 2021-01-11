using System;
using System.Collections.Generic;
using Xamarin.Forms;
using zakajo.Mobile.ViewModels;
using zakajo.Mobile.Views;

namespace zakajo.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {

        public int SelectedNoteType { get; set; }
        public string  SelectedNoteTypeTitle { get; set; } 

        public AppShell()
        {
            InitializeComponent();
        

            Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));
            Routing.RegisterRoute(nameof(NewNotePage), typeof(NewNotePage));
            Routing.RegisterRoute(nameof(NotesPage), typeof(NotesPage));
        }

        //  $"{nameof(NotesPage)}?{nameof(NoteViewModel.NoteTypeId)}={NoteType.Id}"


        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Shell.Current.CurrentItem.CurrentItem.Items.Add(new AboutPage());
            Shell.Current.CurrentItem.CurrentItem.Items.RemoveAt(0);
            Shell.Current.FlyoutIsPresented = false;
        }
         
        private async void OnCategoryClicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            SelectedNoteType = Convert.ToInt32(item.CommandParameter);
            SelectedNoteTypeTitle = item.Text ;
            // await Shell.Current.GoToAsync("ItemsPage");
            //await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?{nameof(NoteDetailViewModel.NoteId)}={Note.Id}");
          //  await Shell.Current.GoToAsync($"//NotesPage");

            Shell.Current.CurrentItem.CurrentItem.Items.Add(new NotesPage());
            Shell.Current.CurrentItem.CurrentItem.Items.RemoveAt(0);
            Shell.Current.FlyoutIsPresented = false;

        }
    }
}
