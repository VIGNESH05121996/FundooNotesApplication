using Authentication.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class JwtTokenGenerate : IAuthrnticate
    {
        public readonly string key;
        public JwtTokenGenerate(LoginModel Key)
    }
}
