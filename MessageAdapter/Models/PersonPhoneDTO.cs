using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAdapter.Models
{
    public class PersonPhoneDTO
    {
        /// <summary>Initializes a new instance of the <see cref="PersonPhoneDTO"/> class.</summary>
        public PersonPhoneDTO() { }

        /// <summary>Initializes a new instance of the <see cref="PersonEmailDTO"/> class with initial values.</summary>
        /// <param name="phoneType">The use type of the phone.</param>
        /// <param name="phone">The phone number value.</param>
        public PersonPhoneDTO(byte phoneType, string phone)
        {
            PhoneType = phoneType;
            Phone = phone;
        }

        /// <summary>The use type of the phone.</summary>
        public byte PhoneType { get; set; }

        /// <summary>The phone number value.</summary>
        public string Phone { get; set; }
    }
}
