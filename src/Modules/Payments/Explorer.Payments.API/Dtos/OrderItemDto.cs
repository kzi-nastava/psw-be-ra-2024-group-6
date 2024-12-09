using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos;

public class OrderItemDto
{
    public int Id { get; set; }
    public int TourId { get; set; }
    public int ShoppingCartId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public ProductDto Product { get; set; }
}
