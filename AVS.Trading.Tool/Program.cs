using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.ProxyUtil;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Tool.Forms;

namespace AVS.Trading.Tool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //when VPN is active I don't need proxy anymore
            ProxyHelper.DontUseProxy = true;

            //if (!PingPoloniex())
            //    return;
            //ensure culture is en
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            //initialize engine 
            
            var config = ConfigurationManager.GetSection("AppConfig") as TradingAppConfig;
            if (config == null)
            {
                Console.WriteLine("Config is missing..");
                return;
            }

            try
            {
                EngineContext.Initialize(false, config);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Application start failed");
                return;
            }

            //var ctx = EngineContext.Current.Resolve<IDbContext>();
            //ctx.ExecuteSqlCommand("select 1");
            //var installation = EngineContext.Current.Resolve<IInstallationService>();
            //installation.InstallScheduledTasks(false);
            //installation.ClearData();
            //new Form1()
            restart:
            try
            {
                var task = Task.Run(() => EngineContext.Current.RunBackgroundTasksAsync());
                Application.Run(new MainForm());
                Task.WaitAny(task);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), @"UNHANDLED EXCEPTION");
                goto restart;
            }
            
        }

        private static bool PingProxy()
        {
            //ping proxy
            try
            {
                var content = ProxyHelper.SendTestWebRequest(
                    "https://poloniex.com/public?command=returnTicker");
                return true;
            }
            catch (Exception)
            {
                Debug.WriteLine("Poloniex API is not accessible\r\n{0} \r\n");
                return false;
            }
        }
    }
}
