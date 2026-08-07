namespace Addressbook1.Areas.Admin.ViewModels.Dashboard
{
    public record DashboardVM
    {
        public int TotalUsers { get; set; }
        public int TotalContacts { get; set; }
        public string TopCategoryName { get; set; }
        public int TopCategoryCount { get; set; }
    }
}
