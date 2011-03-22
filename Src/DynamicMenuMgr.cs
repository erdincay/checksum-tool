// Original code/idea from article at:
// http://www.codeguru.com/csharp/csharp/cs_misc/designtechniques/article.php/c15661__2/Create-a-Dynamic-Menu-Using-C.htm

using System;
using System.Windows.Forms;

namespace CheckSumTool
{
    public class DynamicMenuMgr
    {
        MenuItem _anchor;
        MenuItem _separator;
        DynamicMenuType _dynamicMenuType;
        ItemInsertMode _itemInsertMode;
        int _maxItems;
        int _itemCount;

        public enum ItemInsertMode
        {
            Prepend,
            Append
        };

        public enum DynamicMenuType
        {
            Inline,
            Submenu
        };

        // Constructor.
        public DynamicMenuMgr(MenuItem anchor, MenuItem separator,
           DynamicMenuType dynamicMenuType,
           ItemInsertMode itemInsertMode, int maxItems)
        {
            _anchor = anchor;
            _separator = separator;
            _itemInsertMode = itemInsertMode;
            _dynamicMenuType = dynamicMenuType;
            _maxItems = maxItems > 0 ? maxItems : 4;
            _itemCount = 0;
            // DynamicMenuType is inline.
            if (_dynamicMenuType == DynamicMenuType.Inline)
            {
                // Hide _anchor and _separator if dynamic menus will be inline.
                _anchor.Visible = false;
                if (_separator != null)
                    _separator.Visible = false;
            }
            // DynamicMenuType is submenu.
            else
            {
                // Make _anchor visible as it will be the parent menu item
                // of dynamic menu items in a submenu.
                _anchor.Visible = true;
                // Disable _anchor as there is no menu item in the submenu
                // initially.
                _anchor.Enabled = false;
                // _separator should be visible if there is one.
                if (_separator != null)
                    _separator.Visible = true;
            }
        }
        // Another constructor with maxItems defaults to 4.
        public DynamicMenuMgr(MenuItem anchor, MenuItem separator,
           DynamicMenuType DynamicMenuType, ItemInsertMode ItemInsertMode)
            : this(anchor, separator, DynamicMenuType, ItemInsertMode, 4)
        {
        }

        // Dynamic menu item clicked event handler.
        private void MenuItemClick(object sender, EventArgs e)
        {
            DynamicMenuItem item = (DynamicMenuItem)sender;
            MessageBox.Show(item.Data);
        }

        // Add a group of dynamic menu items.
        public void AddMenuItems(DynamicMenuItem.DynamicMenuItemTextData[]
                                 textDataCollection)
        {
            foreach (DynamicMenuItem.DynamicMenuItemTextData textData
                     in textDataCollection)
                AddMenuItem(textData.MenuText, textData.MenuData);
        }

        // Add one dynamic menu item with menu text being paramter menuText
        // and menu data being parameter data.
        public virtual void AddMenuItem(string menuText, string data)
        {
            Menu.MenuItemCollection menuItems;
            DynamicMenuItem menuItem = new DynamicMenuItem(menuText,
               data, this.MenuItemClick);

            switch (_dynamicMenuType)
            {
                case DynamicMenuType.Inline:
                    menuItems = _anchor.Parent.MenuItems;
                    AddMenuItemInline(menuItem, menuItems);
                    break;
                case DynamicMenuType.Submenu:
                    menuItems = _anchor.MenuItems;
                    AddMenuItemInSubmenu(menuItem, menuItems);
                    break;
                default:
                    break;
            }

        }

        // Add inline dynamic menu item. Parameter menuItems contains
        // existing inline dynamic menu items.
        private void AddMenuItemInline(MenuItem menuItem,
           Menu.MenuItemCollection menuItems)
        {
            int anchorIndex = _anchor.Index;
            switch (_itemInsertMode)
            {
                case ItemInsertMode.Append:
                    if (_itemCount == _maxItems)
                    {
                        menuItems.RemoveAt(anchorIndex + 1);
                        _itemCount--;
                    }
                    menuItems.Add(anchorIndex + _itemCount + 1, menuItem);
                    break;

                case ItemInsertMode.Prepend:
                    if (_itemCount == _maxItems)
                    {
                        menuItems.RemoveAt(anchorIndex + _maxItems);
                        _itemCount--;
                    }
                    menuItems.Add(anchorIndex + 1, menuItem);
                    break;
                default:
                    break;
            }
            _itemCount++;
            _separator.Visible = true;
        }

        // Add dynamic menu item in a submenu. Parameter menuItems
        // contains existing menu items in the submenu.
        private void AddMenuItemInSubmenu(MenuItem menuItem,
           Menu.MenuItemCollection menuItems)
        {
            switch (_itemInsertMode)
            {
                case ItemInsertMode.Append:
                    if (_itemCount == _maxItems)
                    {
                        menuItems.RemoveAt(0);
                        _itemCount--;
                    }
                    menuItems.Add(menuItem);
                    break;

                case ItemInsertMode.Prepend:
                    if (_itemCount == _maxItems)
                    {
                        menuItems.RemoveAt(_maxItems - 1);
                        _itemCount--;
                    }
                    menuItems.Add(0, menuItem);
                    break;
                default:
                    break;
            }
            _itemCount++;
            _anchor.Enabled = true;
        }
    }
}
