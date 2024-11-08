using System.Collections;
using System.Collections.Generic;

namespace StorySystem.Parsing.SyntaxTree
{
    internal class Condition : IEnumerable<Condition>
    {
        private Queue<Condition> _conditions;

        public bool InvertResult { get; set; }

        public ConditionPostfix Postfix { get; set; }

        public Function Function { get; set; }

        public Condition()
        {
            _conditions = new();
        }

        public void Add(Condition condition) => _conditions.Enqueue(condition);

        public IEnumerator<Condition> GetEnumerator() => 
            _conditions.Count != 0 ? _conditions.GetEnumerator() : This();

        private IEnumerator<Condition> This()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}