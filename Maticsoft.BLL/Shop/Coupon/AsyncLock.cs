using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maticsoft.BLL.Shop.Coupon
{
    public class AsyncLock
    {
        private readonly MultiLock m_sem;
        private readonly Task<Releaser> m_releaser;

        public AsyncLock()
        {
            m_sem = new MultiLock(1);
            m_releaser = Task.FromResult(new Releaser(this));
        }

        public Task<Releaser> LockAsync()
        {
            var wait = m_sem.WaitAsync();
            return wait.IsCompleted ?
                m_releaser :
                wait.ContinueWith((_, state) => new Releaser((AsyncLock)state),
                    this, CancellationToken.None,
                    TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
        }

        public struct Releaser : IDisposable
        {
            private readonly AsyncLock m_toRelease;

            internal Releaser(AsyncLock toRelease) { m_toRelease = toRelease; }

            public void Dispose()
            {
                if (m_toRelease != null)
                    m_toRelease.m_sem.Release();
            }
        }
    }
}
