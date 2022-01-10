using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.LableModel
{
    public class LableModel
    {
        [DataType(DataType.Text)]
        public string Lable_Name { get; set; }
    }
}
