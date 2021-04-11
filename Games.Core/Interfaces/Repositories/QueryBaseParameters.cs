namespace Games.Core.Interfaces.Repositories
{
    public abstract class QueryBaseParameters
    {
        protected QueryBaseParameters(int? size, int? pageIndex, string orderBy, bool? isDescending)
        {
            Size = size ?? 10;
            PageIndex = pageIndex ?? 1;
            OrderBy = orderBy;
            IsDescending = isDescending;
        }

        public int Size { get; }
        public int PageIndex { get; }
        public string OrderBy { get; }
        public bool? IsDescending { get; }
    }
}
