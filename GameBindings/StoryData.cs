namespace StorySystem
{
    public abstract class StoryData
    {
        public string this[string key] => Get(key);
        public abstract string Get(string key);
    }
}