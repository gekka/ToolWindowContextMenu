using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Gekka.ToolWindowContextMenu
{
    //[ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    //[ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.EmptySolution, PackageAutoLoadFlags.BackgroundLoad)]
    //[ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    //[ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionHasSingleProject, PackageAutoLoadFlags.BackgroundLoad)]
    //[ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionHasMultipleProjects, PackageAutoLoadFlags.BackgroundLoad)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(VSIXProject4Package.PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(ToolWindow1))]
    public sealed class VSIXProject4Package : AsyncPackage
    {
        public const string PackageGuidString = "9F126C84-CA73-4ECC-AB8E-54A3D5286DE4"; //change your GUID

        #region Package Members

        public VSIXProject4Package()
        {
                
        }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await ToolWindow1Command.InitializeAsync(this);
        }

        #endregion
    }
}



// PresentationCore
// PresentationFramework
// System.Xaml
// WindowsBase

//System.Windows.Window window = System.Windows.Application.Current.MainWindow;
//window.PreviewMouseDown += Window_PreviewMouseDown;

//System.Windows.EventManager.RegisterClassHandler(typeof(System.Windows.Window), System.Windows.Window.LoadedEvent, new System.Windows.RoutedEventHandler(Window_Loaded));

//IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
//var source = System.Windows.Interop.HwndSource.FromHwnd(hwnd);
//source.AddHook(OnMessage);

//dte = await ServiceProvider.GetGlobalServiceAsync(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

//public EnvDTE80.DTE2 dte { get; private set; }


///// <summary> Window Message </summary>
//private IntPtr OnMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
//{
//    const int WM_LBUTTONDOWN = 0x0201;
//    if (msg == WM_LBUTTONDOWN)
//    {
//        System.Diagnostics.Debug.WriteLine(nameof(WM_LBUTTONDOWN));
//    }
//    return IntPtr.Zero;
//}

///// <summary>  window or popup created</summary>
//private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
//{
//    System.Windows.Window window = (System.Windows.Window)sender;
//    window.PreviewMouseDown += Window_PreviewMouseDown;
//}

///// <summary> Mouse Down </summary>
//private void Window_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
//{
//    System.Windows.Window window = (System.Windows.Window)sender;

//    var position = e.GetPosition(window);
//    var positinoSceen = window.PointToScreen(position);

//    System.Diagnostics.Debug.WriteLine(position);

//    if (!(e.OriginalSource is System.Windows.DependencyObject d))
//    {
//        return;
//    }

//    System.Text.StringBuilder sb = new System.Text.StringBuilder();



//    while (d != null)
//    {


//        sb.Append("=>" + d.GetType().Name);
//        if (d is System.Windows.Media.Visual)
//        {
//            if (d is System.Windows.FrameworkElement fe)
//            {
//                var dataType = fe.DataContext?.GetType();
//                sb.Append($"({dataType.Name})");
//            }

//            d = System.Windows.Media.VisualTreeHelper.GetParent(d);

//        }
//        else
//        {
//            d = System.Windows.LogicalTreeHelper.GetParent(d);
//        }
//    }

//    System.Diagnostics.Debug.WriteLine(sb.ToString());

//    if (dte != null)
//    {
//        foreach (EnvDTE.Window w in dte.Windows)
//        {
//            if (!w.Visible)
//            {
//                continue;
//            }

//            int top = w.Top;
//            int bottom = w.Top + w.Height;
//            int left = w.Left;
//            int right = w.Left + w.Width;

//            System.Windows.Rect rect = new System.Windows.Rect(w.Left, w.Top, w.Width, w.Height);


//            if (rect.Contains(position))
//            {
//                System.Diagnostics.Debug.WriteLine("Caption=" + w.Caption);
//            }

//        }
//    }

//}
