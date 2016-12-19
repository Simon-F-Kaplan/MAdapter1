namespace MessageAdapter.Profile
{
    #region --using--
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion
  public class Constants
    {
        #region --Profile--
        public const string ProfileNode = "profile";
        public const string PersonGuid = "personGuid";
        public const string MasterGuid = "masterGuid";
        public const string PersonId = "personId";
        public const string MessageCreatedDate = "messageCreatedDate";
        public const string MessageType = "messageType";
        public const string DateOfBirth = "dateOfBirth";
        public const string PreferredQualificationField = "preferredQualification";
        public const string OffersPromotions = "receiveOffersAndPromotions";
        public const string TermsAndConditions = "termsAndConditions";
        public const string Sender = "sender";
        public const string Originator = "originator";
        public const string ProfileUpdated = "ProfileUpdated";
        public const string DOB = "DOB";
        public const string Country = "country";
        public const string ProfileCountry = "Country";
        public const string InstituteRegNo = "instituteRegNo";
        public const string ProfileInstituteRegNo = "InstituteRegnNo";
        public const string ProfilePreferredProduct = "PreferredProduct";
        public const string ProfileOffersPromotions = "OffersPromotions";
        public const string ProfileTermsAndConditions = "TermsAndConditions";
        public const string ApiGatewayURL = "ApiGatewayURL";
        public const string ProfileURI = "ProfileURI";
        public const string UpdatedByQueryString = "UpdatedByQueryString";
        public static string SuccessStatusCode = "200";
        public const string FalseConst = "False";
        #endregion

        #region --Config Keys--
        public static string NavNSLUserServiceDomain = "NavNSLUserServiceDomain";
        public static string NavNSLUserServiceUserName = "NavNSLUserServiceUserName";
        public static string NavNSLUserServicePassword = "NavNSLUserServicePassword";
        public static string NavNSLUserServiceURL = "NavNSLUserServiceURL";
        #endregion

        #region --Tracing Constants--
        /// <summary>
        /// The web SVC trace switch
        /// </summary>
        public static string webSvcTraceSwitch = "WebSvcTraceSwitch";

        /// <summary>
        /// The web service trace
        /// </summary>
        public static string WebServiceTrace = "Web Service Trace";

        /// <summary>
        /// The trace file location
        /// </summary>
        public static string traceFileLocation = "TracFileLocation";

        /// <summary>
        /// The database connection
        /// </summary>
        public static string DBConnection = "LocalSqlServer";

        /// <summary>
        /// The exception details
        /// </summary>
        public static string ExceptionCaught = "Exception caught: ";
        /// <summary>
        /// Tracing Enable status
        /// </summary>
        public static string IsTracingEnabled = "IsTracingEnabled";
        #endregion
    }
}
