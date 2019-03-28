using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEvernote.Entities
{
    [Table("EvernoteUsers")]
    public class EvernoteUser : MyEntityBase
    {
        [DisplayName("isim"),
         StringLength(25, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"),
         StringLength(25, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"), 
         Required(ErrorMessage = "{0} alanı gereklidir."), 
         StringLength(25, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("E-posta"), 
         Required(ErrorMessage = "{0} alanı gereklidir."), 
         StringLength(70, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Şifre"), 
         Required(ErrorMessage = "{0} alanı gereklidir."), 
         StringLength(25, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        public string Password { get; set; }

        [StringLength(30)]// images/user_12.jpg
        public string ProfileImageFilename { get; set; }

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        [Required]
        public Guid ActivateGuid { get; set; }



        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }


    }
}
