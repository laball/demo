namespace Summary.CodeSnippet
{
    /// <summary>
    /// This class can be used to provide an action when
    /// Dipose method is called.
    /// see 
    /// https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp/DisposeAction.cs
    /// https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.Core/Volo/Abp/DisposeAction.cs
    /// https://github.com/mono/mono/blob/main/mcs/class/referencesource/System.Web/Util/DisposableAction.cs
    /// </summary>
    /// <summary>
    /// This class can be used to provide an action when
    /// Dispose method is called.
    /// </summary>
    public class DisposeAction : IDisposable
    {
        private Action _action;
        private static readonly Action emptyAction = () => { };

        /// <summary>
        /// Creates a new <see cref="DisposeAction"/> object.
        /// </summary>
        /// <param name="action">Action to be executed when this object is disposed.</param>
        public DisposeAction(Action action)
        {
            _action = action ?? emptyAction;
        }

        public void Dispose()
        {
            // Interlocked allows the continuation to be executed only once
            var continuation = Interlocked.Exchange(ref _action, emptyAction);
            continuation?.Invoke();
        }
    }

    /// <summary>
    /// This class can be used to provide an action when
    /// Dispose method is called.
    /// <typeparam name="T">The type of the parameter of the action.</typeparam>
    /// </summary>
    public class DisposeAction<T> : IDisposable
    {
        private Action<T> _action;

        private static readonly Action<T> emptyAction = _ => { };
        private readonly T _parameter;

        /// <summary>
        /// Creates a new <see cref="DisposeAction"/> object.
        /// </summary>
        /// <param name="action">Action to be executed when this object is disposed.</param>
        /// <param name="parameter">The parameter of the action.</param>
        public DisposeAction(Action<T> action, T parameter)
        {
            _action = action ?? emptyAction;
            _parameter = parameter;
        }

        public void Dispose()
        {
            // Interlocked allows the continuation to be executed only once
            var continuation = Interlocked.Exchange(ref _action, emptyAction);
            continuation?.Invoke(_parameter);
        }
    }
}