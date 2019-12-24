using System;

namespace SitefinityAccelerator.Models.Base
{
    public abstract class DynamicContentModelBase
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public Guid Owner { get; set; }
        public DateTime PublicationDate { get; set; }
        public string UrlName { get; set; }
        public string[] Urls { get; set; }
    }
}