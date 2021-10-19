using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using MVVM.ViewModel;
using Possible;

namespace KerrMagneto_OpticMeasurementSystem
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Single<MVVM.Model.EnvironmentInitialization>.Instance.Init();
            //Single<MVVM.Model.VidPidGetCom>.Instance.ToString();
            //Single<MVVM.Model.PBZ_2020_C>.Instance.ToString();
            //Single<MainWinTag>.Instance.com_thread_init.WaitOne();
            base.OnStartup(e);
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }
        protected override void OnLoadCompleted(NavigationEventArgs e)
        {
            base.OnLoadCompleted(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                //Single<MainWinTag>.Instance.Invoke(() =>
                //{
                //Single<MVVM.Model.PBZ20_20>.Instance.Disable();
                //});
            }
            catch (Exception)
            {
            }
            //Single<MVVM.Model.EnvironmentInitialization>.Instance.OnClose();
            base.OnExit(e);
        }
    }
}
