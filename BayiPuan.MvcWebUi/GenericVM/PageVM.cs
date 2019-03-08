namespace BayiPuan.MvcWebUi.GenericVM
{
    public class PageVM
    {
        public int ActivePage { get; set; }
        public int PageCount { get; }
        public int TotalRowCount { get; set; }
        public int PageSize { get; set; }
        public GenericVM SearchVM { get; set; }
    }
}