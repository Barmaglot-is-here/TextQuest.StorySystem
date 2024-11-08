using StorySystem.Parsing.Lexer;
using System.Collections.Generic;
using System;
using StorySystem.Parsing.SyntaxTree;

namespace StorySystem.Parsing.Parser
{
    internal partial class Parser
    {
        private Function ParseFunction()
        {
            Function function = new();

            function.Name = ParseName();
            function.Args = ParseArgs();

            return function;
        }

        private string ParseName()
        {
            if (Next().Type != LexemType.Text)
                throw new Exception(CurrentLexem.Type.ToString());

            return CurrentLexem.Content;
        }

        private string[] ParseArgs()
        {
            Queue<string> args = null;

            if (Next().Type != LexemType.ArgsBegin)
                throw new Exception();

            while (CurrentLexem.Type != LexemType.ArgsEnd)
            {
                var next = Next();

                if (next.Type == LexemType.ArgsSeparation)
                    continue;

                if (next.Type == LexemType.Text)
                {
                    args ??= new();

                    args.Enqueue(next.Content);
                }
                else if (next.Type != LexemType.ArgsEnd)
                    throw new Exception();
            }

            return args?.ToArray();
        }
    }
}