using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Web.Models
{
    public class SlideViewModel
    {
        public int ID { set; get; }
        
        public string Name { set; get; }
        
        public string Description { set; get; }

        public string Content { set; get; }
        
        public string Image { set; get; }
        
        public string URL { set; get; }

        public int? DisplayOrder { set; get; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
        
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool Status { get; set; }
    }
}