using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Lawler
{
    public class Group
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public List<Group> Children { get; set; }
    }
}
