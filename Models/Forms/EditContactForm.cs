using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo_2.Models.Forms
{
    public class EditContactForm
    {

        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(75)]
        public string LastName { get; set; }

        [Required]
        [StringLength(75)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(384)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IList<SelectListItem> Categories { get; set; }

    }
}
