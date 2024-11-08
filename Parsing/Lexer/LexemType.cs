namespace StorySystem.Parsing.Lexer
{
    internal enum LexemType
    {
        Text,
        InsertionBegin,
        InsertionEnd,
        ChoiceBegin,
        ChoiceEnd,
        NextPointer,
        If,
        And,
        Or,
        ExcludingOr,
        Else,
        Not,
        Do,
        BlockBegin,
        BlockEnd,
        ArgsBegin,
        ArgsEnd,
        ArgsSeparation,
        Comment
    }
}