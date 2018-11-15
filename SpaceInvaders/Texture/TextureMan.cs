using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class TextureMan : Manager
    {
        private static TextureMan pInstance = null;
        private Texture poNodeCompare;

        private TextureMan(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.poNodeCompare = new Texture();
        }

        ~TextureMan()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~TextureMan():{0}", this.GetHashCode());
#endif
            //this.poNodeCompare = null;
            //TextureMan.pInstance = null;
        }

        public static void Destroy()
        {
            // Get the instance
            TextureMan pMan = TextureMan.PrivGetInstance();
            Debug.Assert(pMan != null);

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->TextureMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", TextureMan.pInstance, TextureMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            TextureMan.pInstance = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new TextureMan(reserveNum, reserveGrow);
                TextureMan.Add(Texture.Name.Default, "HotPink.tga");
                TextureMan.Add(Texture.Name.NullObject, "HotPink.tga");
            }
        }

        private static TextureMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static Texture Add(Texture.Name name, String pTextureName)
        {
            TextureMan pMan = TextureMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Texture pNode = (Texture)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Debug.Assert(pTextureName != null);
            pNode.Set(name, pTextureName);

            return pNode;
        }

        public static Texture Find(Texture.Name name)
        {
            TextureMan pMan = TextureMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.name = name;

            Texture pData = (Texture)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Dump()
        {
            TextureMan pMan = TextureMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new Texture();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Texture pDataA = (Texture)pLinkA;
            Texture pDataB = (Texture)pLinkB;

            return pDataA.name == pDataB.name;
        }

        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Texture pNode = (Texture)pLink;
            pNode.Wash();
        }

        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Texture pData = (Texture)pLink;
            pData.Dump();
        }
    }
}
