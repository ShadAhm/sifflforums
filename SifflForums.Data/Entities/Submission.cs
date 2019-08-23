using SifflForums.Data.Interfaces;
using System.Collections.Generic;

namespace SifflForums.Data.Entities
{
    public class Submission : EntityBase, IUpvotable
    {
        public int SubmissionId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int VotingBoxId { get; set; }
        public VotingBox VotingBox { get; set; }
        public List<Comment> Comments { get; set; }
        public int ForumSectionId { get; set; }
        public ForumSection ForumSection { get; set; }
    }
}
