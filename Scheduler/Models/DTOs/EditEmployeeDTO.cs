﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class EditEmployeeDTO
    {
        public Employee Employee { get; set; }

        public List<string> TeamNames { get; set; }
        public List<string> TeamRoleNames { get; set; }
    }
}