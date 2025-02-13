﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Entities.Main
{
    public class Base
    {
        public DateTime CreatedOn { get; set; }

        [MaxLength(36)]
        public string? CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [MaxLength(36)]
        public string? ModifiedBy { get; set; }

    }

    public class BaseEntity : Base
    {
        public Guid Id { get; set; }
    }

    public class BaseIntEntity : Base
    {
        public int Id { get; set; }
    }
}
