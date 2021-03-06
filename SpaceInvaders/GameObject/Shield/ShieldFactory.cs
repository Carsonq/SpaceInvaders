﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldFactory
    {
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pCollisionSpriteBatch;

        public ShieldFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = SpriteBatchMan.Find(collisionSpriteBatch);
            Debug.Assert(this.pCollisionSpriteBatch != null);
        }

        ~ShieldFactory()
        {
            this.pSpriteBatch = null;
            this.pCollisionSpriteBatch = null;
        }

        public GameObject Create(ShieldCategory.Type type, GameObject.Name gameName, GameObject pContainer = null, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pShield = null;

            switch (type)
            {
                case ShieldCategory.Type.Brick:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick, posX, posY);
                    break;

                case ShieldCategory.Type.LeftTop1:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_LeftTop1, posX, posY);
                    break;

                case ShieldCategory.Type.LeftTop0:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_LeftTop0, posX, posY);
                    break;

                case ShieldCategory.Type.LeftBottom:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_LeftBottom, posX, posY);
                    break;

                case ShieldCategory.Type.RightTop1:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_RightTop1, posX, posY);
                    break;

                case ShieldCategory.Type.RightTop0:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_RightTop0, posX, posY);
                    break;

                case ShieldCategory.Type.RightBottom:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_RightBottom, posX, posY);
                    break;

                case ShieldCategory.Type.Root:
                    pShield = new ShieldRoot(gameName, GameSprite.Name.NullObject, posX, posY);
                    pShield.SetCollisionColor(0.0f, 0.0f, 1.0f);
                    //Debug.Assert(false);
                    break;

                case ShieldCategory.Type.Grid:
                    pShield = new ShieldGrid(gameName, GameSprite.Name.NullObject, posX, posY);
                    pShield.SetCollisionColor(0.0f, 1.0f, 0.0f);
                    break;

                case ShieldCategory.Type.Column:
                    pShield = new ShieldColumn(gameName, GameSprite.Name.NullObject, posX, posY);
                    pShield.SetCollisionColor(1.0f, 0.0f, 0.0f);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            this.AttachTo(pShield, pContainer);
            return pShield;
        }

        private void AttachTo(GameObject pGameObj, GameObject col)
        {
            Debug.Assert(pGameObj != null);
            if (col != null)
            {
                col.Add(pGameObj);
            }

            pGameObj.ActivateGameSprite(this.pSpriteBatch);
            pGameObj.ActivateCollisionSprite(this.pCollisionSpriteBatch);
        }
    }
}