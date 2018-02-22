using System;

namespace S2p.RestClient.Sdk.Infrastructure
{
    public abstract class DisposableBase : IDisposable
    {
        private bool _disposed = false;
        protected object DisposeLock { get; } = new object();

        protected bool Disposed 
        {
            get
            {
                lock (DisposeLock)
                {
                    return _disposed;
                }
            }
        }

        protected void CheckIfDisposed()
        {
            if (Disposed) { throw new ObjectDisposedException("Object is already disposed"); }
        }

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            lock (DisposeLock)
            {
                if (_disposed) { return; }

                Dispose(true);
                GC.SuppressFinalize(this);

                _disposed = true;
            }
        }

        ~DisposableBase()
        {
            Dispose(false);
        }
    }
}
