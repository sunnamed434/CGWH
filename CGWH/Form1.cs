using CGWH.Core;
using CGWH.Core.Features;
using CGWH.Core.Functions;
using CGWH.Core.Handlers;
using CGWH.Core.Input;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CGWH
{
    internal partial class Main : Form
    {
        internal static Main Instance;



        internal readonly LowLevelKeyboardListener Listener;



        internal Main()
        {
            InitializeComponent();

            Instance = this;

            FormClosed += onMainUnload;

            Listener = new LowLevelKeyboardListener();



            if (!Cheat.TryCheckValidVersion(out string content))
            {
                MessageBox.Show(content, "Game version is not valid!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Cheat.TryFindProcess())
            {
                MessageBox.Show("Please start game!", "CSGO Not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            new ESP(true);

            new Trigger();

            new AntiFlash(false);

            new AutoBunnyhop(false);

            new AutoPistol(true);

            new Radar(false);
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

            Process.GetCurrentProcess().Kill();
        }

        #endregion
    }
}
