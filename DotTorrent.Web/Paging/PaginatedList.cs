using System.Collections.Generic;

namespace DotTorrent.Web.Paging
{
  public class PaginatedList<T> : List<T>
  {
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
  }
}
