using CGWH.Core.Attributes;
using CGWH.Core.Handlers;
using System.Windows.Forms;

namespace CGWH.Core.Functions
{
    [VAC]
    internal class ThirdPerson : InitializeHandler
    {
        private bool enabled;



        protected override void OnEnable() => Main.Instance.Handler.KeyDown += onKeyDown;


        protected override void OnDisable() => Main.Instance.Handler.KeyDown -= onKeyDown;



        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.H)
            {
                if (enabled = !enabled) Player.SetThirdPersonView();

                else Player.SetFirstPersonView();
            }
        }
    }
}
