using System.Collections.Generic;
using System.Reflection;

namespace StorySystem
{
    public abstract class Invokable
    {
        private readonly Dictionary<string, MethodInfo> _methods;

        public Invokable()
        {
            _methods = new();
            var type = this.GetType();
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;

            foreach (var method in type.GetMethods(bindingFlags))
            {
                GameFunctionAttribute attribute = method.GetCustomAttribute<GameFunctionAttribute>();

                if (attribute == null)
                    return;

                string name = attribute.Name;
                MethodInfo info = method;

                _methods.Add(name, info);
            }
        }

        protected object Invoke(string function, string[] args)
        {
            var method = _methods[function];

            return method!.Invoke(this, args)!;
        }
    }
}