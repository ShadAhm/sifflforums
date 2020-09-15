export class CommentPost {
  public id: string;
  public text: string;
  public submissionId: string;
  public upvotes: number;
  public currentUserVoteWeight: number;
  public username: string;
}

export class Submission {
  public id: string;
  public title: string; 
  public text: string;
  public username: string;
  public comments: CommentPost[];
  public commentsCount: number; 
  public upvotes: number; 
  public currentUserVoteWeight: number;
  public forumSectionId: string; 
}
