using ProjectPRN221.BusinessObject3;
using ProjectPRN221.DataAccess;

namespace ProjectPRN221.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public void DeleteComment(int? id) => CommentDAO.Instance.Remove(id);


        public List<Comment> GetAllComments() => CommentDAO.Instance.GetCommentList();

        public Comment GetCommentById(int? id) => CommentDAO.Instance.GetCommentByID(id);

        public void InsertComment(Comment comment) => CommentDAO.Instance.AddNew(comment);

        public void UpdateComment(Comment comment) => CommentDAO.Instance.Update(comment);
    }
}
