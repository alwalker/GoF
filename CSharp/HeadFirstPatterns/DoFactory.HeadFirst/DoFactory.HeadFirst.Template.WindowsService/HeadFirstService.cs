using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;

namespace DoFactory.HeadFirst.Template.WindowsService
{
    public class HeadFirstService : System.ServiceProcess.ServiceBase
    {
        private Container _components = null;

        public HeadFirstService()
        {
            InitializeComponent();
        }

        // The main entry point for the process
        static void Main()
        {
            ServiceBase[] ServicesToRun;
    
            ServicesToRun = new ServiceBase[] { new HeadFirstService() };

            ServiceBase.Run(ServicesToRun);
        }

        private void InitializeComponent()
        {
            _components = new Container();
            this.ServiceName = "HeadFirstService";
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (_components != null) 
                {
                    _components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        // Design Patterns: Template method

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }
 
        // Design Patterns: Template method

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
