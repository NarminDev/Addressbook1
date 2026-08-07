namespace Addressbook1.Areas.Admin.ViewModels.Dashboard
{
    public record CategoryChartItem
    {
        public string CategoryName { get; set; } = string.Empty;
        public int ContactCount { get; set; }
    }

    public record MonthlyUserChartItem
    {
        public string MonthName { get; set; } = string.Empty; // Məsələn: "Mart", "Aprel"
        public int UserCount { get; set; }
    }
    public record DashboardVM
    {
        public int TotalUsers { get; set; }
        public int TotalContacts { get; set; }
        public string TopCategoryName { get; set; } = string.Empty;
        public int TopCategoryCount { get; set; }

        // Dinamik diaqram üçün list
        public List<CategoryChartItem> CategoryChartData { get; set; } = new List<CategoryChartItem>();

        public List<MonthlyUserChartItem> MonthlyUserChartData { get; set; } = new List<MonthlyUserChartItem>();
    }
}
