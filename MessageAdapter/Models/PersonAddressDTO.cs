using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAdapter.Models
{
    public class PersonAddressDTO
    {
          /// <summary>Initializes a new instance of the <see cref="PersonAddressDTO"/> class.</summary>
        public PersonAddressDTO() { }

        /// <summary>Initializes a new instance of the <see cref="PersonAddressDTO"/> class with initial values.</summary>
        /// <param name="addressID">The unique identifier of the address.</param>
        /// <param name="addressType">The use type of the address for the person.</param>
        /// <param name="shipToName">The name which should be used on this address when used for shipping.</param>
        /// <param name="isPrimary">Flag denoting this address as priority use for the person.</param>
        public PersonAddressDTO(int addressID, byte addressType, string shipToName, bool isPrimary)
        {
            AddressID = addressID;
            AddressType = addressType;
            ShipToName = shipToName;
            IsPrimary = isPrimary;
        }

        /// <summary>The unique identifier of an address in the context of a single person.</summary>
        public int AddressNumber { get; set; }

        /// <summary>The unique identifier of an address in the context of the system.</summary>
        public int AddressID { get; set; }

        /// <summary>The use type of the address by the person.</summary>
        public byte AddressType { get; set; }

        /// <summary>The name which should be used on this address when used for shipping.</summary>
        public string ShipToName { get; set; }

        /// <summary>Flag denoting this address as priority use for the person.</summary>
        public bool IsPrimary { get; set; }
    }
}
