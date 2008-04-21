/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc, Kimmo Varis <kimmo.varis@ixonos.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

// $Id$

using System;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;

namespace CheckSumTool.Settings
{
    /// <summary>
    /// A helper class for handling settings in the XML config file.
    /// </summary>
    public class SettingUtils
    {
        public SettingUtils()
        {
        }

        /// <summary>
        /// Get setting value from list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Setting GetSettingFromList(List<Setting> list, string name)
        {
            foreach (Setting item in list)
            {
                if(item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Set setting value one of the list items.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <param name="settingValue"></param>
        public static void SetSettingFromList(List<Setting> list, string name,
                int settingValue)
        {
            foreach (Setting item in list)
            {
                if(item.Name == name)
                {
                    item.SetInt(settingValue);
                    break;
                }
            }

            Setting settingItem = new Setting(0, name, settingValue);
            list.Add(settingItem);
        }

        /// <summary>
        /// Set setting value for one of the list items.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <param name="settingValue"></param>
        public static void SetSettingFromList(List<Setting> list, string name,
                string settingValue)
        {
            foreach (Setting item in list)
            {
                if(item.Name == name)
                {
                    item.SetString(settingValue);
                    break;
                }
            }

            Setting settingItem = new Setting(1, name, settingValue);
            list.Add(settingItem);
        }

        /// <summary>
        /// Set setting value for one of the list items.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <param name="settingValue"></param>
        public static void SetSettingFromList(List<Setting> list, string name,
                bool settingValue)
        {
            foreach (Setting item in list)
            {
                if(item.Name == name)
                {
                    item.SetBool(settingValue);
                    break;
                }
            }

            Setting settingItem = new Setting(1, name, settingValue);
            list.Add(settingItem);
        }

        /// <summary>
        /// Get component position and other values.
        /// </summary>
        /// <param name="name"></param>
        public static void GetSetting(List<Setting> list, XPathHandler handler,
                IComponentSetting setting, string name, string type)
        {
            Setting settingName = new Setting(1, "Name", name);
            list.Add(settingName);
            
            string relPath = "";
            switch (type)
            {
                case "form":
                    relPath = "GUI/Forms/";
                    break;
                case "toolbar":
                    relPath = "GUI/Toolbars/";
                    break;
                case "statusbar":
                    relPath = "GUI/Statusbars/";
                    break;
                case "column":
                    relPath = "GUI/MainWindow/Columns/";
                    break;
            }

            string path = "/configuration/" + relPath + type +
                    @"[@name=""$name""]/*".Replace("$name", name);
            XPathNodeIterator iterator = handler.GetComponentSettings(path);
            
            switch (type)
            {
                case "form":
                    SetFormValues((FormSetting) setting, iterator.Clone());
                    SetPositionValues((FormSetting) setting, iterator.Clone());
                    break;
                case "toolbar":
                    SetPositionValues((ToolbarSetting) setting, iterator.Clone());
                    SetVisibleValue((ToolbarSetting) setting, iterator.Clone());
                    break;
                case "statusbar":
                    SetVisibleValue((StatusbarSetting) setting, iterator.Clone());
                    break;
                case "column":
                    SetColumnValues((ColumnSetting) setting, iterator.Clone());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        public static void SaveSetting(XPathHandler handler,
                IComponentSetting setting, string name, string type)
        {
            //TODO: Save first position values and then other.
            switch (type)
            {
                case "form":
                    handler.SaveForm(@"/configuration/GUI/Forms/" + type +
                            @"[@name=""$name""]".Replace("$name", name),
                            (FormSetting) setting);
                    break;
                case "toolbar":
                    handler.SaveToolbar(@"/configuration/GUI/Toolbars/" +
                            type + @"[@name=""$name""]".Replace("$name", name),
                            (ToolbarSetting) setting);
                    break;
                case "statusbar":
                    handler.SaveStatusbar(@"/configuration/GUI/Statusbars/" +
                            type + @"[@name=""$name""]".Replace("$name", name),
                            (StatusbarSetting) setting);
                    break;
                case "column":
                    handler.SaveColumn(@"/configuration/GUI/MainWindow/Columns/" +
                            type + @"[@name=""$name""]".Replace("$name", name),
                            (ColumnSetting) setting);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Setting position info.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="iterator"></param>
        public static void SetPositionValues(IComponentSetting component,
                XPathNodeIterator iterator)
        {
            int x = -1;
            int y = -1;

            while (iterator.MoveNext())
            {
                XPathNavigator navigator = iterator.Current.Clone();

                switch (navigator.Name)
                {
                    case "X":
                         try
                        {
                            x = Convert.ToInt32(navigator.Value);
                        }
                        catch(Exception)
                        {
                            x = -1;
                        }
                        break;
                    case "Y":
                        try
                        {
                            y = Convert.ToInt32(navigator.Value);
                        }
                        catch(Exception)
                        {
                            y = -1;
                        }
                        break;
                    default:
                        break;
                  }
             }
             component.SetPosition(x, y);
        }

        /// <summary>
        /// Setting form specific info.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="iterator"></param>
        public static void SetFormValues(FormSetting component,
                XPathNodeIterator iterator)
        {
            int height = -1;
            int width = -1;

            while (iterator.MoveNext())
            {
                XPathNavigator navigator = iterator.Current.Clone();

                switch (navigator.Name)
                {
                    case "Width":
                         try
                        {
                            width = Convert.ToInt32(navigator.Value);
                        }
                        catch(Exception)
                        {
                            width = -1;
                        }
                        break;
                    case "Height":
                        try
                        {
                            height = Convert.ToInt32(navigator.Value);
                        }
                        catch(Exception)
                        {
                            height = -1;
                        }
                        break;
                    default:
                        break;
                  }
             }

            component.SetValues(width, height);
        }

        /// <summary>
        /// Setting visible value info.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="iterator"></param>
        public static void SetVisibleValue(IComponentSetting component,
                XPathNodeIterator iterator)
        {
            bool visible = false;

            while (iterator.MoveNext())
            {
                XPathNavigator navigator = iterator.Current.Clone();

                switch (navigator.Name)
                {
                    case "Visible":
                        try
                        {
                            visible = Convert.ToBoolean(navigator.Value);
                        }
                        catch(Exception)
                        {
                            visible = false;
                        }
                        break;
                    default:
                        break;
                 }
            }

            if(component is ToolbarSetting)
            {
                ToolbarSetting toolbar = (ToolbarSetting) component;
                toolbar.SetValues(visible);
            }

            if(component is StatusbarSetting)
            {
                StatusbarSetting statusbar = (StatusbarSetting) component;
                statusbar.SetValues(visible);
            }
        }

        /// <summary>
        /// Setting displayIndex value info.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="iterator"></param>
        public static void SetColumnValues(ColumnSetting component,
                XPathNodeIterator iterator)
        {
            int displayIndex = 0;
            int width = 0;

            while (iterator.MoveNext())
            {
                XPathNavigator navigator = iterator.Current.Clone();

                switch (navigator.Name)
                {
                    case "DisplayIndex":
                        try
                        {
                            displayIndex = Convert.ToInt32(navigator.Value);
                        }
                        catch(Exception)
                        {
                            displayIndex = 0;
                        }
                        break;
                    case "Width":
                        try
                        {
                            width = Convert.ToInt32(navigator.Value);
                        }
                        catch(Exception)
                        {
                            width = 0;
                        }
                        break;
                    default:
                        break;
                 }
            }

            component.SetValues(displayIndex, width);
        }
    }
}
