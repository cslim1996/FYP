//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamTimetabling2016
{
    using System;
    using System.Collections.Generic;
    
    public partial class Room2
    {
        public string RoomCode { get; set; }
        public Nullable<int> Floor { get; set; }
        public string Function { get; set; }
        public string VenueID { get; set; }
        public string BlockCode { get; set; }
    
        public virtual Block2 Block { get; set; }
        public virtual Venue2 Venue { get; set; }
    }
}
