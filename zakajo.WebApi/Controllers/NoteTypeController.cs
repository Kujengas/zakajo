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
    public class NoteTypeController : ApiController
    {
        // GET: api/NoteType
        public IEnumerable<NoteType> Get()
        {
            return NotesData.GetNoteTypes().ToArray();
        }

        // GET: api/NoteType/5
        public NoteType Get(int id)
        {
            return NotesData.GetNoteTypes().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/NoteType
        public void Post([FromBody]string value)
        {
            NotesData.CreateNoteType(value);
        }



        // PUT: api/NoteType/5
        public void Put([FromBody] NoteType noteType)
        {
            NotesData.UpdateNoteType(noteType);
        }

        // DELETE: api/NoteType/5
        public void Delete([FromBody] NoteType noteType)
        {
            NotesData.DeleteNoteType(noteType);
        }
    }
}
