using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAdapter.Models
{
    public class PersonEmailDTO
    {
        public PersonEmailDTO() { }

        /// <summary>Initializes a new instance of the <see cref="PersonEmailDTO"/> class with initial values.</summary>
        /// <param name="emailType">The use type of the email.</param>
        /// <param name="email">The email address value.</param>
        public PersonEmailDTO(EmailTypes emailType, string email)
        {
            EmailType = emailType;
            Email = email;
        }

        /// <summary>The email address value.</summary>
         public EmailTypes EmailType { get; set; }

        /// <summary>The use type of the email.</summary>
        public string Email { get; set; }
    }
}
