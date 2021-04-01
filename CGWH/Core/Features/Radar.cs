using CGWH.Core.Handlers;
using CGWH.Core.Input;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CGWH.Core.Features
{
    internal class Radar : InitializeHandler
    {
        private bool enabled;



        internal Radar(bool enabled)
        {
            this.enabled = enabled;
        }


            
        protected override void OnEnable()
        {
            Main.Instance.Listener.OnKeyPressed += onKeyDown;



            enable();
        }

        

        protected override void OnDisable() 
        {
            Main.Instance.Listener.OnKeyPressed -= onKeyDown;
        }



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
                            for (int i = 0; i < 10; i++)
                            {
                                int enemy = Cheat.Memory.Read<int>(Cheat.ModuleAddress + Offsets.dwEntityList + (i * 0x10));

                                if (Cheat.Memory.Read<bool>(enemy + Offsets.m_bDormant)) continue;

                                Cheat.Memory.Write<bool>(enemy + Offsets.m_bSpotted, true);
                            }
                        }
                    }
                }
            });

            task.Start();
        }



        private void onKeyDown(KeyPressArgs e)
        {
            if (WindowHandler.TryGetCSGOWindow())
            {
                if (e.KeyPressed == Key.N) enabled = !enabled;
            }
        }
    }
}
