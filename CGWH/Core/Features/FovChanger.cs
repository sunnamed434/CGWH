using CGWH.Core.Attributes;
using CGWH.Core.Handlers;
using System.Windows.Forms;
using static CGWH.Core.Input.GlobalKeyboardHook;

namespace CGWH.Core.Functions
{
    [VAC]
    internal class FovChanger : InitializeHandler
    {
        private bool enabled = false;



        protected override void OnEnable() => Main.Instance.Hook.KeyboardPressed += onKeyPress;



        protected override void OnDisable() => Main.Instance.Hook.KeyboardPressed -= onKeyPress;



        private void onKeyPress(object sender, Input.GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardData.Key == Keys.O && e.KeyboardState == KeyboardState.KeyDown && !Player.IsScoping)
            {
                if (enabled = !enabled) Player.SetFov(110);

                else Player.SetFovByDefault();
            }
        }
    }
}