using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DCCore.WebMvc.Models
{
    public class GroupCheckBoxListUserViewModel
    {
        [Display(Name = "nombre")]
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public bool IsSelected { get; set; }
    }
}