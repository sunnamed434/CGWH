using CGWH.Core.Attributes;
using CGWH.Core.Handlers;
using System.Windows.Forms;
using static CGWH.Core.Input.GlobalKeyboardHook;

namespace CGWH.Core.Functions
{
    [VAC]
    internal class ThirdPerson : InitializeHandler
    {
        private bool enabled;



        protected override void OnEnable() => Main.Instance.Hook.KeyboardPressed += onKeyPress;



        protected override void OnDisable() => Main.Instance.Hook.KeyboardPressed -= onKeyPress;



        private void onKeyPress(object sender, Input.GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardData.Key == Keys.H && e.KeyboardState == KeyboardState.KeyDown)
            {
                if (enabled = !enabled)
                {
                    Player.SetThirdPersonView();
                }
                else
                {
                    Player.SetFirstPersonView();
                }
            }
        }
    }
}
