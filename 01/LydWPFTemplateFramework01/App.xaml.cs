using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Configuration;
using System.Data;
using System.Windows;
using Wpf.Ui;
using LydWPFTemplateFramework01.ViewModels;
using LydWPFTemplateFramework01.ViewModels.Pages;
using LydWPFTemplateFramework01.ViewModels.Windows;
using LydWPFTemplateFramework01.Views.Pages;
using LydWPFTemplateFramework01.Views.Windows;

namespace LydWPFTemplateFramework01
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string SerilogOutputTemplate = "{NewLine}{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" + new string('-', 100);
        static LoggerConfiguration loggerConfig = new LoggerConfiguration().Enrich.FromLogContext();
                

        private static readonly IHost _host = Host.CreateDefaultBuilder()
            .ConfigureServices(
                (context, services) =>
                {
                    loggerConfig = loggerConfig.WriteTo.File("Logs/log.log", rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate, retainedFileCountLimit: 31);
                    Log.Logger = loggerConfig.CreateLogger();

                    services.AddSingleton<INavigationService, NavigationService>();

                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<IWindow, MainWindow>();

                    services.AddSingleton<SettingsPageViewModel>();
                    services.AddSingleton<SettingsPage>();
                    services.AddSingleton<P1Page>();
                    services.AddSingleton<P2Page>();
                    services.AddSingleton<P3Page>();
                    services.AddSingleton<NotifyIconViewModel>();
                }
            )
            .Build();

        /// <summary>
        /// 获取注入服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T? GetService<T>() where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }
        /// <summary>
        /// 获取注入服务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object? GetService(Type type)
        {
            return _host.Services.GetService(type);
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            RegisterEvents();
            //启动依赖注入
            await _host.StartAsync();
            //显示主窗体
            GetService<IWindow>()?.Show();

            base.OnStartup(e);
            Log.Information("程序启动");
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            //UI线程未捕获异常处理事件（UI主线程）
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                var exception = e.Exception as Exception;
                if (exception != null)
                {
                    HandleException(exception);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                e.SetObserved();
            }
        }

        //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)      
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    HandleException(exception);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                //ignore
            }
        }

        //UI线程未捕获异常处理事件（UI主线程）
        private static void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                HandleException(e.Exception);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                //处理完后，我们需要将Handler=true表示已此异常已处理过
                e.Handled = true;
            }
        }
        private static void HandleException(Exception e)
        {
            //MessageBox.Show("程序异常：" + e.Source + "\r\n@@" + Environment.NewLine + e.StackTrace + "\r\n##" + Environment.NewLine + e.Message);

            //记录日志
            Log.Error(e, "程序发生异常");
        }
    }

}
