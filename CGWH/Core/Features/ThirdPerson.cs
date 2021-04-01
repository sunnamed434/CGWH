using CGWH.Core.Attributes;
using CGWH.Core.Handlers;
using System.Windows.Forms;

namespace CGWH.Core.Functions
{
    internal class ThirdPerson : InitializeHandler
    {
        private bool enabled;



        internal ThirdPerson(bool enabled)
        {
            this.enabled = enabled;
        }



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
