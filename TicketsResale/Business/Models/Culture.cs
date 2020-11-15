using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsResale.Business.Models
{
    public class Culture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Resource> Resources { get; set; }
        public Culture()
        {
            Resources = new List<Resource>();
        }
    }
}
