//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aion.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TicketType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TicketType()
        {
            this.TicketStubs = new HashSet<TicketStub>();
            this.TicketTemplates = new HashSet<TicketTemplate>();
        }
    
        public int TicketTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte SLAPeriod { get; set; }
        public short EscalationId { get; set; }
        public Nullable<bool> Live { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketStub> TicketStubs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketTemplate> TicketTemplates { get; set; }
    }
}
