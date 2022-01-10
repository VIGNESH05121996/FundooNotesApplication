using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CollaboratorModels
{
    public class CollabResponseModel
    {
        public long CollaboratorId { get; set; }
        public long NotesId { get; set; }
        public long UserId { get; set; }
        public string Collaborated_Email { get; set; }
    }
}
