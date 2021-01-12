using System;
  using System.Collections.Generic;
  using System.Globalization;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Converters;

namespace zakajo.Model
{
    public partial class Note
    {

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }

        [JsonProperty("CreationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("NoteType")]
        public int NoteType { get; set; }

        [JsonProperty("NoteTypeDescription")]
        public string NoteTypeDescription { get; set; }

        [JsonProperty("ImageFile")]
        public string ImageFile { get; set; }

        [JsonProperty("ImageThumb")]
        public string ImageThumb { get; set; }

        [JsonProperty("Comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        [JsonProperty("Links")]
        public List<Link> Links { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("UpdateUserGuid")]
        public string UpdateUserGuid { get; set; }

        [JsonProperty("LastUpdateDate")]
        public DateTime LastUpdateDate { get; set; }


    }

    public partial class Note
    {
        public static List<Note> FromJson(string json) => JsonConvert.DeserializeObject<List<Note>>(json, Converter.Settings);
    }

}