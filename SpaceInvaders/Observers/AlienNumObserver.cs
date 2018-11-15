using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienNumObserver : ColObserver
    {
        int initial;
        int num;
        IrrKlang.ISoundEngine sndEngine;

        public AlienNumObserver(IrrKlang.ISoundEngine pEng)
        {
            int count = 0;
            GameObject pGameObj = GameObjectMan.Find(GameObject.Name.AlienGroup);

            AlienGroup pAlienGroup = (AlienGroup)pGameObj;
            ForwardIterator pFor = new ForwardIterator(pAlienGroup);

            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                if (pNode.holder == Component.Container.LEAF)
                {
                    count++;
                }
                pNode = pFor.Next();
            }

            this.num = count;
            this.initial = count;
            this.sndEngine = pEng;
        }

        public override void Notify()
        {
            this.num--;

            if (this.num == this.initial/5*4)
            {
                // increate move rate
                // change sound
                TimerMan.UpdateEvent(0.7f);
            }
            else if (this.num == this.initial / 2)
            {
                TimerMan.UpdateEvent(0.7f);
            }
            else if (this.num == this.initial / 5)
            {
                TimerMan.UpdateEvent(0.7f);
            }
            //Debug.WriteLine(" Snd_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            //GameObject.Name name = this.pSubject.pObjB.GetName();


            //Font pTestMessage = FontMan.Find(Font.Name.Score1);
            //Debug.Assert(pTestMessage != null);
            //int newScore = Int32.Parse(pTestMessage.GetMessage()) + this.pScore;
            //String newScoreString = newScore.ToString().PadLeft(4, '0');
            //pTestMessage.UpdateMessage(newScoreString);
        }
    }
}
