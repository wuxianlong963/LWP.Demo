using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWP.Console.Lib
{
    public class Perant
    {
        public event EventHandler<ProjectDownloadEventArgs> OnProjectCompletedDownload;

        public void addEvent()
        {
            
        }

        public void Rewirte(string input)
        {
            if (OnProjectCompletedDownload != null)
                OnProjectCompletedDownload(null, new ProjectDownloadEventArgs());
        }
    }


    public class Child : Perant
    {
        public void Re()
        {
            OnProjectCompletedDownload += new EventHandler<ProjectDownloadEventArgs>(rew);
        }

        public void rew(object sender, ProjectDownloadEventArgs e)
        {
        }
    }

    public class ProjectDownloadEventArgs : EventArgs
    {

    }
}
