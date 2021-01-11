using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Text;
using System;
using System.Net.Http.Headers;
using System.Net;
using Akavache;
using System.Reactive.Linq;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using zakajo.Model;
using zakajo.Mobile.Services;

[assembly: Dependency(typeof(WebDataService))]
namespace zakajo.Mobile.Services
{
    public static class WebDataService
    {
        const string baseUrl = "http://zakajo.shuthuluwhiskeyroses.com/";
        //const string baseUrl = "http://localhost:59639/";
        const string apiPath = "api/";
        const string applicationName = "Zakajo";
        const string zakajoAuthHeader = "ZakajoAuth";
        const string tokenPrefix = "Bearer ";
        const string bearerTokenHeaderName = "Bearer";
        const string notConnectedMessage = "Your device does not appear to have an internet connection. Please check your connection and try again.";

        #region getRequests
       
        public static async Task<IEnumerable<NoteType>> GetNoteTypes()
        {
            var json = await GetStringDataWithCacheAsync("NoteType/", "noteTypes", DateTime.Now.AddDays(1), null);
            var noteTypes = NoteType.FromJson(json);
            return noteTypes;
        }

        public static async Task<IEnumerable<CommentType>> GetCommentTypes()
        {
            var json = await GetStringDataWithCacheAsync("CommentType/", "commentTypes", DateTime.Now.AddDays(1), null);
            var commentTypes = CommentType.FromJson(json);
            return commentTypes;
        }

        public static async Task<IEnumerable<LinkType>> GetLinkTypes()
        {
            var json = await GetStringDataWithCacheAsync("LinkType/", "linkTypes", DateTime.Now.AddDays(1), null);
            var linkTypes = LinkType.FromJson(json);
            return linkTypes;
        }

        private static async Task<string> GetStringDataWithCacheAsync(string apiSuffix, string keyName, DateTime cacheExpiration, UserToken token)
        {
            //if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
           // {
                BlobCache.ApplicationName = applicationName;
                string json;

                try
                {
                    json = await BlobCache.LocalMachine.GetObject<string>(keyName);
                }
                catch (KeyNotFoundException)
                {
                    json = await GetStringDataAsync(apiSuffix, token);

                    await BlobCache.LocalMachine.InsertObject<string>(keyName, json, cacheExpiration);
                }

                return json;
           // }
         //   else throw new Exception(notConnectedMessage);
        }

        private static async Task<string> GetStringDataAsync(string apiSuffix, UserToken token)
        {
        //    if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
        //    {
                HttpClient httpClient = new HttpClient();
                string json;

                if (token != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(bearerTokenHeaderName, token.AccessToken);
                }

                json = await httpClient.GetStringAsync(baseUrl + apiPath + apiSuffix);

                return json;
         //   }
         //   else throw new Exception(notConnectedMessage);
        }

        #endregion

        #region postRequests

        /*  public static async Task<DeviceToken> LoginDeviceAsync(LoginDeviceRequestDTO request)
          {
              if (String.IsNullOrWhiteSpace(request.DeviceId))
              {
                  KujengasSettings.DeviceId = request.DeviceId = await RegisterNewDeviceAsync();
              }

              string json = await PostDataAsync<LoginDeviceRequestDTO>(request, "Account/DeviceToken", null);
              var token = DeviceToken.FromJson(json);

              return token;
          }

          public static async Task<string> RegisterNewDeviceAsync()
          {
              //string did = await PostDataAsync<RateRequestDTO>(null, "Account/Device", null);
              string did = await PostDataAsync<string>(null, "Account/Device", null);
              return did.Replace("\"", "");
          }


          public static async Task<string> UpdateUserPlaylistSongsAsync(UpdateUserPlaylistRequestDTO request, DeviceToken token)
          {
              return await PostDataAsync<UpdateUserPlaylistRequestDTO>(request, "playlist/updateuserplaylist", token);
          }
  */

        public static async Task<IEnumerable<Note>> GetNotesByUserIdAsync(string request)
        {
            var data = await PostDataAsync<string>(request, "note/notes", null);
            var all = Note.FromJson(data);
            return all;
        }

        public static async Task<IEnumerable<Comment>> AddComment(Comment request)
        {
            var data = await PostDataAsync<Comment>(request, "note/AddComment", null);
            var all = Comment.FromJson(data);
            return all;
        }

        public static async Task<IEnumerable<Note>> CreateNote(Note request)
        {
            var data = await PostDataAsync<Note>(request, "note/", null);
            var all = Note.FromJson(data);
            return all;
        }

        public static async Task<IEnumerable<Note>> UpdateNote(Note request)
        {
            var data = await PutDataAsync<Note>(request, "note/", null);
            var all = Note.FromJson(data);
            return all;
        }

        private static async Task<string> PostDataAsync<T>(T request, string apiSuffix, UserToken token)
        {
           // if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
           // {
                HttpClient httpClient = new HttpClient();

                if (token != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(bearerTokenHeaderName, token.AccessToken);
                    httpClient.DefaultRequestHeaders.Add(zakajoAuthHeader, tokenPrefix + token.AccessToken);
                }

                var content = (request != null) ? new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json") : null;

                var data = await httpClient.PostAsync(baseUrl + apiPath + apiSuffix, content);
                var response = await data.Content.ReadAsStringAsync();
                return response;
         //   }
         //   else throw new Exception(notConnectedMessage);
        }

        private static async Task<string> PutDataAsync<T>(T request, string apiSuffix, UserToken token)
        {
            // if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            // {
            HttpClient httpClient = new HttpClient();

            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(bearerTokenHeaderName, token.AccessToken);
                httpClient.DefaultRequestHeaders.Add(zakajoAuthHeader, tokenPrefix + token.AccessToken);
            }

            var content = (request != null) ? new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json") : null;

            var data = await httpClient.PutAsync(baseUrl + apiPath + apiSuffix, content);
            var response = await data.Content.ReadAsStringAsync();
            return response;
            //   }
            //   else throw new Exception(notConnectedMessage);
        }


        #endregion
    }
}