using StorySystem.Parsing.Lexer;
using StorySystem.Parsing.SyntaxTree;
using System;

namespace StorySystem.Parsing.Parser
{
    internal partial class Parser
    {
        private ConditionableBlock ParseConditionableBlock()
        {
            SyntaxTree.Condition condition = new();
            ParseCondition(condition);

            return ParseBlock(condition);
        }

        private SyntaxTree.Condition ParseCondition(SyntaxTree.Condition condition)
        {
            var next = Next();

            while (CurrentLexem.Type != LexemType.BlockBegin 
                && CurrentLexem.Type != LexemType.ArgsEnd)
            {
                switch (next.Type)
                {
                    case LexemType.Text:
                        Skip(-1);

                        Function function = ParseFunction();

                        condition.Function = function;

                        next = Next();

                        condition.Postfix = next.Type switch
                        {
                            LexemType.And => ConditionPostfix.And,
                            LexemType.Or => ConditionPostfix.Or,
                            LexemType.ExcludingOr => ConditionPostfix.ExcludingOr,
                            _ => ConditionPostfix.None
                        };

                        if (condition.Postfix == ConditionPostfix.None)
                            Skip(-1);

                        break;
                    case LexemType.Not:
                        condition.InvertResult = true;

                        break;
                    case LexemType.ArgsBegin:
                        SyntaxTree.Condition sub = new();
                        ParseCondition(sub);

                        condition.Add(sub);

                        break;
                    default:
                        throw new Exception();
                }
            }

            return condition;
        }

        private ConditionableBlock ParseBlock(SyntaxTree.Condition condition)
        {
            Skip(1); //Skip block begin lexem

            ConditionableBlock block    = new();
            block.Condition             = condition;

            Parse(block);

            if (Next().Type == LexemType.Else)
            {
                if (Next().Type == LexemType.If)
                    block.Alt = ParseConditionableBlock();
                else
                {
                    Skip(1); //Skip block begin lexem

                    block.Alt = new();
                }
                
                Parse(block.Alt);
            }

            return block;
        }
    }
}