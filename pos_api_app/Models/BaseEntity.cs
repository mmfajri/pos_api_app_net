using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models;

public abstract class BaseEntity
{
    [Key]
    [Column("guid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Guid { get; set; }
    [Column("created_time")]
    public DateTime? CreatedTime { get; set; }
    [Column("updated_time")]
    public DateTime? UpdatedTime { get; set; }
    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }
}
