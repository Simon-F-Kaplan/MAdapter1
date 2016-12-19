using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using MessageAdapter.Profile;
using System.Collections.Generic;
using MessageAdapter.Models;
using System.Collections;


namespace MessageAdapter.Tests
{
    [TestClass]
    public class UnitTest1
    {

        #region --Test Methods--

        #region --Send NAV Methods--
        /// <summary>
        /// Tests the send person to nav.
        /// </summary>
        [TestMethod]
        public void TestSendPersonToNAV()
        {
            XDocument xml;
            using (StreamReader sr = new StreamReader("person.xml", true))
            {
                xml = XDocument.Load(sr);
            };

            Person p = new Person();
            string xmlString = xml.ToString();
            bool isSuccess = p.SendPersonToNAV(xmlString);
            Assert.IsTrue(isSuccess);
        }
        /// <summary>
        /// Tests the send nav profile.
        /// </summary>
        [TestMethod]
        public void TestSendNavProfile()
        {
            XDocument xml;
            using (StreamReader sr = new StreamReader("NAVFailedProfile.xml", true))
            {
                xml = XDocument.Load(sr);
            };

            MessageAdapter.Profile.Profile p = new MessageAdapter.Profile.Profile();
            string xmlString = xml.ToString();
            bool isSuccess = p.SendProfileToNAV(xmlString);
            Assert.IsTrue(isSuccess);
        }
        #endregion

        #region --Send Service Methods--
        /// <summary>
        /// Tests the send service profile.
        /// </summary>
        [TestMethod]
        public void TestSendServiceProfile()
        {
            XDocument xml;
            using (StreamReader sr = new StreamReader("NAVProfile404.xml", true))
            {
                xml = XDocument.Load(sr);
            };

            MessageAdapter.Profile.Profile p = new MessageAdapter.Profile.Profile();
            string xmlString = xml.ToString();
            bool isSuccess = p.SendProfileToService(xmlString);
            Assert.IsTrue(isSuccess);
        }

        /// <summary>
        /// Tests the send personto service.
        /// </summary>
        [TestMethod]
        public void TestSendPersontoService()
        {
            XDocument xml;
            using (StreamReader sr = new StreamReader("NAVPersonStageFailed.xml", true))
            {
                xml = XDocument.Load(sr);
            };

            Person p = new Person();
            string xmlString = xml.ToString();

            bool isSuccess = p.SendPersonToService(xmlString);
            Assert.IsTrue(isSuccess);
        }
        #endregion

        #region --Test Person/Address Services--
        /// <summary>
        /// Tests the create person.
        /// </summary>
        [TestMethod]
        public void TestCreatePerson()
        {
            PersonDTO person = new PersonDTO();
            PersonEmailDTO email = new PersonEmailDTO();
            PersonPhoneDTO phone = new PersonPhoneDTO();
            PersonAddressDTO address = new PersonAddressDTO();
            List<PersonPhoneDTO> phones = new List<PersonPhoneDTO>();
            phone.PhoneType = 1;
            phone.Phone = "9907864345";
            phones.Add(phone);

            address.AddressID = 1234;
            address.AddressNumber = 1;
            address.AddressType = 1;
            address.IsPrimary = true;
            address.ShipToName = "TestUser";

            List<PersonAddressDTO> addres = new List<PersonAddressDTO>();
            addres.Add(address);
            List<PersonEmailDTO> emails = new List<PersonEmailDTO>();
            email.Email = "testcase@gmail.com";

            List<EmailTypes> emailtype = new List<EmailTypes>();
            EmailTypes type = (EmailTypes)Enum.ToObject(typeof(EmailTypes), 1);
            email.EmailType = type;
            emails.Add(email);

            emailtype.Add(type);

            person.Emails = emails;
            person.PersonID = 8234;
            person.PersonGuid = Guid.Parse("f2f224db-2c8a-456f-b025-f5955cd16051");
            person.MasterGuid = Guid.Parse("f2f224db-2c8a-456f-b025-f5955cd16051");

            person.Addresses = addres;
            person.Phones = phones;
            person.FirstName = "TestUser";
            person.MiddleName = "TestCase";
            person.LastName = "Kaplan123";
            person.Title = "Ms";
            string createdBy = "TestUser";
            bool Republish = false;
            var result = MessageAdapter.Utilities.Utility.CreatePersonAsync(person, createdBy,Republish).Result;
         
        }


