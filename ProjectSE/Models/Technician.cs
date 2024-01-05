namespace ProjectSE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Technician")]
    public partial class Technician
    {
        [Key]
        public int technician_Id { get; set; }

        [StringLength(50)]
        public string technicianName { get; set; }

        [StringLength(50)]
        public string typeRepair { get; set; }

        [StringLength(10)]
        public string phone { get; set; }

        [StringLength(50)]
        public string image { get; set; }
    }
}
