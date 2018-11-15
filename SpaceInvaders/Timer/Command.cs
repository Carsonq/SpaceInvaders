using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class Command
    {
        abstract public void Execute(float deltaTime, bool repeat);
        abstract public void UpdateRange(int delta);
    }
}
