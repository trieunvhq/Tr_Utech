#nullable enable
using System.Collections.Generic;

namespace QRMSWeb.Components.Common.SideBar
{
    public class SidebarItem
    {
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";

        public string Icon { get; set; } = "";
        public List<SubSidebarItem>? Children { get; set; }
        public string? AccountTypeCode { get; set; }
    }

    public class SubSidebarItem
    {
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
    }
}