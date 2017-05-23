using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace GoldMantis.Entity
{

    public class SAAttachment
    {
        public virtual int ID { get; set; }

        [Required]
        public virtual int BillID { get; set; }

        [Required]
        [StringLength(128)]
        public virtual string TableName { get; set; }

        [StringLength(128)]
        public virtual string Expand { get; set; }

        [Required]
        [StringLength(128)]
        public virtual string DocName { get; set; }

        [Required]
        [StringLength(1024)]
        public virtual string DocPath { get; set; }

        [Required]
        public virtual int DocSize { get; set; }

        [StringLength(1024)]
        public virtual string DocEncryptPath { get; set; }


        public virtual bool? IsCheck { get; set; }
    }
}
