using StorySystem.Parsing.Lexer;
using StorySystem.Parsing.SyntaxTree;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorySystem.Parsing.Parser
{
    internal partial class Parser
    {
        private Lexem[] _lexems;
        private int _position;

        private bool LexemsOver => _lexems.Length <= _position + 1;
        private Lexem CurrentLexem => _lexems[_position];

        public TreeRoot Parse(IEnumerable<Lexem> lexems)
        {
            TreeRoot root = new();

            Load(lexems);
            Parse(root);

            return root;
        }

        private void Load(IEnumerable<Lexem> lexems)
        {
            _lexems     = lexems.Where(x => x.Type != LexemType.Comment).ToArray();
            _position   = 0;
        }

        private void Parse(TreeRoot root)
        {
            while (!LexemsOver && CurrentLexem.Type != LexemType.BlockEnd)
            {
                ConstructionType constructionType   = GetConstructionType();
                ISyntaxCounstruction counstruction  = constructionType switch
                {
                    ConstructionType.Condition  => ParseConditionableBlock(),
                    ConstructionType.Action     => ParseFunction(),
                    ConstructionType.Text       => ParseText(),
                    ConstructionType.Insertion  => ParseInsertion(),
                    ConstructionType.Choice     => ParseChoice(),
                    _ => throw new NotImplementedException()
                };

                root.Add(counstruction);

                Skip(1); //Переход к следующей лексеме
            }
        }

        private ConstructionType GetConstructionType() => CurrentLexem.Type switch
        {
            LexemType.If                => ConstructionType.Condition,
            LexemType.Do                => ConstructionType.Action,
            LexemType.Text              => ConstructionType.Text,
            LexemType.InsertionBegin    => ConstructionType.Insertion,
            LexemType.ChoiceBegin       => ConstructionType.Choice,
            _ => throw new Exception(_lexems[_position - 1].Content) //Тут сообщение написать
        };

        private Lexem Next() => _lexems[++_position];

        private void Skip(int count) => _position += count;
    }
}