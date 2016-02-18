using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebInviteOpdracht.Models
{
    public class GuestResponse {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression("^(?:\\(?)(\\d{3})(?:[\\).]?)(\\d{3})(?:[-\\.]?)(\\d{4})(?!\\d)", ErrorMessage = "Please enter a valid Phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please specify whter you'll attend")]
        public bool? WillAttend { get; set; }
    }
}