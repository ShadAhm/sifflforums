export class CommentPost {
  public commentId: number;
  public text: string;
  public submissionId: number;
  public upvotes: number;
  public currentUserVoteWeight: number;
  public username: string;
}

export class Submission {
  public submissionId: number;
  public title: string; 
  public text: string;
  public username: string;
  public comments: CommentPost[];
  public commentsCount: number; 
  public upvotes: number; 
  public currentUserVoteWeight: number;
  public forumSectionId: number; 
}
