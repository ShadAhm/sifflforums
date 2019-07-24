export class CommentPost {
  public commentId: number;
  public text: string;
  public submissionId: number; 
}

export class Submission {
  public submissionId: number;
  public title: string; 
  public text: string;
  public username: string;
  public comments: CommentPost[];
  public upvotes: number; 
  public currentUserVoteWeight: number;
  public votingBoxId: number; 
}
