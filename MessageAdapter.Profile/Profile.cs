namespace MessageAdapter.Profile
{
    #region --using--
    using System.Configuration;
    using System.Net.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using System.Net;
    using Newtonsoft.Json;
    using MessageAdapter.Profile.ErrorTracing;
    using NAVNSLUser;
    #endregion
    public class Profile
    {
        #region --Private variables--
        /// <summary>
        /// The nav NSL user port client
        /// </summary>
        private NSLUser navNSLUserPortClient;
        #endregion

        #region --Constructor--
        public Profile()
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
        public bool SendProfileToNAV(string xml)
        {
            bool result = true;
            StringBuilder traceInfo = new StringBuilder();
            traceInfo.Append("---Send Profile To NAV -> Call Received----\n\r");
            traceInfo.Append(" Time:" + DateTime.Now + "----\n\r");

            try
            {
                //convert the string to xml object
                XDocument xmlDoc = XDocument.Parse(xml);
              List<profile> profileObj = Utility.GetNAVProfile(xmlDoc);
               
                if (profileObj != null && profileObj.Count > 0)
                {
                    foreach (profile profile in profileObj)
                    {
                        string responseID = string.Empty;
                        string responseText = string.Empty;
                        navNSLUserPortClient.Profile(profile, ref responseID, ref responseText);
                        if (!responseID.Equals(Constants.SuccessStatusCode))
                        {
                            result = false;
                        }
                        traceInfo.Append("---Result received from NAV:" + responseID + "Response:" + responseText + " Time:" + DateTime.Now + "----\n\r");
                    }
                }
                else
                {
                    result = false;
                    traceInfo.Append("---Failed to read xml----\n\r");
                }
                FileErrorTracing.Log(traceInfo.ToString());
            }
            catch (Exception ex)
            {
                result = false;
                traceInfo.Append(ex.Message + string.Empty + ex.StackTrace);
                FileErrorTracing.Log(traceInfo.ToString());
            }
            return result;
        }


        /// <summary>
        /// Sends the profile to service.
        /// </summary>
        /// <param name="xml">The XML.</param>
        public bool SendProfileToService(string xml)
        {
            bool result = true;
            StringBuilder traceInfo = new StringBuilder();
            traceInfo.Append("---Send Profile To ProfileService -> Call Received----\n\r");
            traceInfo.Append(" Time:" + DateTime.Now + "----\n\r");
            try
            {
                //convert the string to xml object
                XDocument xmlDoc = XDocument.Parse(xml);

                List<ProfileDTO> profileObj = Utility.GetProfile(xmlDoc);
                if (profileObj != null && profileObj.Count > 0)
                {
                    foreach (ProfileDTO profile in profileObj)
                    {
                        var response = Utility.UpdateProfile(profile).Result;
                        if(response.StatusCode != HttpStatusCode.OK)
                        {
                            result = false;
                            string error = response.Content.ReadAsStringAsync().Result;
                            traceInfo.Append("---Error received from Service:" + error + " Time:" + DateTime.Now + "----\n\r");
                        }
                        traceInfo.Append("---Result received from Service:" + response + " Time:" + DateTime.Now + "----\n\r");
                    }
                }
                else
                {
                    result = false;
                    traceInfo.Append("---Failed to read xml----\n\r");
                }
                FileErrorTracing.Log(traceInfo.ToString());
            }
            catch (Exception ex)
            {
                result = false;
                traceInfo.Append(ex.Message + string.Empty + ex.StackTrace);
                FileErrorTracing.Log(traceInfo.ToString());
            }
            return result;
        }
        #endregion
    }
}


