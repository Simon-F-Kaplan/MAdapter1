namespace MessageAdapter.Models
{
    #region --Using--
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion
    public  class PersonDTO
    {

        #region --Properties--
        public int? PersonID { get; set; }

        /// <summary>The globally unique identifier for the person.</summary>
        public Guid? PersonGuid { get; set; }

        /// <summary>The globally unique identifier of the master account for this duplicate</summary>
        public Guid? MasterGuid { get; set; }

        /// <summary>The email address used by the person.</summary>
        public IEnumerable<PersonEmailDTO> Emails { get; set; } 

        /// <summary>The enumeration of addresses configured for the person.</summary>
        public IEnumerable<PersonAddressDTO> Addresses { get; set; } 

        /// <summary>The enumeration of phone number for the person.</summary>
        public IEnumerable<PersonPhoneDTO> Phones { get; set; }

        /// <summary>The first name of the person.</summary>
        public string FirstName { get; set; }

        /// <summary>The middle name of the person.</summary>
        public string MiddleName { get; set; }

        /// <summary>The last name of the person.</summary>
        public string LastName { get; set; }

        /// <summary>The title of the person.</summary>
        public string Title { get; set; }
        #endregion
    }
}
