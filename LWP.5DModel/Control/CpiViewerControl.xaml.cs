using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RIB.Visual.Components.Wpf;
using RIB.Visual.Components.CPI;
using System.Reflection;
using System.IO;

namespace LWP._5DModel.Control
{
    /// <summary>
    /// Interaction logic for CpiViewerControl.xaml
    /// </summary>
    public partial class CpiViewerControl : UserControl
    {
        private Dictionary<string, Assembly> _resolvedAssemblies = new Dictionary<string, Assembly>();
        private Rib3DViewer _currentViewerControl;
        public ICpiObjects cpiObjects;

        public CpiViewerControl(ICpiObjects cpiObjects)
        {
            InitializeComponent();
            this.cpiObjects = cpiObjects;
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            _currentViewerControl = new Rib3DViewer("myGlobalModel");
            

            _currentViewerControl.StartTransferObjects();
            _currentViewerControl.TransferCpiObjects(cpiObjects);
            _currentViewerControl.EndTransferObjects();

            
            _currentViewerControl.ShowToolBar(true);
            _currentViewerControl.SetWiredMode(false);
            _currentViewerControl.SetSolidMode(true);
            _currentViewerControl.SetGrid(true);
            _currentViewerControl.ProjectGridsVisible(false);

            _currentViewerControl.Refresh();
            _currentViewerControl.SetHome();
            _currentViewerControl.ZoomToVisible();
            //_currentViewerControl.Selection();
            _currentViewerControl.Orbit();
           
            _currentViewerControl.onObjectSelectionChanged += new Rib3DViewer.SelectionChangedHandler(selectionEvent);

            MainGrid.Children.Add(_currentViewerControl);

        }

        public void SetViewModeTo(ViewMode mode)
        {
            switch (mode)
            {
                case ViewMode.Pan:
                    _currentViewerControl.Pan();
                    break;
                case ViewMode.Orbit:
                    _currentViewerControl.Orbit();
                    break;
                case ViewMode.Walk:
                    _currentViewerControl.WalkThrough();
                    break;
                case ViewMode.Selection:
                    _currentViewerControl.Selection();
                    break;
            }
        }

        public void SetHome()
        {
            _currentViewerControl.SetHome();
        }

        private void selectionEvent(object sender, SelectionChangedArgs e)
        {
            var str = "";
            if (e.SelectedIds != null)
                e.SelectedIds.ForEach(x => str += x);
            MessageBox.Show(str);
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string name = new AssemblyName(args.Name).Name;
            Assembly assembly = null;

            if (_resolvedAssemblies.TryGetValue(name, out assembly))
            {
                System.Diagnostics.Trace.WriteLine(string.Format("{0} already loaded", args.Name));
            }
            else
            {
                System.Diagnostics.Trace.WriteLine(string.Format("Try resolving {0}", args.Name));
                string appMode = IntPtr.Size == 4 ? "x32" : "x64";
                string appFile = name + ".dll";
                string lookup1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, System.IO.Path.Combine(appMode, appFile));
                string lookup2 = System.IO.Path.Combine(System.Environment.CurrentDirectory, System.IO.Path.Combine(appMode, appFile));
                string lookup3 = System.IO.Path.Combine(System.Environment.CurrentDirectory, appFile);

                try
                {
                    if (File.Exists(lookup1))
                        assembly = Assembly.LoadFrom(lookup1);
                    else if (File.Exists(lookup2))
                        assembly = Assembly.LoadFrom(lookup2);
                    else if (File.Exists(lookup3))
                        assembly = Assembly.LoadFrom(lookup3);

                    if (assembly != null)
                        _resolvedAssemblies.Add(name, assembly);
                }
                catch (Exception e)
                {
                }
            }

            return assembly;
        }

        public void Dispose()
        {
            cpiObjects = null;
            _currentViewerControl.PrepareClose();
            _currentViewerControl = null;
        }

        public void ShowCpiObject(List<string> objectIds)
        {
            _currentViewerControl.HideAll();
            _currentViewerControl.ShowIDs(objectIds);
            _currentViewerControl.Refresh();
        }

        public bool GetViewPixelDataExtends(out int left, out int right, out int bottom, out int top)
        {
            return _currentViewerControl.GetViewPixelDataExtends(out left, out right, out bottom, out top);
        }

        public void ShowAll()
        {
            _currentViewerControl.ShowAll();
        }
    }
}
