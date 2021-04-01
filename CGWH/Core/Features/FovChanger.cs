using CGWH.Core.Handlers;
using CGWH.Core.Input;
using System.Windows.Forms;
using System.Windows.Input;

namespace CGWH.Core.Functions
{
    internal class FovChanger : InitializeHandler
    {
        private bool enabled;



        internal FovChanger(bool enabled)
        {
            this.enabled = enabled;
        }



        protected override void OnEnable() => Main.Instance.Listener.OnKeyPressed += onKeyDown;

        

        protected override void OnDisable() => Main.Instance.Listener.OnKeyPressed -= onKeyDown;



        private void onKeyDown(KeyPressArgs e)
        {
            if (e.KeyPressed == Key.O && !Player.IsScoping)
            {
                if (enabled = !enabled) Player.SetFov(110);

                else Player.SetFovByDefault();
            }
        }
    }
}