        /// <summary>
        /// Tests the update person.
        /// </summary>
        [TestMethod]
        public void TestUpdatePerson()
        {
            Guid guid = new Guid("2E5DA098-91D6-40C4-95BC-98B206F8A36F");
            string updatedBy = "TsetUserupdate";
            PersonDTO person = new PersonDTO();
            PersonEmailDTO email = new PersonEmailDTO();
            PersonPhoneDTO phone = new PersonPhoneDTO();
            PersonAddressDTO address = new PersonAddressDTO();
            List<PersonPhoneDTO> phones = new List<PersonPhoneDTO>();
            phone.PhoneType = 1;
            phone.Phone = "9907864345";
            phones.Add(phone);

            address.AddressID = 1234;
            address.AddressNumber = 1;
            address.AddressType = 1;
            address.IsPrimary = true;
            address.ShipToName = "TestUser";

            List<PersonAddressDTO> addres = new List<PersonAddressDTO>();
            addres.Add(address);
            List<PersonEmailDTO> emails = new List<PersonEmailDTO>();
            email.Email = "testcase@gmail.com";
            List<EmailTypes> emailtype = new List<EmailTypes>();
            person.Emails = emails;
            person.PersonID = 8234;

           
            EmailTypes type = (EmailTypes)Enum.ToObject(typeof(EmailTypes), 1);
            email.EmailType = type;
            emails.Add(email);

            emailtype.Add(type);

            person.MasterGuid = Guid.Parse("2E5DA098-91D6-40C4-95BC-98B206F8A36F");
            person.Phones = phones;
            person.Addresses = addres;
            person.FirstName = "TestUserupdate";
            person.MiddleName = "TestCase";
            person.LastName = "Kaplan123";
            person.Title = "Miss";
            bool Republish = false;
            var result = MessageAdapter.Utilities.Utility.UpdatePerson(guid, person, updatedBy,Republish).Result;

        }

        /// <summary>
        /// Tests the get person by person unique identifier.
        /// </summary>
         [TestMethod]
        public void TestGetPersonByPersonGuid()
        {
            Guid guid = new Guid("2E5DA098-91D6-40C4-95BC-98B206F8A36F");
            var result = MessageAdapter.Utilities.Utility.GetPersonByGuid(guid).Result;
        }

         /// <summary>
         /// Tests the create address.
         /// </summary>
         [TestMethod]
         public void TestCreateAddress()
         {
             AddressDTO ad = new AddressDTO();
             ad.AddressID = 0;
             ad.Line1 = "11 Main Street";
             ad.Line2 = "";
             ad.City = "London";
             ad.CountryCode = "UK";
             ad.PostalCode = "SW1A 2EE";
             ad.SuggestionWasSelected = false;
             ad.SuggestionLimit = 2;

             AddressValidationResultDTO result = MessageAdapter.Utilities.Utility.CreateAddress(ad).Result;
             Assert.IsNotNull(result);
         }
        #endregion

        #region --Utility Methods--
        /// <summary>
        /// Tests the get nav person.
        /// </summary>
        [TestMethod]
        public void TestGetNavPerson()
        {
            XDocument xml;
            using (StreamReader sr = new StreamReader("person.xml", true))
            {
                xml = XDocument.Load(sr);
            };

            XDocument Xmlfile = new XDocument(xml);
            var result = MessageAdapter.Utilities.Utility.GetNAVPerson(Xmlfile);
        }

        /// <summary>
        /// Tests the get person.
        /// </summary>
        [TestMethod]
        public void TestGetPerson()
        {
            XDocument xml;
            using (StreamReader sr = new StreamReader("person.xml", true))
            {
                xml = XDocument.Load(sr);
            };
            XDocument personXml = new XDocument(xml);
            var result = MessageAdapter.Utilities.Utility.GetPerson(personXml);
        }
        #endregion

        #endregion
    }
}
