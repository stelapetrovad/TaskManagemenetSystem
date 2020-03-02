using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Comments
    {
        [Key]
        public long CommentID { get; set; }
        public string Comment { get; set; }
        public DateTime DateAddedComment { get; set; }
        public DateTime ReminderDateComment { get; set; }
        public virtual Task Task { get; set; }
    }
}
