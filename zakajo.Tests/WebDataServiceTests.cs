using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Diagnostics;
using System;
using zakajo.Utilities.Cryptography;
using System.Linq;
using zakajo.Mobile;
using zakajo.Mobile.Services;
using zakajo.Model;

namespace zakajo.Tests
{
    [TestClass]
    public class WebDataServiceTests
    {


        [TestMethod]
        public async Task GetNoteTypesAsyncTest()
        {
            try
            {
                Trace.Write(" ");
                var noteTypes = await WebDataService.GetNoteTypes();
                foreach (var nt in noteTypes)
                    Trace.Write(nt.Id + " - " + nt.Title + " - " + nt.CreatedDate);
            }
            catch (Exception ex)
            {
                string output = $"Unable to get noteTypes: { ex.Message}";
                Trace.Write(output);
                Assert.Fail(output);
            }

        }

        [TestMethod]
        public async Task GetCommentTypesAsyncTest()
        {
            try
            {
                Trace.Write(" ");
                var commentTypes = await WebDataService.GetCommentTypes();
                foreach (var ct in commentTypes)
                    Trace.Write(ct.Id + " - " + ct.Title + " - " + ct.CreatedDate);
            }
            catch (Exception ex)
            {
                string output = $"Unable to get commentTypes: { ex.Message}";
                Trace.Write(output);
                Assert.Fail(output);
            }

        }

        [TestMethod]
        public async Task GetLinkTypesAsyncTest()
        {
            try
            {
                Trace.Write(" ");
                var linkTypes = await WebDataService.GetLinkTypes();
                foreach (var lt in linkTypes)
                    Trace.Write(lt.Id + " - " + lt.Title + " - " + lt.CreatedDate);
            }
            catch (Exception ex)
            {
                string output = $"Unable to get linkTypes: { ex.Message}";
                Trace.Write(output);
                Assert.Fail(output);
            }
        }

