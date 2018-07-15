using System;
using System.Collections.Generic;
using System.Text;

namespace Masinc.Miscs
{
    public sealed class ResourceManager : IDisposable
    {
        readonly List<IDisposable> resource;
        bool disposed;
        readonly object disposed_lock;

        public ResourceManager()
        {
            resource = new List<IDisposable>();
            disposed_lock = new object();
        }

        public void Add(IDisposable disposable)
        {
            resource.Add(disposable);
        }

        ~ResourceManager()
        {
            lock (disposed_lock)
            {
                if (!disposed)
                {
                    Dispose(false);
                }
            }
        }

        public void Dispose()
        {
            lock (disposed_lock)
            {
                if (!disposed)
                {
                    Dispose(true);
                }
            }
        }

        void Dispose(bool disposing)
        {
            foreach (var d in resource)
            {
                d.Dispose();
            }
            this.disposed = true;
        }
    }

    public static partial class Misc
    {
        public static TDisposable AddResourceManager<TDisposable>(this TDisposable @this, ResourceManager resourceManager)
            where TDisposable : IDisposable
        {
            resourceManager.Add(@this);
            return @this;
        }
    }
}
