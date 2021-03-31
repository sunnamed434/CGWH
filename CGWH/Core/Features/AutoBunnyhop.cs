using CGWH.Core.Handlers;
using System.Threading;
using System.Windows.Forms;
using static CGWH.Core.Input.GlobalKeyboardHook;

namespace CGWH.Core.Functions
{
    internal class AutoBunnyhop : InitializeHandler
    {
        private bool enabled = false;



        protected override void OnEnable()
        {
            Main.Instance.Hook.KeyboardPressed += onKeyPress;



            enable();
        }



        protected override void OnDisable() => Main.Instance.Hook.KeyboardPressed -= onKeyPress;



        internal void enable()
        {
            Thread thread = new Thread(t =>
            {
                while (true)
                {
                    if (enabled)
                    {
                        if (KeyboardHandler.GetKeyDown(Keys.Space) && Player.IsGround)
                        {
                            Player.Jump(5);
                        }
                    }

                    Thread.Sleep(1);
                }
            });

            thread.Start();
        }



        private void onKeyPress(object sender, Input.GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == KeyboardState.KeyDown && e.KeyboardData.Key == Keys.C) enabled = !enabled;
        }
    }
}
