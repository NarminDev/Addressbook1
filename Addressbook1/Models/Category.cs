using Addressbook1.Models.Base;

namespace Addressbook1.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
