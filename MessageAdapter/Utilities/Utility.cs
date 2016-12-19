using MessageAdapter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using MessageAdapter.NAVNSLUser;

namespace MessageAdapter.Utilities
{
    public class Utility
    {
        /// <summary>
        /// Gets the nav person.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <returns></returns>
        public static List<person> GetNAVPerson(XDocument xmlDoc)
        {
            List<person> personObj = new List<person>();
            //Fetch the elements 
            IEnumerable<XElement> xelementList = xmlDoc.Descendants(Constants.PersonNode);
            //TODO: multiple person
            if (xelementList != null)
            {
                //Read the element value into person object
                personObj = (from b in xelementList
                             select new person
                             {
                                 personGuid = b.Element(Constants.PersonGuid) != null ? b.Element(Constants.PersonGuid).Value : null,
                                 masterGuid = b.Element(Constants.MasterGuid) != null ? b.Element(Constants.MasterGuid).Value : null,
                                 personId = b.Element(Constants.PersonId) != null ? b.Element(Constants.PersonId).Value : null,
                                 firstName = b.Element(Constants.FirstName) != null ? b.Element(Constants.FirstName).Value : null,
                                 lastName = b.Element(Constants.LastName) != null ? b.Element(Constants.LastName).Value : null,
                                 title = b.Element(Constants.Title) != null ? b.Element(Constants.Title).Value : null,
                                 messageCreatedDate = b.Attribute(Constants.MessageCreatedDate) != null ? b.Attribute(Constants.MessageCreatedDate).Value : null,
                                 messageType = b.Attribute(Constants.MessageType) != null ? b.Attribute(Constants.MessageType).Value : null,
                                 sender = b.Attribute(Constants.Sender) != null ? b.Attribute(Constants.Sender).Value : null,
                                 originator = b.Attribute(Constants.Originator) != null ? b.Attribute(Constants.Originator).Value : null,
                             }).ToList();

                foreach (person personData in personObj)
                {
                    //this is done as the person xml sent from person service do not have a value for originator. A work around for empty originator from person service
                    if (string.IsNullOrEmpty(personData.originator))
                    {
                        personData.originator = ConfigurationManager.AppSettings[Constants.DefaultOriginator];
                    }
                    //generate list of emails, phones and addresses
                    List<emails> emailList = new List<emails>();
                    List<addresses> addressList = new List<addresses>();
                    List<phones> phoneList = new List<phones>();

                    //Fetch the emails node content
                    IEnumerable<XElement> emailsElementList = xelementList.Elements(Constants.Emails);
                    if (emailsElementList != null && emailsElementList.Count() > 0)
                    {
                        //Fetch the email node from the emails element list
                        IEnumerable<XElement> emailElement = emailsElementList.Elements(Constants.Email);
                        if (emailElement != null && emailElement.Count() > 0)
                        {
                            //Read the Type and address of each email into the emails list
                            foreach (XElement email in emailElement)
                            {
                                email mail = new email();
                                List<email> emailsList = new List<email>();
                                emails emails = new emails();

                                mail.type = email.Attribute(Constants.Type) != null ? email.Attribute(Constants.Type).Value : null;
                                mail.address = email.Attribute(Constants.Address) != null ? email.Attribute(Constants.Address).Value : null;

                                emailsList.Add(mail);
                                emails.email = emailsList.ToArray();
                                emailList.Add(emails);
                            }
                            emails[] emailArray = emailList.ToArray();
                            personData.emails = emailArray;
                        }
                    }

                    //Fetch the addresses node content
                    IEnumerable<XElement> addressesElementList = xelementList.Elements(Constants.Addresses);
                    if (addressesElementList != null && addressesElementList.Count() > 0)
                    {
                        //Fetch the address node from the addresses element list
                        IEnumerable<XElement> addressElement = addressesElementList.Elements(Constants.Address);
                        if (addressElement != null && addressElement.Count() > 0)
                        {
                            foreach (XElement addressDetail in addressElement)
                            {
                                address addressObj = new address();
                                List<address> addressesList = new List<address>();
                                addresses addressesObj = new addresses();

                                addressObj.number = addressDetail.Attribute(Constants.Number) != null ? addressDetail.Attribute(Constants.Number).Value : null;
                                addressObj.id = addressDetail.Attribute(Constants.ID) != null ? addressDetail.Attribute(Constants.ID).Value : null;
                                addressObj.type = addressDetail.Attribute(Constants.Type) != null ? addressDetail.Attribute(Constants.Type).Value : null;
                                addressObj.shipToName = addressDetail.Attribute(Constants.ShipToName) != null ? addressDetail.Attribute(Constants.ShipToName).Value : null;
                                addressObj.isPrimary = addressDetail.Attribute(Constants.IsPrimary) != null ? addressDetail.Attribute(Constants.IsPrimary).Value : null;
                                addressObj.companyName = addressDetail.Element(Constants.CompanyName) != null ? addressDetail.Element(Constants.CompanyName).Value : null;
                                addressObj.country = addressDetail.Element(Constants.Country) != null ? addressDetail.Element(Constants.Country).Value : null;

                                addressObj.city = addressDetail.Element(Constants.City) != null ? addressDetail.Element(Constants.City).Value : null;
                                addressObj.line1 = addressDetail.Element(Constants.Line1) != null ? addressDetail.Element(Constants.Line1).Value : null;
                                addressObj.line2 = addressDetail.Element(Constants.Line2) != null ? addressDetail.Element(Constants.Line2).Value : null;
                                addressObj.line3 = addressDetail.Element(Constants.Line3) != null ? addressDetail.Element(Constants.Line3).Value : null;
                                addressObj.regionCode = addressDetail.Element(Constants.RegionCode) != null ? addressDetail.Element(Constants.RegionCode).Value : null;
                                addressObj.postalCode = addressDetail.Element(Constants.PostalCode) != null ? addressDetail.Element(Constants.PostalCode).Value : null;

                                addressesList.Add(addressObj);
                                addressesObj.address = addressesList.ToArray();

                                addressList.Add(addressesObj);
                            }
                            addresses[] addressArray = addressList.ToArray();
                            personData.addresses = addressArray;
                        }
                    }
                    //Fetch the phones node content
                    IEnumerable<XElement> phonesElementList = xelementList.Elements(Constants.Phones);
                    if (phonesElementList != null && phonesElementList.Count() > 0)
                    {
                        //Fetch the phone node from the phones element list
                        IEnumerable<XElement> phoneElement = phonesElementList.Elements(Constants.Phone);
                        if (phoneElement != null && phoneElement.Count() > 0)
                        {
                            foreach (XElement phoneDetail in phoneElement)
                            {
                                phone phoneObj = new phone();
                                phones phonesObj = new phones();
                                List<phone> phonesList = new List<phone>();

                                phoneObj.number = phoneDetail.Attribute(Constants.Number).Value;
                                phoneObj.type = phoneDetail.Attribute(Constants.Type).Value;


                                phonesList.Add(phoneObj);
                                phonesObj.phone = phonesList.ToArray();
                                phoneList.Add(phonesObj);
                            }
                            phones[] phoneArray = phoneList.ToArray();
                            personData.phones = phoneArray;
                        }
                    }
                }
            }
            return personObj;
        }

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <returns></returns>
        public static List<PersonDTO> GetPerson(XDocument xmlDoc)
        {
            IEnumerable<XElement> xelementList = xmlDoc.Descendants(Constants.PersonNode);
            List<PersonDTO> personObj = new List<PersonDTO>();
            //multiple person
            if (xelementList != null)
            {
                //Read the element value into person object
                personObj = (from b in xelementList
                             select new PersonDTO
                             {
                                 PersonGuid = b.Element(Constants.PersonGuid) != null && !string.IsNullOrWhiteSpace(b.Element(Constants.PersonGuid).Value) ? new Guid(b.Element(Constants.PersonGuid).Value) : Guid.Empty,
                                 MasterGuid = b.Element(Constants.MasterGuid) != null && !string.IsNullOrWhiteSpace(b.Element(Constants.MasterGuid).Value) ? new Guid(b.Element(Constants.MasterGuid).Value) : Guid.Empty,
                                 PersonID = b.Element(Constants.PersonId) != null && !string.IsNullOrWhiteSpace(b.Element(Constants.PersonId).Value) ? Convert.ToInt32(b.Element(Constants.PersonId).Value) : 0,
                                 FirstName = b.Element(Constants.FirstName) != null ? b.Element(Constants.FirstName).Value : null,
                                 LastName = b.Element(Constants.LastName) != null ? b.Element(Constants.LastName).Value : null,
                                 Title = b.Element(Constants.Title) != null ? b.Element(Constants.Title).Value : null
                             }).ToList();

                foreach (PersonDTO person in personObj)
                {
                    //generate list of emails, phones and addresses
                    List<emails> emailList = new List<emails>();
                    List<addresses> addressList = new List<addresses>();
                    List<phones> phoneList = new List<phones>();

                    person.Addresses = new List<PersonAddressDTO>();
                    person.Emails = new List<PersonEmailDTO>();
                    person.Phones = new List<PersonPhoneDTO>();

                    //Fetch the emails node content
                    IEnumerable<XElement> emailsElementList = xelementList.Elements(Constants.Emails);
                    if (emailsElementList != null)
                    {
                        //Fetch the email node from the emails element list
                        IEnumerable<XElement> emailElement = emailsElementList.Elements(Constants.Email);
                        if (emailElement != null)
                        {
                            List<PersonEmailDTO> emailsList = new List<PersonEmailDTO>();

                            //Read the Type and address of each email into the emails list
                            foreach (XElement email in emailElement)
                            {
                                PersonEmailDTO mail = new PersonEmailDTO();
                                string emailTypeString = email.Attribute(Constants.Type) != null ? email.Attribute(Constants.Type).Value : null;

                                if (!string.IsNullOrEmpty(emailTypeString))
                                {
                                    EmailTypes emailType;
                                    bool isParsed = Enum.TryParse<EmailTypes>(emailTypeString.ToLower(), out emailType);
                                    if (isParsed)
                                    {
                                        mail.EmailType = emailType;
                                        mail.Email = email.Attribute(Constants.Address) != null ? email.Attribute(Constants.Address).Value : null;
                                        emailsList.Add(mail);
                                    }
                                }
                            }
                            person.Emails = emailsList;
                        }
                    }

                    /******Commented as on Nov 11th 2016, address service was not integrated and was not taken up for drop 1 release*/

                    //Fetch the addresses node content
                    IEnumerable<XElement> addressesElementList = xelementList.Elements(Constants.Addresses);
                    if (addressesElementList != null)
                    {
                        //Fetch the address node from the addresses element list
                        IEnumerable<XElement> addressElement = addressesElementList.Elements(Constants.Address);
                        if (addressElement != null)
                        {
                            List<PersonAddressDTO> addressesList = new List<PersonAddressDTO>();
                            foreach (XElement addressDetail in addressElement)
                            {
                                PersonAddressDTO addressObj = new PersonAddressDTO();
                                if (addressDetail.Attribute(Constants.Number) != null)
                                {
                                    int addressNumber;
                                    int.TryParse(addressDetail.Attribute(Constants.Number).Value, out addressNumber);
                                    addressObj.AddressNumber = addressNumber;
                                }
                                if (addressDetail.Attribute(Constants.ID) != null)
                                {
                                    int addressID;
                                    int.TryParse(addressDetail.Attribute(Constants.ID).Value, out addressID);
                                    addressObj.AddressID = addressID;
                                }
                                if (addressDetail.Attribute(Constants.Type) != null)
                                {
                                    AddressType addressType;
                                    Enum.TryParse<AddressType>(addressDetail.Attribute(Constants.Type).Value.ToLower(), out addressType);
                                    addressObj.AddressType = (byte)addressType;
                                }
                                if (addressDetail.Attribute(Constants.IsPrimary) != null)
                                {
                                    bool isPrimary;
                                    bool.TryParse(addressDetail.Attribute(Constants.IsPrimary).Value, out isPrimary);
                                    addressObj.IsPrimary = isPrimary;
                                }

                                addressObj.ShipToName = addressDetail.Attribute(Constants.ShipToName) != null ? addressDetail.Attribute(Constants.ShipToName).Value : null;

                                AddressDTO address = new AddressDTO();
                                //TODO: the below properties are to be updated to address service : Done
                                address.CompanyName = addressDetail.Element(Constants.CompanyName) != null ? addressDetail.Element(Constants.CompanyName).Value : null;
                                address.CountryCode = addressDetail.Element(Constants.Country) != null ? addressDetail.Element(Constants.Country).Value : null;

                                address.City = addressDetail.Element(Constants.City) != null ? addressDetail.Element(Constants.City).Value : null;
                                address.Line1 = addressDetail.Element(Constants.Line1) != null ? addressDetail.Element(Constants.Line1).Value : null;
                                address.Line2 = addressDetail.Element(Constants.Line2) != null ? addressDetail.Element(Constants.Line2).Value : null;
                                address.Line3 = addressDetail.Element(Constants.Line3) != null ? addressDetail.Element(Constants.Line3).Value : null;
                                address.RegionCode = addressDetail.Element(Constants.RegionCode) != null ? addressDetail.Element(Constants.RegionCode).Value : null;
                                address.PostalCode = addressDetail.Element(Constants.PostalCode) != null ? addressDetail.Element(Constants.PostalCode).Value : null;
                                AddressValidationResultDTO addressValidationResult = CreateAddress(address).Result;
                                if (addressValidationResult != null && addressValidationResult.AddressID != 0)
                                {
                                    addressObj.AddressID = addressValidationResult.AddressID;
                                    addressesList.Add(addressObj);
                                }
                            }
                            person.Addresses = addressesList;
                        }
                    }
                    //Fetch the phones node content
                    IEnumerable<XElement> phonesElementList = xelementList.Elements(Constants.Phones);
                    if (phonesElementList != null)
                    {
                        //Fetch the phone node from the phones element list
                        IEnumerable<XElement> phoneElement = phonesElementList.Elements(Constants.Phone);
                        if (phoneElement != null)
                        {
                            List<PersonPhoneDTO> phonesList = new List<PersonPhoneDTO>();
                            foreach (XElement phoneDetail in phoneElement)
                            {
                                PersonPhoneDTO phoneObj = new PersonPhoneDTO();
                                string phoneTypeString = phoneDetail.Attribute(Constants.Type) != null ? phoneDetail.Attribute(Constants.Type).Value : null;
                                if (!string.IsNullOrEmpty(phoneTypeString))
                                {
                                    PhoneType phoneType;
                                    bool isPhoneTypeParsed = Enum.TryParse<PhoneType>(phoneTypeString.ToLower(), out phoneType);
                                    if (isPhoneTypeParsed)
                                    {
                                        phoneObj.PhoneType = (byte)phoneType;
                                        phoneObj.Phone = phoneDetail.Attribute(Constants.Number) != null ? phoneDetail.Attribute(Constants.Number).Value : null;
                                        phonesList.Add(phoneObj);
                                    }
                                }
                            }
                            person.Phones = phonesList;
                        }
                    }
                }
            }
            return personObj;
        }

