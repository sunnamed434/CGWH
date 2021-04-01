using System.Windows.Input;

namespace CGWH.Core.Input
{
    internal class KeyPressArgs
    {
        internal Key KeyPressed { get; }



        internal KeyPressArgs(Key key)
        {
            KeyPressed = key;
        }
    }
}
