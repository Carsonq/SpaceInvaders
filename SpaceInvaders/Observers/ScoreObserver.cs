using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ScoreObserver : ColObserver
    {
        int pScore;

        public ScoreObserver()
        {
            this.pScore = 0;
        }

        public override void Notify()
        {
            //Debug.WriteLine(" Snd_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            GameObject.Name name = this.pSubject.pObjB.GetName();
            switch (name)
            {
                case GameObject.Name.Octopus:
                    this.pScore = 10;
                    break;
                case GameObject.Name.Crab:
                    this.pScore = 20;
                    break;
                case GameObject.Name.Squid:
                    this.pScore = 30;
                    break;
                case GameObject.Name.UFO:
                    this.pScore = 200;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            if (SceneStateGame.GetCurrPlayer() == 1)
            {
                Font pTestMessage = FontMan.Find(Font.Name.Score1);
                Debug.Assert(pTestMessage != null);
                int newScore = Int32.Parse(pTestMessage.GetMessage()) + this.pScore;
                String newScoreString = newScore.ToString().PadLeft(4, '0');
                pTestMessage.UpdateMessage(newScoreString);

                pTestMessage = FontMan.Find(Font.Name.ScoreHigh);
                Debug.Assert(pTestMessage != null);
                if (newScore > Int32.Parse(pTestMessage.GetMessage()))
                {
                    pTestMessage.UpdateMessage(newScoreString);
                }
            }
            else if (SceneStateGame.GetCurrPlayer() == 2)
            {
                Font pTestMessage = FontMan.Find(Font.Name.Score2);
                Debug.Assert(pTestMessage != null);
                int newScore = Int32.Parse(pTestMessage.GetMessage()) + this.pScore;
                String newScoreString = newScore.ToString().PadLeft(4, '0');
                pTestMessage.UpdateMessage(newScoreString);

                pTestMessage = FontMan.Find(Font.Name.ScoreHigh);
                Debug.Assert(pTestMessage != null);
                if (newScore > Int32.Parse(pTestMessage.GetMessage()))
                {
                    pTestMessage.UpdateMessage(newScoreString);
                }
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}
