using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace zakajo.Model
{
    public partial class Comment
    {
       
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("NoteId")]
        public int NoteId { get; set; }

        [JsonProperty("CommentType")]
        public int CommentType { get; set; }

        [JsonProperty("CommentTypDescription")]
        public int CommentTypeDescription { get; set; }

        [JsonProperty("CommentDate")]
        public DateTime CommentDate { get; set; }

        [JsonProperty("CommentText")]
        public string CommentText { get; set; }

        [JsonProperty("UpdateUserGuid")]
        public string UpdateUserGuid { get; set; }

        [JsonProperty("LastUpdateDate")]
        public DateTime LastUpdateDate { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }


    public partial class Comment
    {
        public static List<Comment> FromJson(string json) => JsonConvert.DeserializeObject<List<Comment>>(json, Converter.Settings);
    }
}
