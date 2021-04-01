using CGWH.Core.Handlers;
using CGWH.Core.Other;
using System.Threading;
using System.Windows.Forms;

namespace CGWH.Core.Functions
{
    internal class Trigger : InitializeHandler
    {
        protected override void OnEnable() => enable();



        protected override void OnDisable() { }



        private void enable()
        {
            Thread thread = new Thread(t =>
            {
                while (true)
                {
                    if (InputHandler.GetKeyDown(Keys.LMenu))
                    {
                        if (Player.TryGetCrosshairEnemyTrigger(out CrosshairParameters parameters) && !parameters.TriggerIsTeammate())
                        {
                            Player.Attack();
                        }
                    }

                    Thread.Sleep(1);
                }
            });

            thread.Start();
        }
    }
}
