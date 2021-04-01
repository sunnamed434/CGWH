using CGWH.Core.Handlers;
using System.Threading;
using System.Windows.Forms;

namespace CGWH.Core.Functions
{
    internal class AutoBunnyhop : InitializeHandler
    {
        private bool enabled = false;



        protected override void OnEnable()
        {
            Main.Instance.Handler.KeyDown += onKeyDown;



            enable();
        }

       

        protected override void OnDisable() => Main.Instance.Handler.KeyDown -= onKeyDown;



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
                            Player.Jump(6);
                            Thread.Sleep(3);
                        }
                    }

                    Thread.Sleep(1);
                }
            });

            thread.Start();
        }



        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C) enabled = !enabled;
        }
    }
}
