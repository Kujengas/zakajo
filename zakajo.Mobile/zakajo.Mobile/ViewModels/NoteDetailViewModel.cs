using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections;
using Xamarin.Forms;

using zakajo.Mobile.Models;
using zakajo.Mobile.Services;
using zakajo.Model;
using System.Collections.Generic;

namespace zakajo.Mobile.ViewModels
{
    [QueryProperty(nameof(NoteId), nameof(NoteId))]
    public class NoteDetailViewModel : BaseViewModel
    {
        private string noteId;
        private string text;
        private string description;
        private string comment;
        private IEnumerable<Comment> comments;

        public Note CurrentNote { get; set; } = new Note();


        public int Id { get; set; }

        public NoteDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            CommentCommand = new Command(OnComment, ValidateComment);
            this.PropertyChanged +=
               (_, __) => CommentCommand.ChangeCanExecute();
        }


        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        private bool ValidateComment()
        {
            return !String.IsNullOrWhiteSpace(comment);
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

        public string Comment
        {
            get => comment;
            set => SetProperty(ref comment, value);
        }

        public IEnumerable<Comment> Comments
        {
            get => comments;
            set => SetProperty(ref comments, value);
        }

        public string NoteId
        {
            get
            {
                return noteId;
            }
            set
            {
                noteId = value;
                LoadNoteId(value);
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command CommentCommand { get; }

        public async void LoadNoteId(string itemId)
        {
            try
            {
                CurrentNote = await DataStore.GetItemAsync(Convert.ToInt32(itemId));
                Id = CurrentNote.Id;
                Text = CurrentNote.Title;
                Description = CurrentNote.Content;
                Comments = CurrentNote.Comments;
                Shell.Current.Title = Text;

                OnPropertyChanged("CurrentNote");
              
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var app = App.Current as zakajo.Mobile.App;
            var userGuid = app.DeviceId;

            CurrentNote.Content = Description;
            CurrentNote.Title = Text;
            await DataStore.UpdateItemAsync(CurrentNote);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnComment()
        {
        
            Comments = await WebDataService.AddComment(new Comment { CommentText = comment, NoteId = CurrentNote.Id, UpdateUserGuid = CurrentNote.UpdateUserGuid, CommentType = 1 }); 
            LoadNoteId(noteId);
            Comment = string.Empty;
  
        }
    }
}
