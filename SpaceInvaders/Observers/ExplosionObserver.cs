using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ExplosionObserver : ColObserver
    {
        GameObject pGameObj;
        GameObject.Name pGOname;
        GameSprite.Name pGSname;
        ExplosionFactory factory;
        ExplosionGroup container;
        int p;

        public ExplosionObserver(ExplosionFactory factory, GameObject.Name pGOname, GameSprite.Name pGSname, ExplosionGroup container, int p)
        {
            this.pGameObj = null;
            this.factory = factory;
            this.pGOname = pGOname;
            this.pGSname = pGSname;
            this.container = container;
            this.p = p;
        }

        public ExplosionObserver(ExplosionObserver b)
        {
            Debug.Assert(b != null);
            this.pGameObj = b.pGameObj;
        }

        public override void Notify()
        {
            GameObject placeObject;
            if (this.p == 1)
            {
                placeObject = this.pSubject.pObjA;
            }
            else
            {
                placeObject = this.pSubject.pObjB;
            }

            // TODO new ??
            GameObject pGameObjDies = this.factory.Create(this.pGOname, this.pGSname, this.container, placeObject.x, placeObject.y);

            //SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            //SpriteBatch pSB_Box = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            //GameObject pGameObjDies = new AlienDies(this.pGOname, this.pGSname, placeObject.x, placeObject.y);
            //GameObject pExplosionGroup = GameObjectMan.Find(GameObject.Name.ExplosionGroup);
            //Debug.Assert(pExplosionGroup != null);

            //pExplosionGroup.Add(pGameObjDies);

            //pGameObjDies.ActivateGameSprite(pSB_Aliens);
            //pGameObjDies.ActivateCollisionSprite(pSB_Box);

            this.pGameObj = pGameObjDies;

            if (pGameObj.bMarkForDeath == false)
            {
                pGameObj.bMarkForDeath = true;
                //   Delay
                ExplosionObserver pObserver = new ExplosionObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            RemoveSprite pAnimGameObjDies = new RemoveSprite(this.pGameObj);
            TimerMan.Add(TimerEvent.Name.Explosion, pAnimGameObjDies, 0.3f, false);
        }
    }
}