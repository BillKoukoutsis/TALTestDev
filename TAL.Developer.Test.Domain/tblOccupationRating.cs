//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TAL.Developer.Test.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOccupationRating
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblOccupationRating()
        {
            this.tblOccupations = new HashSet<tblOccupation>();
        }
    
        public int oraId { get; set; }
        public string oraName { get; set; }
        public decimal oraFactor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOccupation> tblOccupations { get; set; }
    }
}
