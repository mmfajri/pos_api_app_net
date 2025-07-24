using pos_api_app.Models;
using pos_api_app.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Model.Entities;

[Table("tb_m_unit")]
public class Unit : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    //Cardinality
    public ICollection<Price>? Prices { get; set; }
}
