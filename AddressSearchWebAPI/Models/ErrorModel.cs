using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressSearchWebAPI.Models
{
    public class ErrorModel
    {
        public string FunName { get; set; }
        public string IpAddress { get; set; }
        public string ExceptionText { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string AccountNo { get; set; }
        public string Source { get; set; }
        public string BrandCode { get; set; }
    }
}