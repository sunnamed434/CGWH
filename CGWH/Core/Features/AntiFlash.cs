using CGWH.Core.Handlers;
using System.Windows.Forms;
using static CGWH.Core.Input.GlobalKeyboardHook;

namespace CGWH.Core.Functions
{
    internal class AntiFlash : InitializeHandler
    {
        private bool enabled = false;



        protected override void OnEnable() => Main.Instance.Hook.KeyboardPressed += onKeyPress;



        protected override void OnDisable() => Main.Instance.Hook.KeyboardPressed -= onKeyPress;



        private void onKeyPress(object sender, Input.GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardData.Key == Keys.J && e.KeyboardState == KeyboardState.KeyDown)
            {
                if (enabled = !enabled) Player.SetFlashAlpha(.0f);

                else Player.SetFlashAlphaByDefault();
            }
        }
    }
}
