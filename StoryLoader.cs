using StorySystem.Parsing.Lexer;
using StorySystem.Parsing.Parser;
using StorySystem.ViewData;
using System.IO;

namespace StorySystem
{
    public class StoryLoader
    {
        private readonly Parser _parser;
        private readonly Interpritator _interpritator;

        private string _saveFolder;

        public StoryLoader(StoryActions actions, StoryConditions conditions, StoryData data)
        {
            _parser         = new();
            _interpritator  = new(actions, conditions, data);
        }

        public StoryBlock Load(string file)
        {
            //Смысл взятия _saveFolder каждый раз при загрузке файла заключается в 
            //Поддержке относительных путей, когда дирректория меняется вверх или вниз
            _saveFolder = Path.GetDirectoryName(file);

            var src     = File.ReadAllText(file);
            var lexems  = Lexer.Split(src);

            var syntaxTree = _parser.Parse(lexems);

            return _interpritator.Interpritate(syntaxTree);
        }

        public StoryBlock GetFrom(Choice choice)
        {
            string path = Path.Combine(_saveFolder, choice.Next + StoryConfig.FILE_EXTENSION);
            
            return Load(path);
        }
    }
}