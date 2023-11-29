using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework16.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string CustomerEmail { get; set; }
        public int Code { get; set; }
    }
}
