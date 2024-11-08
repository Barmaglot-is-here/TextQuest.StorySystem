namespace StorySystem.Parsing.SyntaxTree
{
    internal class ConditionableBlock : Block
    {
        public Condition Condition { get; set; }
        public Block Alt { get; set; }
    }
}