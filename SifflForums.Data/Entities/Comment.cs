using SifflForums.Data.Interfaces;

namespace SifflForums.Data.Entities
{
    public class Comment : EntityBase, IUpvotable
    {
        public int CommentId { get; set;}
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int SubmissionId { get; set; }
        public int VotingBoxId { get; set; }
        public VotingBox VotingBox { get; set; }
        public Submission Submission { get; set; }
    }
}
