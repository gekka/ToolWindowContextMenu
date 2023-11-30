namespace Gekka.ToolWindowContextMenu
{
    [System.Runtime.InteropServices.Guid("EE9A2989-8C0D-4666-B4B7-9A7C14F095FA")]
    public class ToolWindow1 : Microsoft.VisualStudio.Shell.ToolWindowPane
    {
        public ToolWindow1() : base(null)
        {
            var ctl = new ToolWindow1Control();
            ctl.MouseRightButtonDown += Ctl_MouseRightButtonDown;

            this.Caption = "ToolWindow1";
            this.Content = ctl;
        }

        private void Ctl_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.Handled || ToolWindow1Command.Instance == null)
            {
                return;
            }

            var control = (System.Windows.Controls.Control)sender;
            var point = control.PointToScreen(e.GetPosition(control));

            ToolWindow1Command.Instance.ShowContentContextMenu((int)point.X, (int)point.Y);
            e.Handled = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

    }
}
