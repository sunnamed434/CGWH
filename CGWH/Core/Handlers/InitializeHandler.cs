namespace CGWH.Core.Handlers
{
    internal abstract class InitializeHandler
    {
        protected InitializeHandler()
        {
            ApplicationHandler.Load += onEnable;

            ApplicationHandler.Unload += onDisable;
        }



        protected abstract void OnEnable();

        protected abstract void OnDisable();



        private void onEnable()
        {
            ApplicationHandler.Load -= onEnable;

            OnEnable();
        }

        private void onDisable()
        {
            ApplicationHandler.Load -= onDisable;

            OnDisable();
        }
    }
}
