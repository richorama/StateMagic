﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace StateMagic.Designer
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        public static string Username { get; set; }

        public static string Password { get; set; }

        public static int ModelId { get; set; }

        public static Guid APIKey { get; set; }

        public static readonly Guid SystemKey = new Guid("3FB3447D-5707-4525-91DF-8FE7B2396088");

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            // read input parameters
            if (e.InitParams.ContainsKey("username"))
            {
                Username = e.InitParams["username"];
            }

            if (e.InitParams.ContainsKey("modelid"))
            {
                string modelString = e.InitParams["modelid"];
                if (!string.IsNullOrEmpty(modelString))
                {
                    int modelid = 0;
                    int.TryParse(modelString, out modelid);
                    ModelId = modelid;
                }
            }

            if (e.InitParams.ContainsKey("apikey"))
            {
                string apiKeyString = e.InitParams["apikey"];
                if (!string.IsNullOrEmpty(apiKeyString))
                {
                    APIKey = new Guid(apiKeyString);
                }
            }
            this.RootVisual = new Page1();

        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
