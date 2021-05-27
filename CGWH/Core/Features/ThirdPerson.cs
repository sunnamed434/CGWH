using CGWH.Core.Handlers;
using System.Windows.Input;

namespace CGWH.Core.Functions
{
    internal class ThirdPerson : InitializeHandler
    {
        private bool enabled;



        internal ThirdPerson(bool enabled) => this.enabled = enabled;



        protected override void OnEnable() => Main.Instance.Listener.OnKeyPressed += onKeyDown;

        protected override void OnDisable() => Main.Instance.Listener.OnKeyPressed -= onKeyDown;



        private void onKeyDown(Key key)
        {
            if (WindowHandler.TryGetCSGOWindow())
            {
                if (key == Key.H)
                {
                    if (enabled = !enabled) Player.SetThirdPersonView();

                    else Player.SetFirstPersonView();
                }
            }
        }
    }
}
