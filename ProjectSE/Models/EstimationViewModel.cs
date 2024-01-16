using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectSE.Models
{
    public class EstimationViewModel
    {
        [Key]
        public int EstimateId { get; set; }
        public string Technician { get; set; }

        public string Phone { get; set; }

        public string Rate { get; set; }

       
    }
}