using System;

namespace StorySystem
{
    public abstract class StoryActions : Invokable
    {
        public new void Invoke(string function, string[] args)
        {
            try
            {
                base.Invoke(function, args);
            }
            catch
            {
                Console.WriteLine("Function: " + function);

                if (args != null)
                    foreach (string arg in args)
                        Console.WriteLine("Arg: " + arg);

                throw;
            }
        }
    }
}