using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zakajo.Mobile.Models;
using zakajo.Model;

namespace zakajo.Mobile.Services
{
    public class CommentDataStore : IDataStore<Comment>
    {
        readonly List<Comment> items;

        public CommentDataStore()
        {
            items = new List<Comment>();
             //items = GetItemsAsync().Result.ToList();
        }

        public async Task<bool> AddItemAsync(Comment item)
        {

            items.Clear();
            items.AddRange(await WebDataService.AddComment(item));

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Comment item)
        {

            //items.Clear();
            // items.AddRange(await WebDataService.UpdateComment(item));

            //return await Task.FromResult(true);

            return true;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Comment arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Comment> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(i => i.Id == id));
        }

        public async Task<IEnumerable<Comment>> GetItemsAsync(bool forceRefresh = false)
        {
            var app =  zakajo.Mobile.App.Current as zakajo.Mobile.App;
            var userguid = app.DeviceId;
            //items.Clear();
            //items.AddRange(await WebDataService.GetComments(userguid));
            return items;
        }
    }
}