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



            if (!Cheat.TryCheckValidVersion(out string content))
            {
                InformationText.Text = "Version is not valid!";

                InformationText.ForeColor = Color.Red;

                MessageBox.Show(content, "Game version is not valid!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Cheat.TryFindProcess())
            {
                InformationText.Text = "Can't found CS:GO Process!";

                InformationText.ForeColor = Color.Red;

                MessageBox.Show("Please start game!", "CSGO Not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InformationText.Text = "Cheat loaded to CS:GO";
            InformationText.ForeColor = Color.Green;

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
