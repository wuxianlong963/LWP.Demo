using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RIB.Visual.Services.CpiModel.Shared;
using RIB.Visual.Components.Model.CPI;
using RIB.Visual.Components.CPI.Common;
using LWP._5DModel.Control;

namespace LWP._5DModel
{
    public partial class Form1 : Form
    {
        private CpiViewerControl cpiViewer;
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.view_FormClosing);
            LoadCpi();
        }

        private void LoadCpi()
        {
            var targetCpiModel = new CpiModel();
            var project = new Project("HHH", "1.0");
            project.KeepModelParts = false;
            targetCpiModel.Project = project;

            IModelReader modelReader = ModelFactory.CreateModelReader(DataSourceType.CpiXmlFile);
            modelReader.CpiXmlFileList = new List<string> { @"D:\SolutionDemo\YuSong\LWP\LWP.5DModel\07-OfficeBuilding_complete.cpixml" };
            modelReader.CpiModel = targetCpiModel;
            modelReader.Read();
            modelReader.Finish();

            var objects = targetCpiModel.Objects as CpiObjects;

            cpiViewer = new CpiViewerControl(objects);
            this.elementHost1.Child = cpiViewer;
            cpiViewer.ShowAll();
        }

        private void view_FormClosing(object sender, EventArgs e)
        {
            cpiViewer.Dispose();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            cpiViewer.SetViewModeTo(RIB.Visual.Components.Wpf.ViewMode.Pan);
        }

        private void btnOrbit_Click(object sender, EventArgs e)
        {
            cpiViewer.SetViewModeTo(RIB.Visual.Components.Wpf.ViewMode.Orbit);
        }

        private void btnWalk_Click(object sender, EventArgs e)
        {
            cpiViewer.SetViewModeTo(RIB.Visual.Components.Wpf.ViewMode.Walk);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            cpiViewer.SetViewModeTo(RIB.Visual.Components.Wpf.ViewMode.Selection);
        }

        private void btnSetHome_Click(object sender, EventArgs e)
        {
            cpiViewer.SetHome();
        }
    }
}
