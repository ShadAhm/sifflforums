export class CommentPost {
  public commentId: number;
  public text: string;
  public commentThreadId: number; 
}

export class CommentThread {
  public commentThreadId: number;
  public title: string; 
  public text: string;
  public username: string; 
}
