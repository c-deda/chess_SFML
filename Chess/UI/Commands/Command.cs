using System;

namespace Chess.UI
{
    abstract class Command
    {
        public abstract void Execute();
        public virtual void Undo()
        {
            throw new InvalidOperationException("Must override this method to use.");
        }
    }
}