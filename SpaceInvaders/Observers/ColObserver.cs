using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ColObserver : DLink
    {
        public ColSubject pSubject;
        public abstract void Notify();
        public virtual void Execute()
        {
        }
    }
}