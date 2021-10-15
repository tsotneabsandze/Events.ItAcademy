namespace CORE.Specifications
{
    public class SpecParams
    {
        private const int MaxPageSize = 10;
        private int _pageSize = 4;
        
        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}