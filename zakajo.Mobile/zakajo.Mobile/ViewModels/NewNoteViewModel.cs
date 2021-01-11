using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using zakajo.Mobile.Models;
using zakajo.Model;

namespace zakajo.Mobile.ViewModels
{
    public class NewNoteViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private int noteTypeId;

        public NewNoteViewModel()
        {

            var shell = AppShell.Current as zakajo.Mobile.AppShell;
            noteTypeId = shell.SelectedNoteType;
      

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var app = App.Current as zakajo.Mobile.App;
            var userGuid = app.DeviceId;


            Note newItem = new Note()
            {
                NoteType = noteTypeId,
                Title = Text,
                Content = Description,
                UpdateUserGuid = userGuid,
                ImageFile = string.Empty,
                ImageThumb = string.Empty
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
