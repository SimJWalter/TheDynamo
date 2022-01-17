using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DSProcessor.ReadyState;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;

namespace Designer
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
      Thread.CurrentThread.Priority = ThreadPriority.Highest;
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(ReadyStateController.Instance.Open());
    }
  }
}