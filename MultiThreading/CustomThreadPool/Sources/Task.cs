using System;

namespace _10.CustomThreadPool
{
    class Task
    {
        private readonly Action<object> _action;
        private readonly object _arg;
        public Task(Action<object> action, object arg)
        {
            _action = action;
            _arg = arg;
        }

        public Action<object> Action => _action;
        public object Arg => _arg;
    }
}
