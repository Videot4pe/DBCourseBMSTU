using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Linq.Mapping;

namespace RK3
{
    
    [Table(Name = "Accounting")]
    public class Accounting
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string CourseName { get; set; }

        [Column]
        public DateTime DateOfAccounting { get; set; }

        [Column]
        public string Specialty { get; set; }

        [Column]
        public int Timelul { get; set; }

        [Column]
        public int Quantity { get; set; }
    }
    
    [Table(Name = "Employees")]
    public class Employees
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string FIO { get; set; }

        [Column]
        public DateTime DateOfBirth { get; set; }

        [Column]
        public string Specialty { get; set; }

        [Column]
        public DateTime DateOfUpgrade { get; set; }

        [Column]
        public int CourseId { get; set; }
    }
}
