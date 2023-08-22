using System;
using System.Collections.Generic;

namespace NorthWind.Web.Models;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
