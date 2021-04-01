using System;
using System.IO;
using System.Media;

namespace CGWH.Core.Sound
{
    internal class SoundHandler
    {
        private static readonly string directoryPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

        private static readonly string directorySoundsPath = Path.Combine(directoryPath, "sounds\\");



        private SoundPlayer turnOnSound { get; }

        private SoundPlayer turnOffSound { get; }



        internal SoundHandler()
        {
            if (File.Exists(directorySoundsPath + "on.wav") && File.Exists(directorySoundsPath + "off.wav"))
            {
                turnOnSound = new SoundPlayer(directorySoundsPath + "on.wav");

                turnOffSound = new SoundPlayer(directorySoundsPath + "off.wav");
            }
        }



        internal void PlayOff()
        {
            turnOffSound.Play();
        }

        internal void PlayOn()
        {
            turnOnSound.Play();
        }
    }
}
