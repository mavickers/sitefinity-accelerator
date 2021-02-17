using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using SitefinityAccelerator.Configuration.Security.Widgets;
using SitefinityAccelerator.Interfaces;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Security;

namespace SitefinityAccelerator.Filters
{
    /// <summary>
    /// Filter toolbox items based on if the widget has been designated for use
    /// by any groups the current user belongs to.
    /// </summary>
    public class UserRoleToolboxItemFilter : IToolboxItemFilter
    {
        private readonly ConfigElementList<WidgetSecurityElement> _settings;
        private readonly Guid _userId;
        private readonly RoleManager _appRoleManager;

        public UserRoleToolboxItemFilter
        (
            ConfigElementList<WidgetSecurityElement> settings,
            [Dependency("UserId")] Guid userId,
            [Dependency("AppRoles")] RoleManager appRoleManager
        )
        {
            _settings = settings;
            _userId = userId;
            _appRoleManager = appRoleManager;
        }

        public bool IsEnabled(KeyValuePair<string, ToolboxItem> toolboxItem, Page page)
        {
            // first check to see if there is a toolbox entry in the settings; if not then
            // consider it enabled.

            var widgetConfig = _settings.Elements.FirstOrDefault(el => el.Widget == toolboxItem.Key);

            if (widgetConfig == null)
            {
                return true;
            }

            var roleNames = widgetConfig.SecurityRoles?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(role => role.Trim()) ?? new string[] { };

            // no roles, no access
            if (!roleNames.Any())
            {
                return false;
            }

            // if there is an asterisk in the roles list, access for everybody - ignore
            // any other values

            if (roleNames.Any(r => r == "*"))
            {
                return true;
            }

            // otherwise let's see if there is match

            foreach (var roleName in roleNames)
            {
                if (_appRoleManager.RoleExists(roleName) && _appRoleManager.IsUserInRole(_userId, roleName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}