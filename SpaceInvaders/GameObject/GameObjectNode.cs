using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameObjectNode_Link : DLink { }

    public class GameObjectNode : GameObjectNode_Link
    {
        public GameObject pGameObj;

        public GameObjectNode()
            : base()
        {
            this.Clear();
        }

        public void Set(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            this.pGameObj = pGameObject;
        }

        public GameObject GetGameObj()
        {
            return this.pGameObj;
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }

        private new void Clear()
        {
            this.pGameObj = null;
        }

        public void Dump()
        {
            Debug.Assert(this.pGameObj != null);
            Debug.WriteLine("\t\t     GameObject: {0}", this.GetHashCode());

            this.pGameObj.Dump();
        }
    }
}
