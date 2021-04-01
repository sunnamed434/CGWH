using CGWH.Core.Handlers;
using System.Threading;
using System.Windows.Forms;

namespace CGWH.Core.Functions
{
    internal class ESP : InitializeHandler
    {
        private bool enabled = true;



        public ESP(bool enabled)
        {
            this.enabled = enabled;
        }



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
                        int playerTeamNum = Cheat.Memory.Read<int>(Player.Local + Offsets.m_iTeamNum);

                        for (int i = 0; i < 64; i++)
                        {
                            int enemyNum = Cheat.Memory.Read<int>(Cheat.ModuleAddress + Offsets.dwEntityList + i * 16);
                            int enemyTeamNum = Cheat.Memory.Read<int>(enemyNum + Offsets.m_iTeamNum);
                            int index = Cheat.Memory.Read<int>(enemyNum + Offsets.m_iGlowIndex);

                            if (enemyTeamNum != 0)
                            {
                                if (enemyTeamNum != playerTeamNum) drawEnemy(index, 255, 0, 0, 255);

                                else drawEnemy(index, 0, 0, 255, 255);
                            }
                        }
                    }

                    Thread.Sleep(1);
                }
            });

            thread.Start();
        }



        private void drawEnemy(int index, int red, int green, int blue, int alpha)
        {
            int num = Cheat.Memory.Read<int>(Cheat.ModuleAddress + Offsets.dwGlowObjectManager);

            Cheat.Memory.Write(num + index * 56 + 4, red / 100f);
            Cheat.Memory.Write(num + index * 56 + 8, green / 100f);
            Cheat.Memory.Write(num + index * 56 + 12, blue / 100f);
            Cheat.Memory.Write(num + index * 56 + 16, alpha / 100f);

            Cheat.Memory.Write(num + index * 56 + 36, true);
            Cheat.Memory.Write(num + index * 56 + 37, false);
        }



        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z) enabled = !enabled;
        }
    }
}
