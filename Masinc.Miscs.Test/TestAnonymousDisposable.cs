using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Masinc.Miscs.Disposers;
using System.Linq;
using System.Threading.Tasks;

namespace Masinc.Miscs.Test
{
    [TestClass]
    public class TestAnonymousDisposable
    {
        [TestMethod]
        public void DisposeTest()
        {
            var i = 0;
            var d = Misc.Disposable(x => i++);
            d.Dispose();
            Assert.AreEqual(i, 1);
        }

        [TestMethod]
        public void DisposeTest2()
        {
            var i = 0;
            var d = Misc.Disposable(x => i++);

            var r = Enumerable.Range(0, 10)
                .Select(_ => { d.Dispose(); return 1; }).Sum();
            Assert.AreEqual(i, 1);
            Assert.AreEqual(r, 10);
        }

    }
}
