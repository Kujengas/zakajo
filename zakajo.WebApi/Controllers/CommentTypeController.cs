using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using zakajo.DataAccess;
using zakajo.Model;

namespace zakajo.WebApi.Controllers
{
    public class CommentTypeController : ApiController
    {
        // GET: api/NoteType
        public IEnumerable<CommentType> Get()
        {
            return NotesData.GetCommentTypes().ToArray();
        }

        // GET: api/NoteType/5
        public CommentType Get(int id)
        {
            return NotesData.GetCommentTypes().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/NoteType
        public void Post([FromBody] string value)
        {
            NotesData.CreateCommentType(value);
        }

        // PUT: api/NoteType/5
        public void Put([FromBody] CommentType commentType)
        {
            NotesData.UpdateCommentType(commentType);
        }

        // DELETE: api/NoteType/5
        public void Delete([FromBody] CommentType commentType)
        {
            NotesData.DeleteCommentType(commentType);
        }
    }

}
