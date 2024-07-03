using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Domain.Entities;

[Table("Orders")]
public class Order : BaseEntity
{

    [Required]
    [MaxLength(50)]
    public string UserId { get; set; }

    [Required]
    public OrderStatus Status { get; set; }

    [Precision(14, 2)]
    public decimal TotalAmount { get; set; }

    public virtual List<OrderItem> OrderItems { get; set; }

    [Required]
    public int ShippingAddressId { get; set; }

    public Address ShippingAddress { get; set; }

    [Required]
    public PaymentMethod PaymentMethod { get; set; }

}
