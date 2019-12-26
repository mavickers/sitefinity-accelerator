using System.Linq;
using SitefinityAccelerator.Interfaces;
using SitefinityAccelerator.Models.Base;
using Telerik.Sitefinity.DynamicModules.Model;

namespace SitefinityAccelerator.Mappers
{
    public class DynamicContentToDynamicContentModelBaseMapper : IAugmentingExternalMapper<DynamicContent, DynamicContentModelBase>
    {
        public DynamicContentModelBase Map(DynamicContent augmentingItem, DynamicContentModelBase baseItem)
        {
            if (augmentingItem == null || baseItem == null)
            {
                return baseItem;
            }

            baseItem.Id = augmentingItem.OriginalContentId;
            baseItem.DateCreated = augmentingItem.DateCreated;
            baseItem.LastModified = augmentingItem.LastModified;
            baseItem.Owner = augmentingItem.Owner;
            baseItem.PublicationDate = augmentingItem.PublicationDate;
            baseItem.UrlName = augmentingItem.UrlName.ToString();
            baseItem.Urls = augmentingItem.Urls.Select(u => u.Url).ToArray();

            return baseItem;
        }
    }
}