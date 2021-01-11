using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace zakajo.Model
{
    public partial class NoteType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Status { get; set; }

    }

    public partial class NoteType
    {
        public static List<NoteType> FromJson(string json) => JsonConvert.DeserializeObject<List<NoteType>>(json, Converter.Settings);
    }

}