        [TestMethod]
        public async Task GetNotesByUserIdAsyncTest()
        {
            try
            {
                Trace.Write(" ");
                var notes = await WebDataService.GetNotesByUserIdAsync("22c9e6dd-2b43-479f-a3a1-0f465e81aed3");
                foreach (var note in notes)
                {
                    Trace.WriteLine("Id: " + note.Id);
                    Trace.WriteLine("Title: " + note.Title);
                    Trace.WriteLine("Content: " + note.Content);
                    Trace.WriteLine("Comments: ");
                    foreach (var comment in note.Comments)
                    {
                        Trace.WriteLine("------ " + comment.CommentText);
                    }
                    Trace.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                string output = $"Unable to get notes: { ex.Message}";
                Trace.Write(output);
                Assert.Fail(output);
            }
        }

            [TestMethod]
            public async Task CreateNoteAsyncTest()
            {
                try
                {
                    Trace.Write(" ");
                var request = new Note
                {
                    NoteType = 1,
                    Content = Guid.NewGuid().ToString() +"  "+Environment.NewLine+ Guid.NewGuid().ToString() + "  " + Environment.NewLine + Guid.NewGuid().ToString() + "  " ,
                    UpdateUserGuid = "22c9e6dd-2b43-479f-a3a1-0f465e81aed3",
                    Title = "Note" + "Test Note " + DateTime.Now,
                    ImageFile=string.Empty,
                    ImageThumb = string.Empty
                };

                    var notes = await WebDataService.CreateNote(request);
                    foreach (var note in notes)
                    {
                        Trace.WriteLine("Id: " + note.Id);
                        Trace.WriteLine("Title: " + note.Title);
                        Trace.WriteLine("Content: " + note.Content);
                        Trace.WriteLine("Comments: ");
                        foreach (var comment in note.Comments)
                        {
                            Trace.WriteLine("------ " + comment.CommentText);
                        }
                        Trace.WriteLine("");
                    }
                }
                catch (Exception ex)
                {
                    string output = $"Unable to get notes: { ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }
            }

            [TestMethod]
            public async Task AddCommentAsyncTest()
            {
                try
                {
                    Trace.Write(" ");
                    var request = new Comment
                    {
                        CommentText = "Test comment " + DateTime.Now + " " + Guid.NewGuid().ToString(),
                        UpdateUserGuid = "22c9e6dd-2b43-479f-a3a1-0f465e81aed3",
                        NoteId = 6,
                        CommentType = 1

                    };
                    var comments = await WebDataService.AddComment(request);
                    foreach (var note in comments)
                    {
                        foreach (var comment in comments)
                        {
                            Trace.WriteLine("------ " + comment.CommentText);
                        }
                        Trace.WriteLine("");
                    }
                }
                catch (Exception ex)
                {
                    string output = $"Unable to get notes: { ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }

            }



            /*
            [TestMethod]
            public async Task GetUserPlayListsAsyncTest()
            {
                try
                {
                    var token = await WebDataService.LoginDeviceAsync(new LoginDeviceRequestDTO { DeviceId = TripleDes.Encrypt("a226ed6d-e512-4de1-9bae-5c0337d5a208") });

                    var stations = await WebDataService.LoadUserPlaylistsAsync(token);
                    foreach (var station in stations)
                        Trace.Write(station.Id + " - " + station.Name);
                }
                catch (Exception ex)
                {
                    string output = $"Unable to get songs: { ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }
            }

            [TestMethod]
            public async Task GetAlbumSongsAsyncTest()
            {
                try
                {
                    var songs = await WebDataService.GetAlbumSongsAsync(34343);
                    foreach (var song in songs)
                        Trace.Write(song.Sid + " - " + song.Title + " - " + song.Mp3);
                }
                catch (Exception ex)
                {
                    string output = $"Unable to get songs: { ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }

            }

            [TestMethod]
            public async Task GetStationsAsyncTest()
            {
                try
                {
                    var stations = await WebDataService.GetStationsAsync();
                    foreach (var station in stations)
                        Trace.WriteLine(station.Id + " - " + station.Name);
                }
                catch (Exception ex)
                {

                    string output = $"Unable to get stations: { ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }

            }

            [TestMethod]
            public async Task LoginDeviceAsyncTest()
            {
                try
                {
                    // var response = await WebDataService.LoginDeviceAsync(TripleDes.Encrypt("a226ed6d-e512-4de1-9bae-5c0337d5a208"));
                    // var response = await WebDataService.LoginDeviceAsync("AGViLT40oP9YvBbrGZ1lVhwwtwkoeLXjUiiyVAH73M / yg2UDFnUMBQ ==");
                    var response = await WebDataService.LoginDeviceAsync(new LoginDeviceRequestDTO { DeviceId = TripleDes.Encrypt("a226ed6d-e512-4de1-9bae-5c0337d5a208") });

                    Trace.WriteLine("Access Token: " + response.AccessToken);
                    Trace.WriteLine("UserName: " + response.UserName);
                }
                catch (Exception ex)
                {
                    string output = $"Unable to Register Device: {ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }
            }

            [TestMethod]
            public async Task RateAsyncTest()
            {
                try
                {
                    var token = await WebDataService.LoginDeviceAsync(new LoginDeviceRequestDTO { DeviceId = TripleDes.Encrypt("a226ed6d-e512-4de1-9bae-5c0337d5a208") });


                    var rating = new RateRequestDTO { IsThumbsUp = true, SongId = 34343, StationId = 2 };
                    var response = await WebDataService.RateSongAsync(rating, token);

                    Trace.WriteLine("UserName: " + response);
                }
                catch (Exception ex)
                {
                    string output = $"Unable to Register Device: {ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }
            }

            [TestMethod]
            public async Task CreateUserPlaylistAsyncTest()
            {
                try
                {
                    var token = await WebDataService.LoginDeviceAsync(new LoginDeviceRequestDTO { DeviceId = TripleDes.Encrypt("a226ed6d-e512-4de1-9bae-5c0337d5a208") });


                    //var rating = new RateRequest { IsThumbsUp = true, SongId = 34343, StationId = 2 };
                    var response = await WebDataService.CreateUserPlaylistAsync(new CreateUserPlaylistRequestDTO { PlaylistName = "Pls - " + DateTime.Now.ToString() }, token);

                    Trace.WriteLine(response);
                }
                catch (Exception ex)
                {
                    string output = $"Unable to Register Device: {ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }
            }

            [TestMethod]
            public async Task CopySongToStationAsyncTest()
            {
                try
                {
                    var token = await WebDataService.LoginDeviceAsync(new LoginDeviceRequestDTO { DeviceId = TripleDes.Encrypt("a226ed6d-e512-4de1-9bae-5c0337d5a208") });

                    var updateobj = new StationUpdateRequestDTO { EditCommand = "copy", EntityType = "song", SongId = 34343, FromStationId = 2, ToStationId = 2087 };
                    var response = await WebDataService.UpdateStationAsync(updateobj, token);

                    Trace.WriteLine(response);
                }
                catch (Exception ex)
                {
                    string output = $"Unable to Register Device: {ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }
            }

            [TestMethod]
            public async Task DeleteSongFromStationAsyncTest()
            {
                try
                {
                    var token = await WebDataService.LoginDeviceAsync(new LoginDeviceRequestDTO { DeviceId = TripleDes.Encrypt("a226ed6d-e512-4de1-9bae-5c0337d5a208") });


                    var updateobj = new StationUpdateRequestDTO { EditCommand = "delete", EntityType = "song", SongId = 34343, FromStationId = 2087 };
                    var response = await WebDataService.UpdateStationAsync(updateobj, token);

                    Trace.WriteLine(response);
                }
                catch (Exception ex)
                {
                    string output = $"Unable to Register Device: {ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }
            }

            [TestMethod]
            public async Task RegisterNewDeviceAsyncTest()
            {
                try
                {
                    var response = await WebDataService.RegisterNewDeviceAsync();

                    Trace.WriteLine("Response: " + response);

                }
                catch (Exception ex)
                {
                    string output = $"Unable to Register Device: {ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }

            }

            [TestMethod]
            public async Task GetRssFeedAsyncTest()
            {
                try
                {
                    var feedUri = "https://feed.podbean.com/thedormtainmentpodcast/feed.xml";
                    feedUri = "https://www.talkshoe.com/rss-brotherpanics-community-call.xml";
                    feedUri = "http://www.blogtalkradio.com/starrdom100/podcast";
                    feedUri = "https://podcasts.apple.com/us/podcast/the-anjunadeep-edition/id879507964";
                    feedUri = "http://ugbradi1.w26.wh-2.com/hsapi/api/feed/test";
                    feedUri = "http://ugbradio.com/hsapi/api/feed/podcasts";

                    var response = await WebDataService.GetFeaturedPodcastsAsync();


                    var response2 = await WebDataService.GetRssFeedAsync(response.ToList()[0]);
                }
                catch (Exception ex)
                {
                    string output = $"Unable to Register Device: {ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }
            }

            [TestMethod]
            public async Task GetFeaturedVideosTest()
            {
                try
                {
                    var response = await WebDataService.GetFeaturedVideosAsync();
                }
                catch (Exception ex)
                {
                    string output = $"Unable to Register Device: {ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }
            }

            [TestMethod]
            public async Task GetFeaturedPodcasts()
            {
                try
                {
                    var response = await WebDataService.GetFeaturedPodcastsAsync();
                }
                catch (Exception ex)
                {
                    string output = $"Unable to Register Device: {ex.Message}";
                    Trace.Write(output);
                    Assert.Fail(output);
                }
            }*/
        }
    }
