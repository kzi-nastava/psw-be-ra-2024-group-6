using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public double Price { get; set; }
    public int ResourceTypeId { get; set; }
    public long? ResourceId { get; set; }
}
