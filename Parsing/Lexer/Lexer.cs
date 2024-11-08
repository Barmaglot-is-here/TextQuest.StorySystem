using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace StorySystem.Parsing.Lexer
{
    internal static class Lexer
    {
        private readonly static Dictionary<LexemType, Regex> _patterns = new()
        {
            { LexemType.Text,           new(@"^([А-я0-9]+(\-[^>])*([^!][\?\!])*[_,\s\.]*)+", RegexOptions.Compiled) },
            { LexemType.InsertionBegin, new(@"^\s*<\s*",    RegexOptions.Compiled) },
            { LexemType.InsertionEnd,   new(@"^\s*>\s*",    RegexOptions.Compiled) },
            { LexemType.ChoiceBegin,    new(@"^\s*\[\s*",   RegexOptions.Compiled) },
            { LexemType.ChoiceEnd,      new(@"^\s*\]\s*",   RegexOptions.Compiled) },
            { LexemType.NextPointer,    new(@"^\s*->\s*",   RegexOptions.Compiled) },
            { LexemType.If,             new(@"^\s*\?\s*",   RegexOptions.Compiled) },
            { LexemType.And,            new(@"^\s*&\s*",    RegexOptions.Compiled) },
            { LexemType.Or,             new(@"^\s*\|\s*",   RegexOptions.Compiled) },
            { LexemType.ExcludingOr,    new(@"^\s*\^\s*",   RegexOptions.Compiled) },
            { LexemType.Else,           new(@"^\s*:\s*",    RegexOptions.Compiled) },
            { LexemType.Not,            new(@"^\s*~\s*",    RegexOptions.Compiled) },
            { LexemType.Do,             new(@"^\s*!\s*",    RegexOptions.Compiled) },
            { LexemType.BlockBegin,     new(@"^\s*{\s*",    RegexOptions.Compiled) },
            { LexemType.BlockEnd,       new(@"^\s*}\s*",    RegexOptions.Compiled) },
            { LexemType.ArgsBegin,      new(@"^\s*\(\s*",   RegexOptions.Compiled) },
            { LexemType.ArgsEnd,        new(@"^\s*\)\s*",   RegexOptions.Compiled) },
            { LexemType.ArgsSeparation, new(@"^\s*,\s*",    RegexOptions.Compiled) },
            { LexemType.Comment,        new(@";.*",         RegexOptions.Compiled) },
        };

        public static IEnumerable<Lexem> Split(string src)
        {
            Queue<Lexem> lexems = new();

            while (src.Length != 0)
            {
                Lexem lexem = ReadNext(ref src);

                lexems.Enqueue(lexem);
            }

            return lexems;
        }

        private static Lexem ReadNext(ref string text)
        {
            foreach (var pattern in _patterns)
            {
                var match = pattern.Value.Match(text);

                if (match.Success)
                {
                    string content = text[..match.Length];
                    text = text.Remove(0, match.Length).TrimStart();

                    return new(content, pattern.Key);
                }
            }

            throw new Exception($"Unparsable part: {text}");
        }
    }
}