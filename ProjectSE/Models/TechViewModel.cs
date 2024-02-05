using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSE.Models
{
    public class TechViewModel
    {
        public Technician technician { get; set; }

        public Account account { get; set; }
        public List<String> selectedTypeRepair { get; set; }
    }
}