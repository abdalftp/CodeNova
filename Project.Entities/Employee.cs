﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOJ { get; set; }
        public Department Department { get; set; }
        public Project ProjectID { get; set; }

    }
}