        /// <summary>
        /// Updates the person asynchronous.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="person">The person.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <returns></returns>
        public static async Task<PersonDTO> UpdatePerson(Guid guid, PersonDTO person, string updatedBy, bool republish)
        {
            HttpResponseMessage result;
            PersonDTO personObj = null;

            //HttpClient Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings[Constants.ApiGatewayURL]);
                result = await client.PutAsJsonAsync(ConfigurationManager.AppSettings[Constants.UpdatePersonURI] + guid + ConfigurationManager.AppSettings[Constants.UpdatedByQueryString] + updatedBy + Constants.RePublishQueryString + republish, person).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    personObj = result.Content.ReadAsAsync<PersonDTO>().Result;
                }
                else
                {
                    string error = result.Content.ReadAsStringAsync().Result;
                }
                return personObj;
            }
        }

        /// <summary>
        /// Creates the person asynchronous.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="createdBy">The created by.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> CreatePersonAsync(PersonDTO person, string createdBy, bool republish)
        {
            HttpResponseMessage result;
            //HttpClient Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings[Constants.ApiGatewayURL]);
                result = await client.PostAsJsonAsync(ConfigurationManager.AppSettings[Constants.CreatePersonURI] + ConfigurationManager.AppSettings[Constants.CreatedByQueryString] + createdBy + Constants.RePublishQueryString + republish, person).ConfigureAwait(false);
                return result;
            }
        }
        /// <summary>
        /// Gets the originator.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <returns></returns>
        public static string GetMessageTypeAndOriginator(XDocument xmlDoc, out string messageType)
        {
            string originator = string.Empty;
            messageType = string.Empty;
            //Fetch the message type and originator
            if (xmlDoc != null && xmlDoc.Root != null)
            {
                messageType = xmlDoc.Root.Attribute(Constants.MessageType) != null ? xmlDoc.Root.Attribute(Constants.MessageType).Value : null;
                originator = xmlDoc.Root.Attribute(Constants.Originator) != null ? xmlDoc.Root.Attribute(Constants.Originator).Value : null;
            }
            return originator;
        }

        /// <summary>
        /// Gets the person by unique identifier.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        public static async Task<PersonDTO> GetPersonByGuid(Guid userID)
        {
            HttpResponseMessage result;
            PersonDTO userDetail = new PersonDTO();
            //HttpClient Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings[Constants.ApiGatewayURL]);
                string uri = ConfigurationManager.AppSettings[Constants.GetPersonURI] + userID;
                result = await client.GetAsync(uri).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    userDetail = result.Content.ReadAsAsync<PersonDTO>().Result;
                }
                return userDetail;
            }
        }

        /// <summary>
        /// Creates the address.
        /// </summary>
        /// <param name="newAddress">The new address.</param>
        /// <returns></returns>
        public static async Task<AddressValidationResultDTO> CreateAddress(AddressDTO newAddress)
        {
            HttpResponseMessage result;
            AddressValidationResultDTO validationStatus = null;
            //HttpClient Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings[Constants.ApiGatewayURL]);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(ConfigurationManager.AppSettings[Constants.AuthorizationScheme], ConfigurationManager.AppSettings[Constants.AuthorizationParameter]);
                result = await client.PostAsJsonAsync(ConfigurationManager.AppSettings[Constants.AddressServicePostUri], newAddress).ConfigureAwait(false);
                validationStatus = result.Content.ReadAsAsync<AddressValidationResultDTO>().Result;
                //var error = result.Content.ReadAsStringAsync().Result;
                return validationStatus;
            }
        }
    }
}