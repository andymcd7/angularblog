﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApi.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string BlogText { get; set; }
        public DateTime? BlogDate { get; set; }
        public Int32 BlogUser { get; set; }
    }
}