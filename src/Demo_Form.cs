using CADLib;
using CADLibKernel;
using CdeLib;
using LightweightDataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IFC_PLUGIN.CADLib_data;

namespace IFC_PLUGIN
{
    public partial class Demo_Form : Form, ICADLibPlugin
    {
        private const string dir_cadlib_output = @"C:\Windows\Temp";
        private const string file_cadlib_output = @"C:\Windows\Temp\cadlib_output.txt";
        
        //dir_cadlib_output


        public Demo_Form()
        {
            InitializeComponent();
        }
        public MenuStrip GetMenu()
        {
            return this.menuStrip_IFC;
        }

        public ToolStripContainer GetToolbars()
        {
            //no control panel
            return null;
        }

        public void TrackInterfaceItems(InterfaceTracker tracker)
        {
            tracker.Add(new InterfaceItemState(ClassIFCToolStripMenuItem, LibConnectionState.Connected, LibFolderState.DoesNotMatter, LibObjectState.DoesNotMatter, LibRequiredPermission.Admin));
        }

    }
}
