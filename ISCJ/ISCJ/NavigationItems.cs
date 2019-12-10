using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISCJ
{
    public class NavigationBar
    {
        public List<Section> Sections { get; set; }
    }

    public class Section
    {
        public Section(string caption, List<SectionItem> sectionItems)
        {
            Caption = caption;
            IsActive = sectionItems.Count(x => x.IsActive == true) > 0;
            SectionItems = sectionItems;
        }


        public string Caption { get; }
        public bool IsActive { get; }
        public List<SectionItem> SectionItems { get; }
    }

    public class SectionItem
    {
        public SectionItem(string caption, bool isActive, string url)
        {
            Caption = caption;
            IsActive = isActive;
            Url = url;
        }


        public string Caption { get; }
        public bool IsActive { get; }
        public string Url { get; }
    }
}
