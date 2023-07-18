using ProjectPRN221.BusinessObject3;

namespace ProjectPRN221.Repository
{
    public interface ICommentRepository
    {
        List<Comment> GetAllComments();
        Comment GetCommentById(int? id);
        void DeleteComment(int? id);
        void UpdateComment(Comment comment);
        void InsertComment(Comment comment);
    }
}
