using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class NotesModel
    {
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name = "Message")]
        [DataType(DataType.Text)]
        public string Message { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Archive")]
        public bool Archive { get; set; }

        [Display(Name = "Pin")]
        public bool Pin { get; set; }

        [Display(Name = "Trash")]
        public bool Trash { get; set; }

        [Display(Name = "CreatedAt")]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        public long UserId { get; set; }
    }
}
