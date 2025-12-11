using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyNews.Models;

public partial class TblNews
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public DateOnly DatePublished { get; set; }

    [Column("isPublished")]
    public bool IsPublished { get; set; }

    public string Body { get; set; } = null!;

    [StringLength(255)]
    public string Image { get; set; } = null!;

    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [Column("isSlide")]
    public bool IsSlide { get; set; }

    [Column("viewCount")]
    public int ViewCount { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("TblNews")]
    public virtual TblCategory Category { get; set; } = null!;

    [InverseProperty("News")]
    public virtual ICollection<TblComment> TblComment { get; set; } = new List<TblComment>();

    [ForeignKey("NewsId")]
    [InverseProperty("News")]
    public virtual ICollection<TblKeyword> Keyword { get; set; } = new List<TblKeyword>();
}
