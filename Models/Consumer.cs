using System;
using System.Collections.Generic;

namespace PortalForBiddingDB.Models;

public partial class Consumer
{
    public int Id { get; set; }

    public string? ConsumerName { get; set; }

    public string? ConsumerMailId { get; set; }

    public int? FarmerId { get; set; }

    public decimal? BiddedPrice { get; set; }

    public string? Status { get; set; }

    //public virtual Farmer? Farmer { get; set;}
}
