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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorId { get; set; }


        public long NotesId { get; set; }
        [ForeignKey("NotesId")]

        public FundooNotes FundooNotes { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Collaborated_Email { get; set; }
    }
}
