using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevCommands.DTOs
{
    public class CommandUpdateDTO
    {
        // Same as Create - update could be changed at a later date
        [Required]
        [MaxLength(200)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }

    }
}
