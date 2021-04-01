using CGWH.Core;
using CGWH.Core.Features;
using CGWH.Core.Functions;
using CGWH.Core.Handlers;
using Gma.System.MouseKeyHook;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CGWH
{
    internal partial class Main : Form
    {
        internal static Main Instance;



        internal readonly IKeyboardMouseEvents Handler;



        internal Main()
        {
            InitializeComponent();

            Instance = this;

            FormClosed += onMainUnload;

            Handler = Hook.GlobalEvents();



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



            new ESP();

            new Trigger();

            new AntiFlash();

            new AutoBunnyhop();

            new AutoPistol();
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
