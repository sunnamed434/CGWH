using CGWH.Core.Handlers;
using CGWH.Core.Input;
using System.Threading;
using System.Threading.Tasks;
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
            Task task = new Task(() =>
            {
                while (true)
                {
                    if (enabled)
                    {
                        if (WindowHandler.TryGetCSGOWindow())
                        {
                            if (InputHandler.GetKeyDown(Keys.Space) && Player.IsGround)
                            {
                                Player.Jump();
                                Thread.Sleep(1);
                            }
                        }
                    }

                    Thread.Sleep(1);
                }
            });

            task.Start();
        }



        private void onKeyDown(KeyPressArgs e)
        {
            if (WindowHandler.TryGetCSGOWindow())
            {
                if (e.KeyPressed == Key.C) enabled = !enabled;
            }
        }
    }
}
