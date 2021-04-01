using CGWH.Core.Handlers;
using CGWH.Core.Input;
using System.Windows.Input;

namespace CGWH.Core.Functions
{
    internal class ThirdPerson : InitializeHandler
    {
        private bool enabled;



        internal ThirdPerson(bool enabled)
        {
            this.enabled = enabled;
        }



        protected override void OnEnable() => Main.Instance.Listener.OnKeyPressed += onKeyDown;



        protected override void OnDisable() => Main.Instance.Listener.OnKeyPressed -= onKeyDown;



        private void onKeyDown(KeyPressArgs e)
        {
            if (WindowHandler.TryGetCSGOWindow())
            {
                if (e.KeyPressed == Key.H)
                {
                    if (enabled = !enabled) Player.SetThirdPersonView();

                    else Player.SetFirstPersonView();
                }
            }
        }
    }
}
