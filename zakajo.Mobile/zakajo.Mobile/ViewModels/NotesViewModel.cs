using System;
using System.Collections.ObjectModel;

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using zakajo.Mobile.Models;
using zakajo.Mobile.Views;
using zakajo.Model;

namespace zakajo.Mobile.ViewModels
{
    
    public class NotesViewModel : BaseViewModel
    {
        private Note _selectedNote;

        public ObservableCollection<Note> Notes { get; }
        public Command LoadNotesCommand { get; }
        public Command AddNoteCommand { get; }
        public Command<Note> NoteTapped { get; }
        public int NoteTypeId { get; set; }

        public NotesViewModel()
        {
            //Title = "Browse All Notes";
            Notes = new ObservableCollection<Note>();
            LoadNotesCommand = new Command(async () => await ExecuteLoadNotesCommand());

            NoteTapped = new Command<Note>(OnNoteSelected);

            AddNoteCommand = new Command(OnAddNote);
        }

        async Task ExecuteLoadNotesCommand()
        {
            IsBusy = true;

            var shell = AppShell.Current as zakajo.Mobile.AppShell;
            NoteTypeId = shell.SelectedNoteType;
            Title = shell.SelectedNoteTypeTitle;


            try
            {
                Notes.Clear();
                var notes = await DataStore.GetItemsAsync(true);
                notes.Where(note=>note.NoteType==NoteTypeId).ForEach<Note>(n => Notes.Add(n));
/*
                foreach (var Note in notes)
                {
                    Notes.Add(Note);
                }
  */         
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedNote = null;
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                SetProperty(ref _selectedNote, value);
                OnNoteSelected(value);
            }
        }

        private async void OnAddNote(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewNotePage));
        }

        async void OnNoteSelected(Note Note)
        {
            if (Note == null)
                return;

            // This will push the NoteDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?{nameof(NoteDetailViewModel.NoteId)}={Note.Id}");
        }
    }
}