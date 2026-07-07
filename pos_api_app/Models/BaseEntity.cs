using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models;

public abstract class BaseEntity
{
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[Column("created_time")]
	public DateTime? CreatedTime { get; set; }
	[Column("updated_time")]
	public DateTime? UpdatedTime { get; set; }
	[Column("is_deleted")]
	public bool? IsDeleted { get; set; }
}
