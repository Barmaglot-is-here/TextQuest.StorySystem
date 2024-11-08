using StorySystem.Parsing.Lexer;
using StorySystem.Parsing.SyntaxTree;
using System;

namespace StorySystem.Parsing.Parser
{
    internal partial class Parser
    {
        private Choice ParseChoice()
        {
            Choice choice = new();

            if (Next().Type != LexemType.Text)
                throw new Exception();

            choice.Text = CurrentLexem.Content;

            if (Next().Type != LexemType.ChoiceEnd)
                throw new Exception();

            if (Next().Type != LexemType.NextPointer)
                throw new Exception();

            switch (Next().Type)
            {
                case LexemType.Text:
                    choice.Next = CurrentLexem.Content;

                    if (!LexemsOver && Next().Type == LexemType.BlockBegin)
                        goto case LexemType.BlockBegin;
                    else
                        Skip(-1);

                    break;
                case LexemType.BlockBegin:
                    Block block = ParseBlock();

                    choice.Block = block;

                    break;
                default:
                    throw new Exception();
            }

            return choice;
        }

        private Block ParseBlock()
        {
            Block block = new();

            Parse(block);

            return block;
        }
    }
}