namespace MessageAdapter.Models
{
    #region --Using--
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion
    public class AddressValidationResultDTO
    {
        public int AddressID { get; set; }
        public AddressDTO OriginalAddress { get; set; }
        public List<ValidatedAddress> ValidatedAddresses { get; set; }
        public ValidationStatus Status { get; set; }
    }
}
