using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Domain.Entities;

[Table("OrderItems")]
public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }

    public virtual Order Order { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [Precision(14, 2)]
    public decimal UnitPrice { get; set; }

}
