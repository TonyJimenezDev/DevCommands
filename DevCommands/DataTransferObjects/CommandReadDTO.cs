using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCommands.DTOs
{
    public class CommandReadDTO
    {
        public int Id { get; set; }

        public string HowTo { get; set; }

        public string Line { get; set; }

        //public string Platform { get; set; }

        //public bool Confirmed { get; set; }
    }
}
