using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyNews.Models;

public partial class TblRole
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(16)]
    public string Name { get; set; } = null!;

    [StringLength(16)]
    public string Title { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<TblUser> TblUser { get; set; } = new List<TblUser>();
}
