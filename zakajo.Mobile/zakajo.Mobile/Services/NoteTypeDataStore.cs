using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zakajo.Mobile.Models;
using zakajo.Model;

namespace zakajo.Mobile.Services
{
    public class NoteTypeDataStore : IDataStore<NoteType>
    {
        readonly List<NoteType> items;

        public NoteTypeDataStore()
        {
            items = new List<NoteType>();
             //items = GetItemsAsync().Result.ToList();
        }

        public async Task<bool> AddItemAsync(NoteType item)
        {

           // items.Clear();
           // items.AddRange(await WebDataService.CreateNoteType(item));

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(NoteType item)
        {
            
            //items.Clear();
            //items.AddRange(await WebDataService.UpdateNoteType(item));

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
           // var oldItem = items.Where((Note arg) => arg.Id == id).FirstOrDefault();
           // items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<NoteType> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(i => i.Id == id));
        }

        public async Task<IEnumerable<NoteType>> GetItemsAsync(bool forceRefresh = false)
        {
            items.Clear();
            items.AddRange(await WebDataService.GetNoteTypes());
            return items;
        }
    }
}