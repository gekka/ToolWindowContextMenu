using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Gekka.ToolWindowContextMenu
{
    internal sealed class ToolWindow1Command
    {
        #region static

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            var dte = await Microsoft.VisualStudio.Shell.ServiceProvider.GetGlobalServiceAsync(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            Instance = new ToolWindow1Command(package, commandService, dte);
        }

        public static readonly Guid CommandSet = new Guid("C085B947-854B-47DB-9178-3C4A987DD6ED"); //change your GUID (need same guid for commandSet in vsct)

        enum ID_MENU : ushort
        {
            ContentContext = 0x100,
        }

        enum ID_CMD : ushort
        {
            ShowToolWindow = 0x0300,

            ContextMenu_1 = 0x0301,
            ContextMenu_2 = 0x0302,
            ContextMenu_3 = 0x0303,
        }

        public static ToolWindow1Command Instance { get; private set; }

        #endregion

        private readonly AsyncPackage package;
        private readonly OleMenuCommandService commandService;
        private readonly EnvDTE80.DTE2 dte;
        //private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider => this.package;

        private ToolWindow1Command(AsyncPackage package, OleMenuCommandService commandService, EnvDTE80.DTE2 dte)
        {
            this.package = package;
            this.commandService = commandService;
            this.dte = dte;

            {
                var menuidShow = new CommandID(CommandSet, (int)ID_CMD.ShowToolWindow);
                var menuCommand = new MenuCommand(this.OnShowToolWindow, menuidShow);
                commandService.AddCommand(menuCommand);
            }

            foreach (ID_CMD id in new[] { ID_CMD.ContextMenu_1, ID_CMD.ContextMenu_2, ID_CMD.ContextMenu_3 })
            {
                var menuidContext = new CommandID(CommandSet, (int)id);
                var menuCommand = new OleMenuCommand(this.OnContextMenu, menuidContext);
                menuCommand.BeforeQueryStatus += MenuItem_ContextMenu_BeforeQueryStatus;
                commandService.AddCommand(menuCommand);
            }
        }

        private void MenuItem_ContextMenu_BeforeQueryStatus(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var menu = (OleMenuCommand)sender;

            EnvDTE80.Window2 activeWindow = (EnvDTE80.Window2)dte.ActiveWindow;
            if (Guid.TryParse(activeWindow.ObjectKind, out Guid activeWindouKind) && activeWindouKind == typeof(ToolWindow1).GUID)
            {
                menu.Enabled = true;
            }
            else
            {
                menu.Enabled = false;
            }
        }

        private void OnShowToolWindow(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var t = typeof(ToolWindow1);
            ToolWindowPane window = this.package.FindToolWindow(typeof(ToolWindow1), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }


        public void ShowContentContextMenu(int screenX, int screenY)
        {
            var cmd = new CommandID(CommandSet, (int)ID_MENU.ContentContext);
            commandService.ShowContextMenu(cmd, screenX, screenY);
        }
        private void OnContextMenu(object sender, EventArgs e)
        {
            ToolWindowPane window = this.package.FindToolWindow(typeof(ToolWindow1), 0, false);
            if ((null == window) || (null == window.Frame))
            {
                return;
            }

            if (window.Content is System.Windows.Controls.ContentControl ctl)
            {
                var cmd = (OleMenuCommand)sender;
                switch ((ID_CMD)cmd.CommandID.ID)
                {
                case ID_CMD.ContextMenu_1:
                    ctl.Background = System.Windows.Media.Brushes.Red;
                    break;
                case ID_CMD.ContextMenu_2:
                    ctl.Background = System.Windows.Media.Brushes.Green;
                    break;
                case ID_CMD.ContextMenu_3:
                    ctl.Background = System.Windows.Media.Brushes.Blue;
                    break;
                default:
                    break;
                }
            }
        }
    }
}
