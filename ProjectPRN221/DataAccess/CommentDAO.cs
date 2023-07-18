using Microsoft.EntityFrameworkCore;
using ProjectPRN221.BusinessObject3;
using System.ComponentModel.Design;

namespace ProjectPRN221.DataAccess
{
    public class CommentDAO
    {
        private static CommentDAO instance = null;
        private static readonly object instanceLock = new object();
        public static CommentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CommentDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Comment> GetCommentList()
        {
            List<Comment> listComment = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                listComment = context.Comments.Include(e => e.Account).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listComment;
        }

        public Comment GetCommentByID(int? commentID)
        {
            Comment comment = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                comment = context.Comments.SingleOrDefault(c => c.CommentId == commentID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return comment;
        }

        public void AddNew(Comment comment)
        {
            try
            {
                Comment commentFind = GetCommentByID(comment.CommentId);
                if (commentFind == null)
                {
                    using var context = new DatabaseTestProjectContext();
                    context.Comments.Add(comment);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The comment is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Comment comment)
        {
            try
            {
                Comment commentFind = GetCommentByID(comment.CommentId);
                if (commentFind != null)
                {
                    using var context = new DatabaseTestProjectContext();
                    context.Comments.Update(comment);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The comment does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int? commentID)
        {
            try
            {
                Comment commentFind = GetCommentByID(commentID);
                if (commentFind != null)
                {
                    using var context = new DatabaseTestProjectContext();
                    context.Comments.Remove(commentFind);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The comment does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        
    }
}
