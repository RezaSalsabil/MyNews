using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyNews.Models;

public partial class TblKeyword
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [ForeignKey("KeywordId")]
    [InverseProperty("Keyword")]
    public virtual ICollection<TblNews> News { get; set; } = new List<TblNews>();
}
