using System;
using System.Collections.Generic;

namespace PortalForBiddingDB.Models;

public partial class Farmer
{
    public int Id { get; set; }

    public string? FarmerName { get; set; }

    public string? FarmerMailId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductCategory { get; set; }

    public decimal? ProductQuantity { get; set; }

    public decimal? ProductBasePrice { get; set; }

    public string? Status { get; set; }

    //public virtual ICollection<Consumer>? Consumers { get; set; }
}
