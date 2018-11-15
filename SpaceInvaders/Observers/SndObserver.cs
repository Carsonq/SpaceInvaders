using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SndObserver : ColObserver
    {
        IrrKlang.ISoundEngine pSndEngine;
        String soundFile;
        float volumn;
        bool isLoop;

        public enum Name
        {
            Alien_Wall,
            Bomb_Shield,
            Missile_Shield,
            Missile_Alien,
            ShootMissile,
            Bomb_Ship,
            AlienMove1,
            AlienMove2,
            AlienMove3,
            AlienMove4,
            UFOFlyLow,
            UFOFlyHigh,
            Uninitialized
        }

        public SndObserver(IrrKlang.ISoundEngine pEng, SndObserver.Name name, float v = 0.2f, bool isLoop = false)
        {
            Debug.Assert(pEng != null);
            this.pSndEngine = pEng;

            switch (name)
            {
                case SndObserver.Name.Alien_Wall:
                    soundFile = "fastinvader1.wav";
                    break;

                case SndObserver.Name.Bomb_Shield:
                    soundFile = "explosion.wav";
                    break;
                case SndObserver.Name.Missile_Shield:
                    soundFile = "explosion.wav";
                    break;
                case SndObserver.Name.Bomb_Ship:
                    soundFile = "explosion.wav";
                    break;
                case SndObserver.Name.Missile_Alien:
                    soundFile = "invaderkilled.wav";
                    break;
                case SndObserver.Name.ShootMissile:
                    soundFile = "shoot.wav";
                    break;
                case SndObserver.Name.AlienMove1:
                    soundFile = "fastinvader1.wav";
                    break;
                case SndObserver.Name.AlienMove2:
                    soundFile = "fastinvader2.wav";
                    break;
                case SndObserver.Name.AlienMove3:
                    soundFile = "fastinvader3.wav";
                    break;
                case SndObserver.Name.AlienMove4:
                    soundFile = "fastinvader4.wav";
                    break;
                case SndObserver.Name.UFOFlyLow:
                    soundFile = "ufo_lowpitch.wav";
                    break;
                case SndObserver.Name.UFOFlyHigh:
                    soundFile = "ufo_highpitch.wav";
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            this.volumn = v;
            this.isLoop = isLoop;
        }

        public IrrKlang.ISound PlaySound()
        {
            pSndEngine.SoundVolume = this.volumn;
            IrrKlang.ISound pSnd = pSndEngine.Play2D(this.soundFile, this.isLoop);
            return pSnd;
        }

        public override void Notify()
        {
            //Debug.WriteLine(" Snd_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            pSndEngine.SoundVolume = this.volumn;
            IrrKlang.ISound pSnd = pSndEngine.Play2D(this.soundFile, this.isLoop);
        }
    }
}