using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameSpriteMan : Manager
    {
        private static GameSpriteMan pInstance = null;
        private GameSprite poNodeCompare;

        private GameSpriteMan(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.poNodeCompare = new GameSprite();
        }

        ~GameSpriteMan()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~GameSpriteMan():{0}", this.GetHashCode());
#endif
            //this.poNodeCompare = null;
            //GameSpriteMan.pInstance = null;
        }

        public static void Destroy()
        {
            // Get the instance
            GameSpriteMan pMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->GameSpriteMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", GameSpriteMan.pInstance, GameSpriteMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            GameSpriteMan.pInstance = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            // Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GameSpriteMan(reserveNum, reserveGrow);

                // Add a NULL Sprite into the Manager, allows find 
                GameSprite pGSprite = GameSpriteMan.Add(GameSprite.Name.NullObject, Image.Name.NullObject, 0, 0, 0, 0, 0, 0, 0, 0);
                Debug.Assert(pGSprite != null);
            }
        }

        private static GameSpriteMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static GameSprite Add(GameSprite.Name name, Image.Name ImageName, float x, float y, float width, float height, float r, float g, float b, float a)
        {
            GameSpriteMan pMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameSprite pNode = (GameSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Image pImage = ImageMan.Find(ImageName);
            //Debug.Assert(pImage != null);
            if (pImage == null)
            {
                pImage = ImageMan.Find(Image.Name.Default);
                Debug.Assert(pImage != null);
            }

            pNode.Set(name, pImage, x, y, width, height, new Azul.Color(r, g, b, a));

            return pNode;
        }

        public static GameSprite Find(GameSprite.Name name)
        {
            GameSpriteMan pMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.name = name;

            GameSprite pData = (GameSprite)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Remove(GameSprite.Name name)
        {
            GameSpriteMan pMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameSprite pNode = Find(name);
            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void Dump()
        {
            GameSpriteMan pMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new GameSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameSprite pDataA = (GameSprite)pLinkA;
            GameSprite pDataB = (GameSprite)pLinkB;

            return pDataA.name == pDataB.name;
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameSprite pNode = (GameSprite)pLink;
            pNode.Wash();
        }

        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameSprite pData = (GameSprite)pLink;
            pData.Dump();
        }
    }
}
