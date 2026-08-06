using System.ComponentModel.DataAnnotations;
using Addressbook1.Models.Base;

namespace Addressbook1.Models
{
    public class Contact : BaseEntity
    {
        [Required(ErrorMessage = "Ad daxil edilməlidir")]
        [StringLength(50, ErrorMessage = "Ad 50 simvoldan çox ola bilməz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad daxil edilməlidir")]
        [StringLength(50, ErrorMessage = "Soyad 50 simvoldan çox ola bilməz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Telefon nömrəsi daxil edilməlidir")]
        public string Phone { get; set; } // int yerinə string istifadə edirik

        [Required(ErrorMessage = "Kategoriya seçilməlidir")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public string? ImageUrl { get; set; }

        // İstifadəçiyə özəl kontaktlar üçün (Account ilə əlaqə)
        public string UserId { get; set; }
        public AppUser? User { get; set; }
    }
}