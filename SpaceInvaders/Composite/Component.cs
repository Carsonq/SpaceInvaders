using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Component : ColVisitor
    {
        public enum Container
        {
            LEAF,
            COMPOSITE,
            UNKNOWN
        }

        public Component()
        { }

        public Component pParent = null;
        public Component pReverse = null;
        public Container holder = Container.UNKNOWN;

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Print();
        public abstract void Move(float x, float y);
        public abstract Component GetFirstChild();
        public abstract void DumpNode();
    }
}
