using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MessageAdapter.Interfaces;

namespace MessageAdapter.AddABook
{
    [Serializable]
    [XmlRoot(ElementName = "KPPortalResourceManager", Namespace = "http://schemas.kaplan.co.uk/messages/2009/03/KPPortalResourceManager")]
    public class KPPortalResourceManager : INavisionMessage
    {
        protected DateTime messageCreatedDate;
        protected string messageId;
        protected List<KPPortalResource> resources;
        public KPPortalResourceManager() { }
        public KPPortalResourceManager(string userId) { UserId = userId; }

        [XmlAttribute(AttributeName = "messageCreatedDate", DataType = "dateTime")]
        public DateTime MessageCreatedDate { get; set; }
        [XmlIgnore]
        public string MessageId { get; }
        [XmlArrayItem(ElementName = "resource", Type = typeof(KPPortalResource))]
        [XmlArray(ElementName = "resourceUpdates")]
        public List<KPPortalResource> Resources { get; set; }
        [XmlAttribute(AttributeName = "userId", DataType = "token")]
        public string UserId { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName ="KPPortalResource")]
    public class KPPortalResource
    {
        protected string id;
        protected bool isDeleted;
        protected string type;

        public KPPortalResource() { }
        public KPPortalResource(string resourceType, string resourceId) { type = resourceType; id = resourceId;  }
        public KPPortalResource(string resourceType, string resourceId, bool resourceIsDeleted) { type = resourceType; id = resourceId; isDeleted = resourceIsDeleted; }

        [XmlAttribute(AttributeName = "id", DataType = "string")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "isDeleted", DataType = "boolean")]
        public bool IsDeleted { get; set; }
        [XmlAttribute(AttributeName = "type", DataType = "string")]
        public string Type { get; set; }
    }
}
