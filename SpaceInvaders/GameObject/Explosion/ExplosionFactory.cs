
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ExplosionFactory
    {
        SpriteBatch pSpriteBatch;
        SpriteBatch pBoxSpriteBatch;

        public ExplosionFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pBoxSpriteBatch = SpriteBatchMan.Find(boxSpriteBatchName);
            Debug.Assert(this.pBoxSpriteBatch != null);
        }

        ~ExplosionFactory()
        {
            Debug.WriteLine("~ExplosionFactory():");
            this.pSpriteBatch = null;
            this.pBoxSpriteBatch = null;
        }

        public GameObject Create(GameObject.Name oName, GameSprite.Name sName, GameObject pContainer = null, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObj = null;

            switch (oName)
            {
                case GameObject.Name.AlienDies:
                    pGameObj = new AlienDies(oName, sName, posX, posY);
                    break;

                case GameObject.Name.BombDies:
                    pGameObj = new BombDies(oName, sName, posX, posY);
                    break;

                case GameObject.Name.MissileDies:
                    pGameObj = new MissileDies(oName, sName, posX, posY);
                    break;

                case GameObject.Name.ShipDies:
                    pGameObj = new ShipDies(oName, sName, posX, posY);
                    break;

                case GameObject.Name.UFODies:
                    pGameObj = new UFODies(oName, sName, posX, posY);
                    break;

                case GameObject.Name.MissileBombCol:
                    pGameObj = new MissileBombCol(oName, sName, posX, posY);
                    break;

                case GameObject.Name.ExplosionGroup:
                    pGameObj = new ExplosionGroup(oName, GameSprite.Name.NullObject, 0.0f, 0.0f);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            // Attached to Group
            this.AttachTo(pGameObj, pContainer);
            return pGameObj;
        }

        private void AttachTo(GameObject pGameObj, GameObject col)
        {
            Debug.Assert(pGameObj != null);
            if (col != null)
            {
                col.Add(pGameObj);
                pGameObj.ActivateGameSprite(this.pSpriteBatch);
                pGameObj.ActivateCollisionSprite(this.pBoxSpriteBatch);
            }
        }
    }
}