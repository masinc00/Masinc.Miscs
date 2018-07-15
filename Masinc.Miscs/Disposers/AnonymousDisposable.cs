using System;

namespace Masinc.Miscs.Disposers
{
    internal class AnonymousDisposable : IDisposable
    {
        Action<bool> DoDispose;

        bool disposed;
        object disposed_lock = new object();

        internal AnonymousDisposable(Action<bool> doDispose)
        {
            this.DoDispose = doDispose;
        }

        ~AnonymousDisposable()
        {
            lock (disposed_lock)
            {
                if (!disposed)
                {
                    DoDispose(false);
                    disposed = true;
                }
            }
        }


        public void Dispose()
        {
            lock (disposed_lock)
            {
                if (!disposed)
                {
                    DoDispose(true);
                    disposed = true;
                }
            }
        }
    }
}
namespace Masinc.Miscs
{ 
    public static partial class Misc
    {
        /// <summary>
        /// disposeを持つスレッドセーフな無名IDisposableインターフェイスを返す
        /// </summary>
        /// <param name="dispose">
        /// disposeメソッド 
        /// 引数: bool disposing
        ///   disposeが明示的に呼ばれた場合trueを返す
        ///   デストラクタで呼ばれた場合falseを返す
        /// disposeメソッドは一度より多く呼ばれることはない
        /// </param>
        /// <returns>スレッドセーフな無名IDisposableインターフェイス</returns>
        public static IDisposable Disposable(Action<bool> dispose)
        {
            return new Disposers.AnonymousDisposable(dispose);
        }
    }
}
