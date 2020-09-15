using SifflForums.Data.Interfaces;
using System.Collections.Generic;

namespace SifflForums.Data.Entities
{
    public class Submission : AuditableEntityBase, IUpvotableEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string VotingBoxId { get; set; }
        public VotingBox VotingBox { get; set; }
        public List<Comment> Comments { get; set; }
        public int ForumSectionId { get; set; }
        public ForumSection ForumSection { get; set; }
        public int Version { get; set; }
    }
}
