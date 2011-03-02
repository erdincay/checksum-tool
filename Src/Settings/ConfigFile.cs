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

using System;
using System.IO;
using System.Reflection;
using System.Xml.XPath;
using CheckSumTool.Utils;

namespace CheckSumTool.Settings
{
    /// <summary>
    /// A class for XML configuration file handling.
    /// </summary>
    public class ConfigFile
    {
        /// <summary>
        /// Filename of the config file.
        /// </summary>
        const string ConfigFileName = "config.xml";
        /// <summary>
        /// Current configuration file version.
        /// </summary>
        const int CurrentVersion = 1;

        /// <summary>
        /// Full path (including filename) to the configuration file.
        /// </summary>
        string _path;

        /// <summary>
        /// Full path to the user's application data folder.
        /// </summary>
        string _appDataPath;

        /// <summary>
        /// Full path to the applications folder.
        /// </summary>
        string _applicationPath;

        /// <summary>
        /// Get a full path to the XML configuration file.
        /// </summary>
        public string FilePath
        {
            get { return _path; }
        }

        /// <summary>
        /// Constructor, initializes the configuration file path.
        /// </summary>
        /// <param name="AppPath">Application's path.</param>
        /// <param name="AppDataPath">User's Application Data folder.</param>
        public ConfigFile(string AppPath, string AppDataPath)
        {
            _applicationPath = AppPath;
            _appDataPath = AppDataPath;
            SetConfigFilePath();
        }

        /// <summary>
        /// Formats a full path (including filename) for the XML
        ///  configuration file.
        /// </summary>
        private void SetConfigFilePath()
        {
            if (!Directory.Exists(_appDataPath))
                Directory.CreateDirectory(_appDataPath);
            _path = _appDataPath + FileUtils.PathSeparator + ConfigFileName;
        }

        /// <summary>
        /// Checks if the configuration file exists.
        /// </summary>
        /// <returns>true if the file exists, false otheriwse.</returns>
        public bool FileExists()
        {
            return File.Exists(_path);
        }

        /// <summary>
        /// Creates a default configuration XML file.
        /// </summary>
        /// <returns>true if succeeded, false otherwise.</returns>
        public bool CreateDefaultFile()
        {
            // Don't overwrite existing file.
            if (FileExists())
                return false;

            // Create a folder if it does not exist
            string folder = Path.GetDirectoryName(_path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            bool success = false;
            if (!File.Exists(_path))
            {
                string defFile = FileUtils.FromNativeSeparators(_applicationPath);
                //defFile += Path.Combine(defFile, "config.xml");
                defFile += FileUtils.PathSeparator;
                defFile += ConfigFileName;
                try
                {
                    File.Copy(defFile, _path);
                    success = true;
                }
                catch (FileNotFoundException)
                {
                }
                catch (DirectoryNotFoundException)
                {
                }
                catch (IOException)
                {
                }
            }
            return success;
        }

        /// <summary>
        /// Check if the configuration file's version number is known version.
        /// </summary>
        /// <returns>true if the version is compatible, false otherwise.</returns>
        public bool IsCompatibleVersion()
        {
            if (FileExists())
            {
                string xpath = "/configuration[@version]";
                XPathDocument doc = new XPathDocument(_path);
                XPathNavigator nav = doc.CreateNavigator();
                XPathExpression expr = nav.Compile(xpath);
                XPathNodeIterator iterator = nav.Select(expr);
                iterator.MoveNext();

                string verStr = iterator.Current.GetAttribute("version", "");
                if (Convert.ToInt32(verStr) == CurrentVersion)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Replace current config file with default config file.
        /// </summary>
        /// <returns>true if succeeded, false otherwise.</returns>
        public bool ReplaceWithDefault()
        {
            if (FileExists())
            {
                File.Delete(_path);
            }

            return CreateDefaultFile();
        }
    }
}
