using System.Collections.Generic;

namespace StorySystem.ViewData
{
    public struct StoryBlock
    {
        public string Text { get; internal set; }
        public Queue<Choice> Choices { get; internal set; }
    }
}