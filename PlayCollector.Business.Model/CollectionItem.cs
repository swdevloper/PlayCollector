//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlayCollector.Business.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CollectionItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CollectionItem()
        {
            this.ItemMediaSet = new HashSet<ItemMedia>();
        }
    
        public long Id { get; set; }
        public Nullable<long> FK_Id_Theme { get; set; }
        public Nullable<long> FK_Id_Storage { get; set; }
        public Nullable<int> FK_Id_Condition { get; set; }
        public string Name { get; set; }
        public string Setnumber { get; set; }
        public Nullable<int> PublishingYear { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Worth { get; set; }
        public decimal Quantity { get; set; }
        public bool WithPackaging { get; set; }
        public bool WithManual { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual Condition Condition { get; set; }
        public virtual Storage Storage { get; set; }
        public virtual Theme Theme { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemMedia> ItemMediaSet { get; set; }
    }
}
