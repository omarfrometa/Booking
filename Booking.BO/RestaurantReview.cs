//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Booking.BO
{
    using System;
    using System.Collections.Generic;
    
    public partial class RestaurantReview
    {
        public int Id { get; set; }
        public System.Guid RestaurantId { get; set; }
        public string Title { get; set; }
        public decimal Food { get; set; }
        public decimal Service { get; set; }
        public decimal Environment { get; set; }
        public decimal Total { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedByUserId { get; set; }
    
        public virtual Restaurant Restaurant { get; set; }
    }
}
