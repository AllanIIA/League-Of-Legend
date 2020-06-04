﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace League_Of_Legend.ViewModels
{
    public class ContactViewModels
    {
        [StringLength(100)]
        [Required(ErrorMessage = "Le nom est requis")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Le mail est incorrect")]
        [Required(ErrorMessage = "L'email est requis")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "L'objet du message est requis")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
