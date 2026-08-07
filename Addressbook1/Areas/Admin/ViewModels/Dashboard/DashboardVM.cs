namespace Addressbook1.Areas.Admin.ViewModels.Dashboard
{
    public record CategoryChartItem
    {
        public string CategoryName { get; set; } = string.Empty;
        public int ContactCount { get; set; }
    }

    public record DashboardVM
    {
        public int TotalUsers { get; set; }
        public int TotalContacts { get; set; }
        public string TopCategoryName { get; set; } = string.Empty;
        public int TopCategoryCount { get; set; }

        // Dinamik diaqram üçün list
        public List<CategoryChartItem> CategoryChartData { get; set; } = new List<CategoryChartItem>();
    }
}
