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
    
    public partial class RestaurantBooking
    {
        public System.Guid Id { get; set; }
        public int Seq { get; set; }
        public System.Guid RestaurantId { get; set; }
        public int GroupSize { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan Time { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedByUserId { get; set; }
    
        public virtual Restaurant Restaurant { get; set; }
    }
}
