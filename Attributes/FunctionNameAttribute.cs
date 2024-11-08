using System;

namespace StorySystem
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class GameFunctionAttribute : Attribute
    {
        public string Name { get; }

        public GameFunctionAttribute(string name)
        {
            Name = name;
        }
    }
}