namespace MessageAdapter.Models
{
    #region --using--
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion
    public enum ValidationStatus
    {
        /// <summary>Address validation was overridden and a save was committed.</summary>
        Overridden,

        /// <summary>Address was valid and a save was committed.</summary>
        Committed,

        /// <summary>Address was valid and a save was not committed because the address AddressID is not 0.</summary>
        Valid,

        /// <summary>Address was not completely valid and the response includes address suggestions.</summary>  
        Suggestions
    }
}
