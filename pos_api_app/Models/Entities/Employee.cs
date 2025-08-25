using pos_api_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models.Entities;

[Table("tb_m_employee")]
public class Employee : BaseEntity
{
    [Column("firstname", TypeName = "varchar(200)")]
    public string FirstName { get; set; } = string.Empty;

    [Column("lastname", TypeName = "varchar(200)")]
    public string? LastName { get; set; }

    [Column("account_id")]
    public int? AccountId { get; set; }

    //Cardinality
    public Role? Role { get; set; }
    public Account? Account { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
}
