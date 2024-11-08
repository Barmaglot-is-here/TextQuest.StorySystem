namespace StorySystem.Parsing.SyntaxTree
{
    internal class Choice : ISyntaxCounstruction
    {
        public string Text { get; set; }
        public string Next { get; set; }
        public Block Block { get; set; }
    }
}