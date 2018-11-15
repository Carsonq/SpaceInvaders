using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimExplosionObserver : ColObserver
    {
        GameObject pGameObj;
        GameObject.Name pGOname;
        GameSprite.Name pGSname;
        ExplosionFactory factory;
        ExplosionGroup container;
        int p;

        public AnimExplosionObserver(ExplosionFactory factory, GameObject.Name pGOname, GameSprite.Name pGSname, ExplosionGroup container, int p)
        {
            this.pGameObj = null;
            this.factory = factory;
            this.pGOname = pGOname;
            this.pGSname = pGSname;
            this.container = container;
            this.p = p;
        }

        public AnimExplosionObserver(AnimExplosionObserver b)
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
                AnimExplosionObserver pObserver = new AnimExplosionObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            AnimationSprite pAnimShip = new AnimationSprite(GameSprite.Name.ShipDies);

            pAnimShip.Attach(Image.Name.ShipDiesB);
            pAnimShip.Attach(Image.Name.ShipDiesA);

            TimerMan.Add(TimerEvent.Name.AnimShip, pAnimShip, 0.1f, true);

            RemoveSprite pAnimGameObjDies = new RemoveSprite(this.pGameObj);
            TimerMan.Add(TimerEvent.Name.Explosion, pAnimGameObjDies, 0.3f, false);
        }
    }
}