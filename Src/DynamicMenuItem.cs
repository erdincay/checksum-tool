// Original code/idea from article at:
// http://www.codeguru.com/csharp/csharp/cs_misc/designtechniques/article.php/c15661__2/Create-a-Dynamic-Menu-Using-C.htm

using System;
using System.Windows.Forms;

namespace CheckSumTool
{
    public class DynamicMenuItem : MenuItem
    {
        private string _data;

        // Dynamic menus information, i.e. menu text and menu data are
        // normally retrieved from some source like a database or
        // Registry. Putting them into an array of DynamicMenuItemTextData
        // struct will make it convenient to prepare for creating a group
        // of dynamic menu items through the dynamic menu manager.
        public struct DynamicMenuItemTextData
        {
            public string MenuText { get; set; }
            public string MenuData { get; set; }

            public DynamicMenuItemTextData(string menuText, string menuData)
            {
                MenuText = menuText;
                MenuData = menuData;
            }
        }

        public string Data { get; set; }

        public DynamicMenuItem(string text, string data,
                               System.EventHandler eventHandler)
            : base(text)
        {
            Data = data;
            // Add menu item clicked event handler when it is created.
            this.Click += new System.EventHandler(eventHandler);
        }
    }
}
