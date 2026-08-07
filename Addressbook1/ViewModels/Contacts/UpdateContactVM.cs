using System.ComponentModel.DataAnnotations;

namespace Addressbook1.ViewModels.Contacts
{
    public record UpdateContactVM
    {
        [Required(ErrorMessage = "Ad daxil edilməlidir")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad daxil edilməlidir")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Telefon nömrəsi daxil edilməlidir")]
        public string Phone { get; set; }

        // Əgər kateqoriya mütləqdirsə, bunu da əlavə edirik
        public int CategoryId { get; set; }
        public int Id { get;  set; }
        public int UserId { get;  set; }
    }
}
