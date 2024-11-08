using System.Collections.Generic;

namespace StorySystem.Parsing.Parser
{
    internal static class Postprocessor
    {
        private delegate string PostProcessDelegate(string original);

        private static readonly Dictionary<ConstructionType, PostProcessDelegate> _processors = new()
    {
        {ConstructionType.Insertion, _ => _.Trim() },
        {ConstructionType.Text, _      => _.Replace("\\" + "n", "\n") },
        {ConstructionType.Action, _    => _.Trim() },
        {ConstructionType.Choice, _    => _.Trim() },
    };

        public static string Process(ConstructionType processorType, string str)
            => _processors[processorType](str);
    }
}