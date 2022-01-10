using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.NotesModels
{
    public class GetNotesResposeModel
    {
        public long NotesId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool Archive { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public long UserId { get; set; }
    }
}
