using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLink.Models
{
    public class EndUser
    {
        public string? Id { get; set; }

        public EndUser(string? id)
        {
            Id = id;
        }
    }
}
