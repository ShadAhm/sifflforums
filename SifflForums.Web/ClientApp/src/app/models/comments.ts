export class CommentPost {
  public id: string;
  public text: string;
  public submissionId: string;
  public upvotes: number;
  public currentUserVoteWeight: number;
  public username: string;
  public createdAtUtc: string;
}

export class Submission {
  public id: string;
  public submissionId: string;
  public title: string; 
  public text: string;
  public username: string;
  public comments: CommentPost[];
  public commentsCount: number; 
  public upvotes: number; 
  public currentUserVoteWeight: number;
  public forumSectionId: string; 
  public createdAtUtc: string;
}
