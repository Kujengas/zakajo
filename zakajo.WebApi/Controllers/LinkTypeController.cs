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
    public class LinkTypeController : ApiController
    {
        // GET: api/NoteType
        public IEnumerable<LinkType> Get()
        {
            return NotesData.GetLinkTypes().ToArray();
        }

        // GET: api/NoteType/5
        public LinkType Get(int id)
        {
            return NotesData.GetLinkTypes().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/NoteType
        public void Post([FromBody] string value)
        {
            NotesData.CreateLinkType(value);
        }

        // PUT: api/NoteType/5
        public void Put([FromBody] LinkType linkType)
        {
            NotesData.UpdateLinkTypes(linkType);
        }

        public void Delete([FromBody] LinkType linkType)
        {
            NotesData.DeleteLinkType(linkType);
        }

    }
}
