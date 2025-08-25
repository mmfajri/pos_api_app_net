using pos_api_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models.Entities;

[Table("tb_m_account")]
public class Account : BaseEntity
{
    [Column("username", TypeName = "varchar(100)")]
    public string UserName { get; set; } = string.Empty;

    [Column("password", TypeName = "varchar(200)")]
    public string Password { get; set; } = string.Empty;

    [Column("role_id")]
    public int? RoleId { get; set; }


    //Cardinality
    public Role? Role { get; set; }
    public Employee? Employee { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
}
