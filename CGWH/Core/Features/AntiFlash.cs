using CGWH.Core.Handlers;
using System.Windows.Forms;

namespace CGWH.Core.Functions
{
    internal class AntiFlash : InitializeHandler
    {
        private bool enabled = false;



        protected override void OnEnable() => Main.Instance.Handler.KeyDown += onKeyDown;



        protected override void OnDisable() => Main.Instance.Handler.KeyDown += onKeyDown;



        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.J)
            {
                if (enabled = !enabled) Player.SetFlashAlpha(.0f);

                else Player.SetFlashAlphaByDefault();
            }
        }
    }
}
