using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class TimerEvent : DLink
    {
        public enum Name
        {
            SquidAnimation,
            CrabAnimation,
            OctopusAnimation,
            AlienMovement,
            AnimShip,
            Explosion,
            Uninitialized
        }

        private Name name;
        private Command pCommand;
        private float deltaTime;
        private float triggerTime;
        private bool repeat;

        public TimerEvent()
            :base()
        {
            this.Clear();
        }

        public void Set(TimerEvent.Name name, Command pCommand, float deltaTimeToTrigger, float currTime, bool repeat = true)
        {
            Debug.Assert(pCommand != null);

            this.name = name;
            this.pCommand = pCommand;
            this.deltaTime = deltaTimeToTrigger;
            this.triggerTime = currTime + deltaTimeToTrigger;
            this.repeat = repeat;
        }

        public void SetDeltaTime(float deltaTimeToTrigger)
        {
            this.deltaTime = deltaTimeToTrigger;
        }

        public Command GetCommand()
        {
            return this.pCommand;
        }

        public void Process()
        {
            Debug.Assert(this.pCommand != null);
            this.pCommand.Execute(this.deltaTime, this.repeat);
        }

        public float GetTriggerTime()
        {
            return this.triggerTime;
        }

        public float GetDeltaTime()
        {
            return this.deltaTime;
        }

        public Name GetName()
        {
            return this.name;
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }

        private new void Clear()
        {
            this.name = TimerEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
            this.repeat = true;
        }

        public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("      Command: {0}", this.pCommand);
            Debug.WriteLine("   Event Name: {0}", this.name);
            Debug.WriteLine(" Trigger Time: {0}", this.triggerTime);
            Debug.WriteLine("   Delta Time: {0}", this.deltaTime);

            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                TimerEvent pTmp = (TimerEvent)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                TimerEvent pTmp = (TimerEvent)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
    }
}
