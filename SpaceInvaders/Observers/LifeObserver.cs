using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class LifeObserver : ColObserver
    {
        int pScore;

        public LifeObserver()
        {
            this.pScore = 0;
        }

        public LifeObserver(LifeObserver b)
        {
            Debug.Assert(b != null);
            this.pScore = b.pScore;
        }

        public override void Notify()
        {
            LifeObserver pCreateShipObserver = new LifeObserver(this);
            DelayedObjectMan.Attach(pCreateShipObserver);
        }

        public override void Execute()
        {
            Font pTestMessage = FontMan.Find(Font.Name.Life);
            Debug.Assert(pTestMessage != null);
            String life = SceneStateGame.GetPlayerLife(SceneStateGame.GetCurrPlayer()).ToString();
            pTestMessage.UpdateMessage(life);
        }
    }
}
