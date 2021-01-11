using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zakajo.Mobile.Models;
using zakajo.Model;

namespace zakajo.Mobile.Services
{
    public class NoteDataStore : IDataStore<Note>
    {
        readonly List<Note> items;

        public NoteDataStore()
        {
            items = new List<Note>();
             //items = GetItemsAsync().Result.ToList();
        }

        public async Task<bool> AddItemAsync(Note item)
        {

            items.Clear();
            items.AddRange(await WebDataService.CreateNote(item));

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Note item)
        {
            
            items.Clear();
            items.AddRange(await WebDataService.UpdateNote(item));

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Note arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Note> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(i => i.Id == id));
        }

        public async Task<IEnumerable<Note>> GetItemsAsync(bool forceRefresh = false)
        {
            var app =  zakajo.Mobile.App.Current as zakajo.Mobile.App;
            var userguid = app.DeviceId;
            items.Clear();
            items.AddRange(await WebDataService.GetNotesByUserIdAsync(userguid));
            return items;
        }

        public async Task<bool> AddCommentAsync(Comment comment)
        {
            await WebDataService.AddComment(comment);
            items.Clear();
            items.AddRange(await GetItemsAsync());
            return await Task.FromResult(true);
        }



    }
}