using CGWH.Core.Attributes;
using CGWH.Core.Handlers;
using System.Windows.Forms;

namespace CGWH.Core.Functions
{
    [VAC]
    internal class FovChanger : InitializeHandler
    {
        private bool enabled = false;



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