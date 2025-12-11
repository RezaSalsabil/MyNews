using System.ComponentModel.DataAnnotations;

namespace MyNews.Models.Dto.User
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }
        [Required]
        [MaxLength(12)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(32)]
        public string UserName { get; set; }


        public TblUser toUser()
        {
            return new TblUser
            {
                Name = Name,
                Password = Password,
                PhoneNumber = PhoneNumber,
                UserName = UserName,
                RoleId = 3
            };

        }
    }
}
