using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOMan
    {
        private static UFOMan instance = null;

        // Active
        private UFO pUFO;
        private Bomb pBomb;

        // Reference
        private UFOStateReady pStateReady;
        private UFOStateFlying pStateFlying;
        private UFOStateDropping pStateDropping;
        private UFOStateEnd pStateEnd;

        public enum State
        {
            Ready,
            Flying,
            Dropping,
            End
        }

        private UFOMan()
        {
            this.pStateReady = new UFOStateReady();
            this.pStateFlying = new UFOStateFlying();
            this.pStateEnd = new UFOStateEnd();
            this.pStateDropping = new UFOStateDropping();

            this.pUFO = null;
        }

        public static void Create(SndObserver pSnd)
        {
            // make sure its the first time
            // Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new UFOMan();
            }

            Debug.Assert(instance != null);

            // Stuff to initialize after the instance was created
            instance.pUFO = ActivateUFO(pSnd);
            instance.pUFO.SetState(UFOMan.State.Ready);
        }

        private static UFOMan PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static UFO GetUFO()
        {
            UFOMan pUFOMan = UFOMan.PrivInstance();

            Debug.Assert(pUFOMan != null);
            Debug.Assert(pUFOMan.pUFO != null);

            return pUFOMan.pUFO;
        }

        public static UFOState GetState(State state)
        {
            UFOMan pUFOMan = UFOMan.PrivInstance();
            Debug.Assert(pUFOMan != null);

            UFOState pUFOState = null;

            switch (state)
            {
                case UFOMan.State.Ready:
                    pUFOState = pUFOMan.pStateReady;
                    break;

                case UFOMan.State.Flying:
                    pUFOState = pUFOMan.pStateFlying;
                    break;

                case UFOMan.State.End:
                    pUFOState = pUFOMan.pStateEnd;
                    break;

                case UFOMan.State.Dropping:
                    pUFOState = pUFOMan.pStateDropping;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pUFOState;
        }


        private static UFO ActivateUFO(SndObserver pSnd)
        {
            UFOMan pUFOMan = UFOMan.PrivInstance();
            Debug.Assert(pUFOMan != null);

            UFO pUFO = new UFO(GameObject.Name.UFO, GameSprite.Name.UFO, 690, 650, pSnd);
            pUFOMan.pUFO = pUFO;

            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            pUFO.ActivateCollisionSprite(pSB_Boxes);
            pUFO.ActivateGameSprite(pSB_Aliens);

            GameObject pUFORoot = GameObjectMan.Find(GameObject.Name.UFORoot);
            Debug.Assert(pUFORoot != null);

            pUFORoot.Add(pUFOMan.pUFO);

            return pUFOMan.pUFO;
        }

        public static Bomb ActivateBomb(UFOCategory pUFO)
        {
            UFOMan pUFOMan = UFOMan.PrivInstance();
            Debug.Assert(pUFOMan != null);

            Bomb pBombObj = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombFork, new FallTuning(), pUFO.x, pUFO.y, pUFO);

            pUFOMan.pBomb = pBombObj;

            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Box = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            pBombObj.ActivateCollisionSprite(pSB_Box);
            pBombObj.ActivateGameSprite(pSB_Aliens);

            // Attach the missile to the missile root
            GameObject pBombRoot = GameObjectMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(pBombRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pBombRoot.Add(pUFOMan.pBomb);

            return pUFOMan.pBomb;
        }
    }
}