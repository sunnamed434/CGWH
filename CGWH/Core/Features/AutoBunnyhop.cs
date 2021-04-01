using CGWH.Core.Handlers;
using CGWH.Core.Input;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace CGWH.Core.Functions
{
    internal class AutoBunnyhop : InitializeHandler
    {
        private bool enabled;



        internal AutoBunnyhop(bool enabled)
        {
            this.enabled = enabled;
        }



        protected override void OnEnable()
        {
            Main.Instance.Listener.OnKeyPressed += onKeyDown;



            enable();
        }

       

        protected override void OnDisable() => Main.Instance.Listener.OnKeyPressed -= onKeyDown;



        private void enable()
        {
            Thread thread = new Thread(t =>
            {
                while (true)
                {
                    if (enabled)
                    {
                        if (InputHandler.GetKeyDown(Keys.Space) && Player.IsGround)
                        {
                            Player.Jump();
                            Thread.Sleep(1);
                        }
                    }

                    Thread.Sleep(1);
                }
            });

            thread.Start();
        }



        private void onKeyDown(KeyPressArgs e)
        {
            if (e.KeyPressed == Key.C) enabled = !enabled;
        }
    }
}
