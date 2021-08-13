using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressSearchWebAPI.Models.Requests
{
    public class BaseRequestModel
    {
        /// <summary>
        /// Source from where the request is originated. Allowed values are MyAccount.
        /// </summary>
        [Required]
        public string Source { get; set; }
    }
}