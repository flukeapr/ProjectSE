using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectSE.Models
{
    public class EstimateViewModel
    {
        public int estimateId { get; set; }

        [Display(Name = "ชือผู้แจ้งซ่อม")]
        public string NameInform { get; set; }
        [Display(Name = "ประเภทการบริการ")]
        public string TypeRepair { get; set; }
        [Display(Name = "รายละเอียด")]
        public string Details { get; set; }
        [Display(Name = "ภาพประกอบ")]
        public string Picture { get; set; }
        [Display(Name = "สถานะ")]
        public string Status { get; set; }
        [Display(Name = "วันที่ต้องการซ่อม")]
        public DateTime Date { get; set; }
        [Display(Name = "เวลาที่ต้องการซ่อม")]
        public string Time { get; set; }
        [Display(Name = "แบบประเมิน")]
        public string EstimateStatus { get; set; }

        public string UserName { get; set; }
    }
}