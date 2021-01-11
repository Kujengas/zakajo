using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using zakajo.Model;

namespace zakajo.WebApi.Controllers
{

    [RoutePrefix("api/note")]
    public class NoteController : ApiController
    {
        [HttpPost]
        [Route("notes")]
        public IEnumerable<Note> Notes([FromBody] string user)
        {

           // string user = "22c9e6dd-2b43-479f-a3a1-0f465e81aed3";
            var notes = DataAccess.NotesData.GetNotes(user);

            /*
              foreach (Note n in notes)
            {

                n.Comments = DataAccess.CommentsData.GetComments(n.Id)

            }*/

            return notes.ToArray();

        }

        [HttpPost]
        //[Route("CreateNote")]
        public IEnumerable<Note> CreateNote([FromBody] Note note)
        {

            DataAccess.NotesData.CreateNote(note);

            return DataAccess.NotesData.GetNotes(note.UpdateUserGuid).ToArray();
        }


        [HttpPost]
        [Route("AddComment")]
        public IEnumerable<Comment> AddComment([FromBody] Comment comment)
        {

            DataAccess.NotesData.CreateComment(comment);

            return DataAccess.NotesData.GetComments(comment.NoteId).ToArray();
        }

        [HttpPut]
        //[Route("UpdateNote")]
        public IEnumerable<Note> UpdateNote([FromBody] Note note)
        {

            DataAccess.NotesData.UpdateNote(note);

            return DataAccess.NotesData.GetNotes(note.UpdateUserGuid).ToArray();
        }

    }
}