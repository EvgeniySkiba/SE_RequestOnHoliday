namespace SE_RequestOnHoliday.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rest")]
    public partial class Rest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rest()
        {
            Employers = new HashSet<Employer>();
        }

        public int Id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime EndDate { get; set; }

        public int Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employer> Employers { get; set; }
    }
}
