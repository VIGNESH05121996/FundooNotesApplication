﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CollaboratorModels
{
    public class CollaborateModel
    {
        [DataType(DataType.EmailAddress)]
        public string Collaborated_Email { get; set; }
    }
}
