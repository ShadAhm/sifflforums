using SifflForums.Data.Interfaces;

namespace SifflForums.Data.Entities
{
    public class Comment : AuditableEntityBase, IUpvotableEntity
    {
        public string Text { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int SubmissionId { get; set; }
        public string VotingBoxId { get; set; }
        public VotingBox VotingBox { get; set; }
        public Submission Submission { get; set; }
    }
}
