using Addressbook1.Models.Base;

namespace Addressbook1.Models
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Phone { get; set; }
        
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
