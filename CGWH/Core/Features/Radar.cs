using CGWH.Core.Handlers;
using System.Threading;
using System.Windows.Forms;

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
            Main.Instance.Handler.KeyDown += onKeyDown;



            enable();
        }

        

        protected override void OnDisable() 
        {
            Main.Instance.Handler.KeyDown -= onKeyDown;
        }



        private void enable()
        {
            Thread thread = new Thread(t =>
            {
                while (true)
                {
                    if (enabled)
                    {
                        for (int i = 0; i < 64; i++)
                        {
                            int enemy = Cheat.Memory.Read<int>(Cheat.ModuleAddress + Offsets.dwEntityList + (i * 0x10));

                            if (Cheat.Memory.Read<bool>(enemy + Offsets.m_bDormant)) continue;

                            Cheat.Memory.Write<bool>(enemy + Offsets.m_bSpotted, true);
                        }
                    }
                }
            });

            thread.Start();
        }



        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N) enabled = !enabled;
        }
    }
}
