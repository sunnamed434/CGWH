using CGWH.Core.Handlers;
using CGWH.Core.Input;
using System.Windows.Input;

namespace CGWH.Core.Functions
{
    internal class AntiFlash : InitializeHandler
    {
        private bool enabled;



        internal AntiFlash(bool enabled)
        {
            this.enabled = enabled;
        }



        protected override void OnEnable() => Main.Instance.Listener.OnKeyPressed += onKeyDown;



        protected override void OnDisable() => Main.Instance.Listener.OnKeyPressed -= onKeyDown;



        private void onKeyDown(KeyPressArgs e)
        {
            if (e.KeyPressed == Key.J)
            {
                if (enabled = !enabled) Player.SetFlashAlpha(.0f);

                else Player.SetFlashAlphaByDefault();
            }
        }
    }
}
