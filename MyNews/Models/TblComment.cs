using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyNews.Models;

public partial class TblComment
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    public string CommenterName { get; set; } = null!;

    public DateOnly DateSubmited { get; set; }

    [StringLength(512)]
    public string Body { get; set; } = null!;

    [Column("isValid")]
    public bool IsValid { get; set; }

    [Column("NewsID")]
    public int NewsId { get; set; }

    [ForeignKey("NewsId")]
    [InverseProperty("TblComment")]
    public virtual TblNews News { get; set; } = null!;
}
