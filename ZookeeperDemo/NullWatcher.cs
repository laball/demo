using System.Diagnostics;
using System.Threading.Tasks;
using org.apache.zookeeper;

namespace ZookeeperDemo
{
    /// <summary>
    ///     In general don't use this. Only use in the special case that you
    ///     want to ignore results (for whatever reason) in your test. Don't
    ///     use empty watchers in real code!
    /// </summary>
    public class NullWatcher : Watcher
    {
        public static readonly NullWatcher Instance = new NullWatcher();
        private NullWatcher() { }
        public override Task process(WatchedEvent evt)
        {
            Trace.WriteLine($"Watcher Path: {evt.getPath()} ,State: {evt.getState()} ,Type: {evt.get_Type()}");
            return Task.CompletedTask;
        }
    }
}
