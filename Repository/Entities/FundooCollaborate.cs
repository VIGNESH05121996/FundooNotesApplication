using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class FundooCollaborate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorId { get; set; }


        public FundooNotes FundooNotes { get; set; }
        public long NotesId { get; set; }


        public FundooUser FundooUser { get; set; }
        public long UserId { get; set; }
 

        [DataType(DataType.EmailAddress)]
        public string Collaborated_Email { get; set; }
    }
}
