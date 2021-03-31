using CGWH.Core;
using CGWH.Core.Functions;
using CGWH.Core.Handlers;
using CGWH.Core.Input;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CGWH
{
    internal partial class Main : Form
    {
        internal static Main Instance;



        internal readonly GlobalKeyboardHook Hook;



        internal Main()
        {
            InitializeComponent();

            Instance = this;



            FormClosed += onMainUnload;



            if (!Cheat.TryCheckValidVersion())
            {
                Status.Text = "Version is not valid!";

                Status.ForeColor = Color.Red;
                return;
            }

            if (!Cheat.TryFindProcess())
            {
                Status.Text = "Can't found CS:GO Process!";

                Status.ForeColor = Color.Red;
                return;
            }

            Status.Text = "All right, let`s go play! =)";
            Status.ForeColor = Color.Green;

            Hook = new GlobalKeyboardHook();

            new ESP();

            new Trigger();

            new AntiFlash();

            new AutoBunnyhop();
        }



        #region Load/Unload

        private void MainLoad(object sender, EventArgs e)
        {
            ApplicationHandler.Load?.Invoke();
        }

        private void onMainUnload(object sender, FormClosedEventArgs e)
        {
            ApplicationHandler.Unload?.Invoke();

            FormClosed -= onMainUnload;
        }

        #endregion
    }
}
