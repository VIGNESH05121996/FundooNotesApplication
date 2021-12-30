using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class FundooNotes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotesId { get; set; }

        [Display(Name ="Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name="Message")]
        [DataType(DataType.Text)]
        public string Message { get; set; }
        
        [Display(Name ="Remainder")]
        [DataType(DataType.DateTime)]
        public DateTime? Remainder { get; set; }

        [Display(Name ="Color")]
        public string Color { get; set; }

        [Display(Name ="Image")]
        public string Image { get; set; }

        [Display(Name ="Archive")]
        public bool Archive { get; set; }

        [Display(Name ="Pin")]
        public bool Pin { get; set; }

        [Display(Name ="Trash")]
        public bool Trash { get; set; }

        [Display(Name ="CreatedAt")]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        [Display(Name ="ModifiedAt")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedAt { get; set; }


        public long UserId { get; set; }
        [ForeignKey("UserId")]

        public FundooUser FundooUser { get; set; }

    }
}
