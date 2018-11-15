using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienMan
    {
        private static AlienMan instance = null;
        private Bomb pBomb;
        private AlienStateReady pStateReady;
        private AlienStateBombFlying pStateBombFlying;
        private AlienStateEnd pStateEnd;

        public enum State
        {
            Ready,
            BombFlying,
            End
        }

        private AlienMan()
        {
            this.pStateReady = new AlienStateReady();
            this.pStateBombFlying = new AlienStateBombFlying();
            this.pStateEnd = new AlienStateEnd();

            this.pBomb = null;
        }

        public static void Create()
        {
            // make sure its the first time
            //Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new AlienMan();
            }

            Debug.Assert(instance != null);
        }

        public static AlienState GetState(State state)
        {
            AlienMan pAlienMan = AlienMan.PrivInstance();
            Debug.Assert(pAlienMan != null);

            AlienState pAlienState = null;

            switch (state)
            {
                case AlienMan.State.Ready:
                    pAlienState = pAlienMan.pStateReady;
                    break;

                case AlienMan.State.BombFlying:
                    pAlienState = pAlienMan.pStateBombFlying;
                    break;

                case AlienMan.State.End:
                    pAlienState = pAlienMan.pStateEnd;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            return pAlienState;
        }

        private static AlienMan PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Bomb ActivateBomb(AlienCategory pAlien)
        {
            AlienMan pAlienMan = AlienMan.PrivInstance();
            Debug.Assert(pAlienMan != null);

            // copy over safe copy
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int iSeed = BitConverter.ToInt32(buffer, 0);
            Random random = new Random(iSeed);

            Bomb pBombObj = null;
            int randint = random.Next(0, 4);
            switch (randint)
            {
                case 0:
                    pBombObj = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombDagger, new FallDagger(), pAlien.x, pAlien.y, pAlien);
                    break;
                case 1:
                    pBombObj = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombRolling, new FallRolling(), pAlien.x, pAlien.y, pAlien);
                    break;
                case 2:
                    pBombObj = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombZigZag, new FallZigZag(), pAlien.x, pAlien.y, pAlien);
                    break;
                case 3:
                    pBombObj = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombStraight, new FallStraight(), pAlien.x, pAlien.y, pAlien);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            pAlienMan.pBomb = pBombObj;

            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Box = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            pBombObj.ActivateCollisionSprite(pSB_Box);
            pBombObj.ActivateGameSprite(pSB_Aliens);

            // Attach the missile to the missile root
            GameObject pBombRoot = GameObjectMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(pBombRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pBombRoot.Add(pAlienMan.pBomb);

            return pAlienMan.pBomb;
        }
    }
}