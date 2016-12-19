﻿namespace MessageAdapter.Models
{
    #region --Using--
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    public class ValidatedAddress
    {
        public string CompanyName { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string RegionCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string SuggestionType { get; set; }
        public string Warnings { get; set; }
    }
}
