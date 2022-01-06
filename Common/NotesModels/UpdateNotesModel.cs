using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.NotesModels
{
    public class UpdateNotesModel
    {
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name = "Message")]
        [DataType(DataType.Text)]
        public string Message { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "ModifiedAt")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedAt { get; set; }
    }
}
