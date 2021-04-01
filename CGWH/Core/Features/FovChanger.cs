using CGWH.Core.Handlers;
using System.Windows.Forms;

namespace CGWH.Core.Functions
{
    internal class FovChanger : InitializeHandler
    {
        private bool enabled;



        internal FovChanger(bool enabled)
        {
            this.enabled = enabled;
        }



        protected override void OnEnable() => Main.Instance.Handler.KeyDown += onKeyDown;

        

        protected override void OnDisable() => Main.Instance.Handler.KeyDown += onKeyDown;



        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.O && !Player.IsScoping)
            {
                if (enabled = !enabled) Player.SetFov(110);

                else Player.SetFovByDefault();
            }
        }
    }
}