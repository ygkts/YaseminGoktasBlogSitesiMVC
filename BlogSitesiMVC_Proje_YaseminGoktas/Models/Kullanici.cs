//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlogSitesiMVC_Proje_YaseminGoktas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            this.Makale = new HashSet<Makale>();
            this.MultiMedia = new HashSet<MultiMedia>();
            this.SiteTakip = new HashSet<SiteTakip>();
            this.Yorum = new HashSet<Yorum>();
        }
    
        public System.Guid Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Mail { get; set; }
        public System.DateTime KayitTarihi { get; set; }
        public string Nick { get; set; }
        public Nullable<int> ResimID { get; set; }
        public bool YazarMi { get; set; }
        public bool Aktif { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makale> Makale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MultiMedia> MultiMedia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiteTakip> SiteTakip { get; set; }
        public virtual MultiMedia MultiMedia1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorum { get; set; }
    }
}
