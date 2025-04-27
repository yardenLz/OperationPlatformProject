using System;
using System.ComponentModel.DataAnnotations;

namespace YardenProject.Models
{
    public class OperationHistory
    {
        [Key]
        public int Id { get; set; }
        public string FieldA { get; set; }
        public string FieldB { get; set; }
        public string Operation { get; set; }
        public string Result { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
