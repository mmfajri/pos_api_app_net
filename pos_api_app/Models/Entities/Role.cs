using pos_api_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models.Entities;

[Table("tb_m_role")]
public class Role : BaseEntity
{
    [Column("name", TypeName = "varchar(50)")]
    public string Name { get; set; } = string.Empty;

    //Cardinality
    public ICollection<Account>? Accounts { get; set; }
}
