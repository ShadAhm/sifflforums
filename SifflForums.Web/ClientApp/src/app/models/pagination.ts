export class PaginatedResult<T> {
  public pageIndex: number;
  public totalPages: number;
  public results: T; 
}
