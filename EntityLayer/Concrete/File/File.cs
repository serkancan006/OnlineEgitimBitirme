using EntityLayer.Concrete.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.File
{
    public class File : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        //public string Storage { get; set; }
        public string FileDisplayName { get; set; }
        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
