
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        SpriteBatch pSpriteBatch;
        SpriteBatch pBoxSpriteBatch;

        public AlienFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pBoxSpriteBatch = SpriteBatchMan.Find(boxSpriteBatchName);
            Debug.Assert(this.pBoxSpriteBatch != null);
        }

        ~AlienFactory()
        {
            Debug.WriteLine("~AlienFactory():");
            this.pSpriteBatch = null;
            this.pBoxSpriteBatch = null;
        }

        public GameObject Create(GameObject.Name name, AlienCategory.Type type, GameObject pContainer = null, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case AlienCategory.Type.Squid:
                    pGameObj = new Squid(GameObject.Name.Squid, GameSprite.Name.Squid, posX, posY);
                    AlienCategory pAlienCategory = (AlienCategory)pGameObj;
                    pAlienCategory.SetState(AlienMan.State.Ready);
                    break;

                case AlienCategory.Type.Crab:
                    pGameObj = new Crab(GameObject.Name.Crab, GameSprite.Name.Crab, posX, posY);
                    pAlienCategory = (AlienCategory)pGameObj;
                    pAlienCategory.SetState(AlienMan.State.Ready);
                    break;

                case AlienCategory.Type.Octopus:
                    pGameObj = new Octopus(GameObject.Name.Octopus, GameSprite.Name.Octopus, posX, posY);
                    pAlienCategory = (AlienCategory)pGameObj;
                    pAlienCategory.SetState(AlienMan.State.Ready);
                    break;

                case AlienCategory.Type.Group:
                    pGameObj = new AlienGroup(name, GameSprite.Name.NullObject, posX, posY);
                    break;

                case AlienCategory.Type.Column:
                    pGameObj = new AlienColumn(name, GameSprite.Name.NullObject, posX, posY);
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
            }

            pGameObj.ActivateGameSprite(this.pSpriteBatch);
            pGameObj.ActivateCollisionSprite(this.pBoxSpriteBatch);
        }
    }
}