namespace StorySystem.ViewData
{
    public struct Choice
    {
        public string Text { get; }
        public string[] Attributes { get; }
        internal string Next { get; }

        internal Choice(string text, string[] attributes, string next)
        {
            Text        = text;
            Attributes  = attributes;
            Next        = next;
        }
    }
}