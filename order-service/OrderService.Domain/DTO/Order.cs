using OrderService.Domain.Enums;

namespace OrderService.Domain.DTO;

public class Order
{
    public string UserId { get; set; }

    public decimal TotalAmount { get; set; }

    public int ShippingAddressId { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
}
