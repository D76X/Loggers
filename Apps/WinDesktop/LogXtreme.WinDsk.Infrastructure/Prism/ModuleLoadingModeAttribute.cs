using System;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    /// <summary>
    /// Refs
    /// https://app.pluralsight.com/player?course=prism-loading-modules-user-roles&author=brian-lagunas&name=prism-loading-modules-user-roles-m2&clip=7&mode=live
    /// </summary>

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleLoadingModeAttribute : Attribute {

        public string[] Roles { get; set; }

        public ModuleLoadingModeAttribute(params string[] roles) {
            this.Roles = roles;
        }
    }
}
