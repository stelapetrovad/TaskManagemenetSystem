using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Task
    {
        [Key]
        public long? TaskID { get; set; }
        public string TaskName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime RequiredByDate { get; set; }
        public string TaskDescription { get; set; }
        public string TaskStatus { get; set; }
        public string TaskType { get; set; }
        public string User { get; set; }
        public DateTime NextActionDate { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }


        /*public override int ToString()
        {
            return TaskID;
        }*/
    }
}
