namespace MessageAdapter.Profile
{
    #region --using--
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using NAVNSLUser;
    #endregion
    public class Utility
    {

        #region GetNAVProfile
        /// <summary>
        /// Gets the nav person.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <returns></returns>
        public static List<profile> GetNAVProfile(XDocument xmlDoc)
        {
            //convert the string to xml object
            List<profile> profileObj = new List<profile>();
            //Fetch the elements 
            IEnumerable<XElement> xelementList = xmlDoc.Descendants(Constants.ProfileNode);
            //TODO: multiple profile
            if (xelementList != null)
            {
                //reading xml values into profile object
                profileObj = (from b in xelementList
                              select new profile
                              {
                                  personGuid = b.Element(Constants.PersonGuid) != null ? b.Element(Constants.PersonGuid).Value : null,
                                  masterGuid = b.Element(Constants.MasterGuid) != null ? b.Element(Constants.MasterGuid).Value : null,
                                  personId = b.Element(Constants.PersonId) != null ? b.Element(Constants.PersonId).Value : null,
                                  dateOfBirth = b.Element(Constants.DateOfBirth) != null ? b.Element(Constants.DateOfBirth).Value : null,
                                  messageCreatedDate = b.Attribute(Constants.MessageCreatedDate) != null ? b.Attribute(Constants.MessageCreatedDate).Value : null,
                                  messageType = b.Attribute(Constants.MessageType) != null ? b.Attribute(Constants.MessageType).Value : null,
                                  preferredQualification = b.Element(Constants.PreferredQualificationField) != null ? b.Element(Constants.PreferredQualificationField).Value : null,
                                  sender = b.Attribute(Constants.Sender) != null ? b.Attribute(Constants.Sender).Value : null,
                                  originator = b.Attribute(Constants.Originator) != null ? b.Attribute(Constants.Originator).Value : null,
                                  receiveOffersAndPromotions = b.Element(Constants.OffersPromotions) != null ? b.Element(Constants.OffersPromotions).Value : null,
                                  country = b.Element(Constants.Country) != null ? b.Element(Constants.Country).Value : null,
                                  instituteRegNo = b.Element(Constants.InstituteRegNo) != null ? b.Element(Constants.InstituteRegNo).Value : null,
                              }).ToList();
            }

            return profileObj;
        }
        #endregion

        #region GetProfile
        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <returns></returns>
        public static List<ProfileDTO> GetProfile(XDocument xmlDoc)
        {
            IEnumerable<XElement> xelementList = xmlDoc.Descendants(Constants.ProfileNode);
            List<ProfileDTO> profileObj = new List<ProfileDTO>();
            //TODO: multiple profile
            if (xelementList != null)
            {
                //Read the element value into profile object
                profileObj = (from b in xelementList
                              select new ProfileDTO
                              {
                                  PersonID = b.Element(Constants.PersonId) != null && !string.IsNullOrWhiteSpace(b.Element(Constants.PersonId).Value) ? Convert.ToInt32(b.Element(Constants.PersonId).Value) : 0,
                                  UserID = b.Element(Constants.PersonGuid) != null && !string.IsNullOrWhiteSpace(b.Element(Constants.PersonGuid).Value) ? new Guid(b.Element(Constants.PersonGuid).Value) : Guid.Empty,
                                  Country = b.Element(Constants.Country) != null ? b.Element(Constants.Country).Value : null,
                                  DOB = b.Element(Constants.DateOfBirth) != null ? b.Element(Constants.DateOfBirth).Value : null,
                                  OffersPromotions = b.Element(Constants.OffersPromotions) != null ? b.Element(Constants.OffersPromotions).Value : null,
                                  PreferredProduct = b.Element(Constants.PreferredQualificationField) != null ? b.Element(Constants.PreferredQualificationField).Value : null,
                                  TermsAndConditions = b.Element(Constants.TermsAndConditions) != null ? b.Element(Constants.TermsAndConditions).Value : null,
                                  CreatedBy = b.Attribute(Constants.Originator) != null ? b.Attribute(Constants.Originator).Value : null,
                                  ModifiedBy = b.Attribute(Constants.Originator) != null ? b.Attribute(Constants.Originator).Value : null,
                                  CreatedTime = DateTime.Now,
                                  ModifiedTime = DateTime.Now,
                                  Sender = b.Attribute(Constants.Sender) != null ? b.Attribute(Constants.Sender).Value : null,
                                  Originator = b.Attribute(Constants.Originator) != null ? b.Attribute(Constants.Originator).Value : null,
                                  InstituteRegNo = b.Element(Constants.InstituteRegNo) != null ? b.Element(Constants.InstituteRegNo).Value : null,
                              }).ToList();
                foreach (ProfileDTO profile in profileObj)
                {
                    Dictionary<string, object> attributes = new Dictionary<string, object>();
                    if (!string.IsNullOrWhiteSpace(profile.Country))
                    {
                        attributes.Add(Constants.ProfileCountry, profile.Country);
                    }
                    if (!string.IsNullOrWhiteSpace(profile.InstituteRegNo))
                    {
                        attributes.Add(Constants.ProfileInstituteRegNo, profile.InstituteRegNo);
                    }
                    attributes.Add(Constants.DOB, !string.IsNullOrWhiteSpace(profile.DOB) ? profile.DOB : string.Empty);
                    attributes.Add(Constants.ProfileOffersPromotions, !string.IsNullOrWhiteSpace(profile.OffersPromotions) ? profile.OffersPromotions : Constants.FalseConst);
                    attributes.Add(Constants.ProfilePreferredProduct, !string.IsNullOrWhiteSpace(profile.PreferredProduct) ? profile.PreferredProduct : string.Empty);
                    attributes.Add(Constants.ProfileTermsAndConditions, !string.IsNullOrWhiteSpace(profile.TermsAndConditions) ? profile.TermsAndConditions : Constants.FalseConst);
                    profile.Attribute = attributes;
                }

            }

            return profileObj;
        }
        #endregion

        #region --UpdateProfile--
        /// <summary>
        /// Updates the profile.
        /// </summary>
        /// <param name="updatedProfileData">The updated profile data.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> UpdateProfile(ProfileDTO updatedProfileData)
        {
            Task<HttpResponseMessage> response;
            HttpResponseMessage result;
            //HttpClient Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
            using (var client = new HttpClient())
            {

                string uri = ConfigurationManager.AppSettings[Constants.ApiGatewayURL] + ConfigurationManager.AppSettings[Constants.ProfileURI];
                //The GetAsync method sends the HTTP GET request; The await keyword suspends execution until the asynchronous method completes
                response = client.PutAsJsonAsync(uri, updatedProfileData);
                result = await response.ConfigureAwait(false);
                return result;
            }
        }
        #endregion
    }
}
