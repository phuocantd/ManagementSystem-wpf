//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManagementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.BillDetails = new HashSet<BillDetail>();
        }
    
        public string ID { get; set; }
        public Nullable<System.DateTime> DateBill { get; set; }
        public Nullable<int> ID_Customer { get; set; }
        public Nullable<int> ID_Sale { get; set; }
        public Nullable<int> ID_Transport { get; set; }
        public Nullable<long> SumPrice { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual Transport Transport { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}