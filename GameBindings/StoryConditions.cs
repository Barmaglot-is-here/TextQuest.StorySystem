namespace StorySystem
{
    public abstract class StoryConditions : Invokable
    {
        public bool Check(string condition, string[] args) => (bool)Invoke(condition, args);
    }
}