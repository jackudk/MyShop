using System;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Model.Abstracts
{
    public abstract class Ass : IAuditable, ISeoable, ISwitchable
    {
        [MaxLength(256)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [MaxLength(256)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(256)]
        public string MetaDescription { get; set; }

        [MaxLength(256)]
        public string MetaKeyword { get; set; }

        public bool Status { get; set; }
    }
}