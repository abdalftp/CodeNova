using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
   public class Project
    {
        [Key]
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public string ProjectManagerName { get; set; }
        public string ProjectManagerEmail { get; set; }
        public virtual List<Employee> Employee { get; set; }

       
    }
}
