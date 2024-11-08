using System.Collections;
using System.Collections.Generic;

namespace StorySystem.Parsing.SyntaxTree
{
    internal class TreeRoot : IEnumerable<ISyntaxCounstruction>
    {
        private readonly Queue<ISyntaxCounstruction> _syntaxCounstructions;

        public TreeRoot() => _syntaxCounstructions = new();

        public void Add(ISyntaxCounstruction counstruction) => _syntaxCounstructions.Enqueue
            (counstruction);

        public IEnumerator<ISyntaxCounstruction> GetEnumerator()
            => _syntaxCounstructions.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _syntaxCounstructions.GetEnumerator();
    }
}