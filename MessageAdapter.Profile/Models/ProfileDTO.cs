namespace MessageAdapter.Profile
{
    #region --using--
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

  public class ProfileDTO
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        public int PersonID { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public System.Guid UserID { get; set; }
        /// <summary>
        /// Gets or sets the attribute.
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        public Dictionary<string, object> Attribute { get; set; }
        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets the DOB.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string DOB { get; set; }

        /// <summary>
        /// Gets or sets the DOB.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string InstituteRegNo { get; set; }
        /// <summary>
        /// Gets or sets the ProfileOffersPromotions.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string OffersPromotions { get; set; }
        /// <summary>
        /// Gets or sets the ProfilePreferredProduct.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string PreferredProduct { get; set; }
        /// <summary>
        /// Gets or sets the ProfileTermsAndConditions.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string TermsAndConditions { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>
        /// The created time.
        /// </value>
        public Nullable<System.DateTime> CreatedTime { get; set; }
        /// <summary>
        /// Gets or sets the modified time.
        /// </summary>
        /// <value>
        /// The modified time.
        /// </value>
        public Nullable<System.DateTime> ModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a Originator
        /// </summary>
        /// <value>
        ///  The Originator
        /// </value>
        public string Originator { get; set; }
        /// <summary>
        /// Gets or sets a Originator
        /// </summary>
        /// <value>
        ///  The Originator
        /// </value>
        public string Sender { get; set; }
    }
}
