using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyNews.Models;

public partial class TblCategory
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(32)]
    public string Name { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<TblNews> TblNews { get; set; } = new List<TblNews>();
}
