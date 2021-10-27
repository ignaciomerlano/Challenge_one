using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_one.Model
{
    public class AppSetting
    {
        public int Id { get; set; }
        public string AppKey { get; set; }
        public string AppValue { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
