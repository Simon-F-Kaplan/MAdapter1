namespace MessageAdapter
{
    #region -Using--
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion
    public class Constants
    {
        #region --Person--
        public const string PersonNode = "person";
        public const string PersonGuid = "personGuid";
        public const string MasterGuid = "masterGuid";
        public const string PersonId = "personId";
        public const string FirstName = "firstName";
        public const string LastName = "lastName";
        public const string Title = "title";
        public const string MessageCreatedDate = "messageCreatedDate";
        public const string MessageType = "messageType";
        public const string Sender = "sender";
        public const string Emails = "emails";
        public const string Email = "email";
        public const string Type = "type";
        public const string Address = "address";
        public const string Addresses = "addresses";
        public const string Number = "number";
        public const string ID = "id";
        public const string ShipToName = "shipToName";
        public const string IsPrimary = "isPrimary";
        public const string CompanyName = "companyName";
        public const string Country = "country";
        public const string City = "city";
        public const string Line1 = "line1";
        public const string Line2 = "line2";
        public const string Line3 = "line3";
        public const string RegionCode = "regionCode";
        public const string PostalCode = "postalCode";
        public const string Phones = "phones";
        public const string Phone = "phone";
        public const string Originator = "originator";
        public const string ApiGatewayURL = "ApiGatewayURL";
        public const string CreatePersonURI = "CreatePersonURI";
        public const string UpdatePersonURI = "UpdatePersonURI";
        public const string UpdatedByQueryString = "UpdatedByQueryString";
        public const string CreatedByQueryString = "CreatedByQueryString";
        public const string RePublishQueryString = "&republish=";
        public const string RePublish = "RePublish";
        public const string GetPersonURI = "GetPersonURI";
        public const string AddressServicePostUri = "AddressServicePostUri";
        public const string AuthorizationScheme = "AuthorizationScheme";
        public const string AuthorizationParameter = "AuthorizationParameter";
        #endregion

        #region --Config Keys--
        public static string NavNSLUserServiceDomain = "NavNSLUserServiceDomain";
        public static string NavNSLUserServiceUserName = "NavNSLUserServiceUserName";
        public static string NavNSLUserServicePassword = "NavNSLUserServicePassword";
        public static string NavNSLUserServiceURL = "NavNSLUserServiceURL";
        public static string SuccessStatusCode = "200";
        public static string DefaultOriginator = "DefaultOriginator";
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
