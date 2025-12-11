using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyNews.Models;

public partial class TblUser
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [StringLength(32)]
    public string Name { get; set; }
    [StringLength(32)]
    public string Password { get; set; }
    [StringLength(12)]
    public string PhoneNumber { get; set; }
    [Column("RoleID")]
    public int RoleId { get; set; }
    [Column("userName")]
    [StringLength(32)]
    public string UserName { get; set; }
    [ForeignKey("RoleId")]
    [InverseProperty("TblUser")]
    public virtual TblRole Role { get; set; } = null!;
}
