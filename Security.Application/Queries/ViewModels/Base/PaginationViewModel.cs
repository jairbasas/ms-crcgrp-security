
namespace Security.Application.Queries.ViewModels.Base
{
    public class PaginationViewModel<T>
    {
        public PaginationViewModel(Pagination pagination, IEnumerable<T> items)
        {
            this.pagination = pagination;
            this.items = items;
        }

        public Pagination pagination { get; }
        public IEnumerable<T> items { get; }
    }
    public class PaginationRequest
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string? sort { get; set; }

        Pagination _pagination;
        public Pagination pagination
        {
            get
            {
                if (this._pagination == null)
                {
                    if (this.pageIndex > 0 && this.pageSize > 0)
                        this._pagination = new Pagination(this.pageIndex, this.pageSize, this.sort);
                    else
                        this._pagination = new Pagination(this.sort);
                }

                return this._pagination;
            }
            set
            {
                this._pagination = value;
            }
        }
    }
    public class Pagination
    {
        public Pagination(string sort)
        {
            this.sort = (sort == null) ? "" : sort;
        }

        public Pagination(int pageIndex, int pageSize, string sort)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.sort = (sort == null) ? "" : sort;
        }

        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public string sort { get; set; }

        private int _total = 0;
        public int total
        {
            get { return this._total; }
            set
            {
                if (this.pageSize > 0)
                    this.pageCount = (value / this.pageSize + ((value % this.pageSize == 0) ? 0 : 1));
                else
                    this.pageCount = 1;

                this._total = value;
            }
        }
    }
}
