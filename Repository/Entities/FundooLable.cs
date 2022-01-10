using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class FundooLable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LableId { get; set; }


        public FundooNotes FundooNotes { get; set; }
        [ForeignKey("FundooNotes")]
        public long NotesId { get; set; }


        public FundooUser FundooUser { get; set; }
        [ForeignKey("FundooUser")]
        public long UserId { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Lable_Name { get; set; }
    }
}
