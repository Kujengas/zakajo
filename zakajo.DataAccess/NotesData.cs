using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using zakajo.Model;

namespace zakajo.DataAccess
{

    public class NotesData
    {

        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "",
                UserID = "",
                Password = "",
                InitialCatalog = ""
            };

            return builder.ConnectionString;
        }

        /* Note Methods */
        public static List<Note> GetNotes(string user) {

            //user = "22c9e6dd-2b43-479f-a3a1-0f465e81aed3";

            var notes = new List<Note>();
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("GetNotes", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@UpdateUserGuid", user));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            notes = (from DataRow row in dt.Rows
                     select new Note
                     {
                         Id = Convert.ToInt32(row["Id"]),
                         Content = row["Content"].ToString(),
                         Title = row["Title"].ToString(),
                         CreationDate = Convert.ToDateTime(row["CreationDate"]),
                         ImageFile = row["ImageFile"].ToString(),
                         ImageThumb = row["ImageThumb"].ToString(),
                         UpdateUserGuid = row["UpdateUserGuid"].ToString(),
                         LastUpdateDate = Convert.ToDateTime(row["LastUpdateDate"]),
                         NoteType = Convert.ToInt32(row["NoteType"]),
                         NoteTypeDescription = row["NoteTypeTitle"].ToString(),
                         Status = row["Status"].ToString(),
                         //Comments = new List<Comment>(),
                     }).ToList();

           // notes.ForEach(n => n.Comments.AddRange(GetComments(n.Id)));
            notes.ForEach(n => n.Comments = GetComments(n.Id));

            return notes;
        }

        public static int CreateNote(Note note) {

            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("CreateNote", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@Title", note.Title));
                cmd.Parameters.Add(new SqlParameter("@Content", note.Content));
                cmd.Parameters.Add(new SqlParameter("@NoteTypeId", note.NoteType));
                cmd.Parameters.Add(new SqlParameter("@ImageFile", note.ImageFile));
                cmd.Parameters.Add(new SqlParameter("@ImageThumb", note.ImageThumb));
                cmd.Parameters.Add(new SqlParameter("@UserGuid", note.UpdateUserGuid));
                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        public static int UpdateNote(Note note)
        {
            /*
            
            EXECUTE @RC = [dbo].[UpdateNote]
               @NoteId = 2
              ,@Title = " Test Note Updated"
              ,@Content = "This is the updated version of the second note"
              ,@NoteTypeId = 2
              ,@ImageFile = ''
              ,@ImageThumb = ''
            GO
            */

            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("UpdateNote", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@NoteId", note.Id));
                cmd.Parameters.Add(new SqlParameter("@UpdateUserGuid", note.UpdateUserGuid));
                cmd.Parameters.Add(new SqlParameter("@Title", note.Title));
                cmd.Parameters.Add(new SqlParameter("@Content", note.Content));
                cmd.Parameters.Add(new SqlParameter("@NoteTypeId", note.NoteType));
                cmd.Parameters.Add(new SqlParameter("@ImageFile", note.ImageFile));
                cmd.Parameters.Add(new SqlParameter("@ImageThumb", note.ImageThumb));
                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        public static int CreateComment(Comment comment) {

            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("CreateComment", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@Content", comment.CommentText));
                cmd.Parameters.Add(new SqlParameter("@NoteId", comment.NoteId));
                cmd.Parameters.Add(new SqlParameter("@CommentType", comment.CommentType));
                cmd.Parameters.Add(new SqlParameter("@UserGuid", comment.UpdateUserGuid));
                result = cmd.ExecuteNonQuery();

            }
            return result;

        }

        public static List<Comment>GetComments(int noteId){
      
            var comments = new List<Comment>();
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("GetComments", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@NoteId", noteId));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            comments = (from DataRow row in dt.Rows
                         select new Comment
                         {
                             Id = Convert.ToInt32(row["Id"]),
                             CommentText = row["Content"].ToString(),
                             CommentDate = Convert.ToDateTime(row["CreationDate"]),
                             UpdateUserGuid = row["UpdateUserGuid"].ToString(),
                             LastUpdateDate = Convert.ToDateTime(row["LastUpdateDate"]),
                             CommentType = Convert.ToInt32(row["CommentType"]),
                             NoteId = Convert.ToInt32(row["NoteId"]),
                             //CommentTypeDescription = row["NoteTypeTitle"].ToString(),
                             Status = row["Status"].ToString(),
                         }).ToList();

            return comments;
        }

        /* NoteType Methods */
        public static List<NoteType> GetNoteTypes() {

            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("GetNoteTypes", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();

                //cmd.Parameters.Add(new SqlParameter("@UpdateUserGuid", user));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            var noteType = (from DataRow row in dt.Rows
                         select new NoteType
                         {               
                             Id = Convert.ToInt32(row["Id"]),
                             Title = row["Title"].ToString(),
                             CreatedDate = Convert.ToDateTime(row["CreationDate"]),                        
                             LastUpdateDate = Convert.ToDateTime(row["CreationDate"])       
                             //Status = row["Status"].ToString()
                         }).ToList();


            return noteType;

        }

        public static int CreateNoteType(string title) {

            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("CreateNoteType", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();
             
                cmd.Parameters.Add(new SqlParameter("@Title", title));
                
                result = cmd.ExecuteNonQuery();

            }
            return result;

        }

        public static int UpdateNoteType(NoteType noteType) {

            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("UpdateNoteType", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@Id", noteType.Id));
                cmd.Parameters.Add(new SqlParameter("@Title", noteType.Title));

                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        public static int DeleteNoteType(NoteType noteType) {

            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("DeleteNoteType", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@Id", noteType.Id));

                result = cmd.ExecuteNonQuery();

            }
            return result;

        }

        /* CommentType Methods */
        public static int UpdateCommentType(CommentType commentType) {
            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("UpdateCommentType", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@Id", commentType.Id));
                cmd.Parameters.Add(new SqlParameter("@Title", commentType.Title));

                result = cmd.ExecuteNonQuery();

            }
            return result;
        }
   
        public static int CreateCommentType(string title) {
            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("CreateCommentType", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@Title", title));

                result = cmd.ExecuteNonQuery();

            }
            return result;

        }
    
        public static List<CommentType> GetCommentTypes() {

            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("GetCommentTypes", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();

                //cmd.Parameters.Add(new SqlParameter("@UpdateUserGuid", user));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            var commentTypes = (from DataRow row in dt.Rows
                            select new CommentType
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                Title = row["Title"].ToString(),
                                CreatedDate = Convert.ToDateTime(row["CreationDate"]),
                                LastUpdateDate = Convert.ToDateTime(row["CreationDate"])
                             //   Status = row["Status"].ToString()
                            }).ToList();


            return commentTypes;


        }

        public static int DeleteCommentType(CommentType commentType) {

            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("DeleteCommentType", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@Id", commentType.Id));

                result = cmd.ExecuteNonQuery();

            }
            return result;

        }

        /* LinkType Methods */
        public static int  CreateLinkType(string title) {

            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("CreateLinkType", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@Title", title));

                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        public static List<LinkType> GetLinkTypes() {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("GetLinkTypes", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();

                //cmd.Parameters.Add(new SqlParameter("@UpdateUserGuid", user));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            var linkTypes = (from DataRow row in dt.Rows
                                select new LinkType
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    Title = row["Title"].ToString(),
                                    CreatedDate = Convert.ToDateTime(row["CreationDate"]),
                                    LastUpdateDate = Convert.ToDateTime(row["CreationDate"])
                                  //  Status = row["Status"].ToString()
                                }).ToList();


            return linkTypes;
        }

        public static int DeleteLinkType(LinkType linkType) {
            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("DeleteLinkType", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@Id", linkType.Id));

                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        public static int UpdateLinkTypes(LinkType linkType) {

            int result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand("UpdateLinkType", conn) { CommandType = CommandType.StoredProcedure })
            {

                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@Id", linkType.Id));
                cmd.Parameters.Add(new SqlParameter("@Title", linkType.Title));

                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

    }
}


