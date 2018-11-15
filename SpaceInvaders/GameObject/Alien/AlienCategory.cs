using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienCategory : Leaf
    {
        //static private int moveDirectionX = 1;
        //static private int moveDirectionY = 0;

        public enum Type
        {
            // temporary location --> move this
            Squid,
            Crab,
            Octopus,
            Group,
            Column,
            UFO
            //Missile,
            //MissileGroup
        }

        public AlienCategory(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName) { }

        public override void Accept(ColVisitor other)
        {
            throw new NotImplementedException();
        }

        public virtual void SetState(AlienMan.State inState)
        {
            Debug.Assert(false);
        }

        public virtual void DropBomb()
        {
            Debug.Assert(false);
        }
        //public override void Move(float speedX, float speedY)
        //{
        //    float nextPosX = this.GetX() + (speedX * AlienCategory.moveDirectionX);
        //    float nextPosY = this.GetY();
        //    this.SetXY(nextPosX, nextPosY);
        //    //if (AlienCategory.moveDirectionY != 0)
        //    //{
        //    //    MoveDown(speedY * AlienCategory.moveDirectionY);
        //    //}
        //    //AlienCategory.moveDirectionY = 0;
        //    //base.Update();
        //    //base.Move(speedX, speedY);
        //}

        //public void MoveDown(float speedY)
        //{

        //    ForwardIterator pFor = new ForwardIterator(ForwardIterator.GetSibling(this));

        //    AlienCategory pNode = (AlienCategory)pFor.First();
        //    while (!pFor.IsDone())
        //    {
        //        pNode.SetXY(pNode.GetX(), pNode.GetY() + speedY);
        //        pNode.Update();
        //        pNode = (AlienCategory)pFor.Next();
        //    }
        //}

        //public static void ChangeDirection(int x, int y)
        //{
        //    AlienCategory.moveDirectionX = x;
        //    //AlienCategory.moveDirectionY = y;
        //}
    }
}
