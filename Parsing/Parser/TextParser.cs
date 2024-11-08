using StorySystem.Parsing.Lexer;
using StorySystem.Parsing.SyntaxTree;
using System;

namespace StorySystem.Parsing.Parser
{
    internal partial class Parser
    {
        private Text ParseText(bool isInsertion = false)
        {
            Text text = new();

            text.Content        = CurrentLexem.Content;
            text.IsInsertion    = isInsertion;

            return text;
        }

        private Text ParseInsertion()
        {
            Skip(1); //Skip insertion begin lexem

            Text insertion = ParseText(true);

            if (Next().Type != LexemType.InsertionEnd)
                throw new Exception(CurrentLexem.Type.ToString());

            return insertion;
        }
    }
}