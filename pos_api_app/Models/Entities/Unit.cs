using pos_api_app.Models;
using pos_api_app.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models.Entities;

[Table("tb_m_unit")]
public class Unit : BaseEntity
{

	[Column("name", TypeName = "varchar(200)")]
	public string Name { get; set; } = string.Empty;

	//Cardinality
	public ICollection<Price>? Prices { get; set; }
}
