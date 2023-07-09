using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketAPP.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public ICollection<Task>? Tasks { get; set; }
    }
}