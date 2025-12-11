using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace MyNews.Models.Dto.User
{
    public class LoginDto
    {
        [Required]
        [MaxLength(32)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

    

    }
}
