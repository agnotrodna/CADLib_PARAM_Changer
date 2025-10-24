using CADLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IFC_PLUGIN
{
    /// <summary>
    /// Ресурсы CADLib при регистрации плагина
    /// </summary>
    internal static class CADLib_data
    {
        public static Form m_mainForm;
        public static IDatabaseBrowser m_mainDBBrowser;
        public static CADLibrary m_library;
    }
}
