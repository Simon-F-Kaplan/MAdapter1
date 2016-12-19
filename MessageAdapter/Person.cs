namespace MessageAdapter
{
    #region --Using--
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using System.Net;
    using System.Configuration;
    using MessageAdapter.Models;
    using MessageAdapter.Utilities;
    using MessageAdapter.ErrorTracing;
    using NAVNSLUser;
    using MessageAdapter.Utilities;
    #endregion
    public class Person
    {
        #region --Constants--
        /// <summary>
        /// The create d_ type - MessageType
        /// </summary>
        public static string CREATED_TYPE = "PersonCreated";
        /// <summary>
        /// The update d_ type - MessageType
        /// </summary>
        public static string UPDATED_TYPE = "PersonUpdated";

        #endregion

        #region --Private variables--

        /// <summary>
        /// The nav NSL user port client
        /// </summary>
        private NSLUser navNSLUserPortClient;
        #endregion

        #region --Constructor--
        public Person()
        {
            navNSLUserPortClient = new NSLUser();

            navNSLUserPortClient.Url = ConfigurationManager.AppSettings[Constants.NavNSLUserServiceURL];
            NetworkCredential networkCredential = new NetworkCredential();
            networkCredential.Domain = ConfigurationManager.AppSettings[Constants.NavNSLUserServiceDomain];
            networkCredential.UserName = ConfigurationManager.AppSettings[Constants.NavNSLUserServiceUserName];
            networkCredential.Password = ConfigurationManager.AppSettings[Constants.NavNSLUserServicePassword];
            navNSLUserPortClient.Credentials = networkCredential;

        }
        #endregion

        #region --Methods--
        /// <summary>
        /// Sends the person to nav.
        /// </summary>
        /// <param name="xml">The XML.</param>
        public bool SendPersonToNAV(string xml)
        {
            StringBuilder traceInfo = new StringBuilder();
            traceInfo.Append("---Send Person To NAV -> Call Received----\n\r");
            traceInfo.Append(" Time:" + DateTime.Now + "----\n\r");
            bool isSuccess = true;
            try
            {
                //convert the string to xml object
                XDocument xmlDoc = XDocument.Parse(xml);

                List<person> personObj = Utility.GetNAVPerson(xmlDoc);
                if (personObj != null && personObj.Count > 0)
                {
                    foreach (person person in personObj)
                    {
                        string responseID = string.Empty;
                        string responseText = string.Empty;
                        navNSLUserPortClient.Person(person, ref responseID, ref responseText);
                        if(!responseID.Equals(Constants.SuccessStatusCode))
                        {
                            isSuccess = false;
                        }
                        traceInfo.Append("---Result received from NAV:" + responseID + "Response:" + responseText + " Time:" + DateTime.Now + "----\n\r");
                    }
                }
                else
                {
                    isSuccess = false;
                    traceInfo.Append("---Failed to read xml----\n\r");
                }
                FileErrorTracing.Log(traceInfo.ToString());
            }
            catch (Exception ex)
            {
                isSuccess = false;
                traceInfo.Append(ex.Message + string.Empty + ex.StackTrace);
                FileErrorTracing.Log(traceInfo.ToString());
            }
            return isSuccess;
        }


        /// <summary>
        /// Sends the person to service.
        /// </summary>
        /// <param name="xml">The XML.</param>
        public bool SendPersonToService(string xml)
        {
            StringBuilder traceInfo = new StringBuilder();
            traceInfo.Append("---Send Person To Service -> Call Received----\n\r");
            traceInfo.Append(" Time:" + DateTime.Now + "----\n\r");
            bool isSuccess = true;
            try
            {
                bool republish = Convert.ToBoolean(ConfigurationManager.AppSettings[Constants.RePublish]);

                //convert the string to xml object
                XDocument xmlDoc = XDocument.Parse(xml);
                string messageType = string.Empty;
                string originator = Utility.GetMessageTypeAndOriginator(xmlDoc, out messageType);
                List<PersonDTO> personObj = Utility.GetPerson(xmlDoc);
                if (personObj != null && personObj.Count > 0)
                {
                    foreach (PersonDTO person in personObj)
                    {
                        if (person.PersonGuid != null)
                        {
                            PersonDTO personDetails = Utility.GetPersonByGuid(person.PersonGuid.Value).Result;
                            if(personDetails != null && personDetails.PersonGuid != null && personDetails.PersonGuid.HasValue)
                            {
                                messageType = UPDATED_TYPE;
                            }
                            else
                            {
                                messageType = CREATED_TYPE;
                            }

                            if (messageType.Equals(CREATED_TYPE, StringComparison.InvariantCultureIgnoreCase))
                            {
                                var result = Utility.CreatePersonAsync(person, originator, republish).Result;
                                if (result.StatusCode != HttpStatusCode.Created)
                                {
                                    isSuccess = false;
                                    string res = result.Content.ReadAsStringAsync().Result;
                                    traceInfo.Append("---Error received from Service:" + res + " Time:" + DateTime.Now + "----\n\r");
                                }
                                traceInfo.Append("---Result received from Service:" + result + " Time:" + DateTime.Now + "----\n\r");
                            }
                            if (messageType.Equals(UPDATED_TYPE, StringComparison.InvariantCultureIgnoreCase))
                            {
                                PersonDTO updatedPerson = Utility.UpdatePerson(person.PersonGuid.Value, person, originator, republish).Result;
                                if (updatedPerson != null && updatedPerson.PersonGuid != null)
                                {
                                    traceInfo.Append("---Result received from Service: Updation Successful. Time:" + DateTime.Now + "----\n\r");
                                }
                                else
                                {
                                    isSuccess = false;
                                    traceInfo.Append("---Result received from Service: Updation Failed. Time:" + DateTime.Now + "----\n\r");
                                }
                            }
                        }
                        else
                        {
                            traceInfo.Append("---Person Guid is null. Time:" + DateTime.Now + "----\n\r");
                        }
                    }
                }
                else
                {
                    isSuccess = false;
                    traceInfo.Append("---Failed to read xml----\n\r");
                }
                FileErrorTracing.Log(traceInfo.ToString());
            }
            catch (Exception ex)
            {
                isSuccess = false;
                traceInfo.Append(ex.Message + string.Empty + ex.StackTrace);
                FileErrorTracing.Log(traceInfo.ToString());
            }
            return isSuccess;
        }

        #endregion
    }
}
