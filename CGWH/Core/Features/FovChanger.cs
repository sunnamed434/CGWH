using CGWH.Core.Handlers;
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



        private void onKeyDown(Key key)
        {
            if (WindowHandler.TryGetCSGOWindow())
            {
                if (key == Key.O && !Player.IsScoping)
                {
                    if (enabled = !enabled) Player.SetFov(110);

                    else Player.SetFovByDefault();
                }
            }
        }
    }
}