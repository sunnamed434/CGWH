using CGWH.Core.Handlers;
using System.Threading;

namespace CGWH.Core.Features
{
    internal class AutoPistol : InitializeHandler
    {
        private bool enabled;



        internal AutoPistol(bool enabled)
        {
            this.enabled = enabled;
        }



        protected override void OnEnable() => enable();



        protected override void OnDisable() { }



        private void enable()
        {
            Thread thread = new Thread(t =>
            {
                while (true)
                {
                    if (enabled)
                    {
                        if (WindowHandler.TryGetCSGOWindow())
                        {
                            if (InputHandler.GetLeftMouseButtonDown() && Player.HasHandsPistol)
                            {
                                Player.Attack();
                                Thread.Sleep(3);
                            }
                        }
                    }

                    Thread.Sleep(1);
                }
            });

            thread.Start();
        }
    }
}
