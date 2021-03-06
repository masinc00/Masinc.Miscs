﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Masinc.Miscs.Disposers
{
    public sealed class ResourceDisposer : IDisposable
    {
        readonly List<IDisposable> resource;
        bool disposed;
        readonly object disposed_lock;

        public ResourceDisposer()
        {
            resource = new List<IDisposable>();
            disposed_lock = new object();
        }

        public void Add(IDisposable disposable)
        {
            resource.Add(disposable);
        }

        ~ResourceDisposer()
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
}
namespace Masinc.Miscs
{
    public static partial class MiscExtensions
    {
        public static TDisposable AddResourceManager<TDisposable>(this TDisposable @this, Disposers.ResourceDisposer resourceManager)
            where TDisposable : IDisposable
        {
            resourceManager.Add(@this);
            return @this;
        }
    }
}
