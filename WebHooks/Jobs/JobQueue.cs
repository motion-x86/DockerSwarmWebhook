using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebHooks.Jobs
{
    public class JobQueue
    {
        private ConcurrentQueue<IJob> Queue { get; set; }
        private Thread JobThread { get; set; }

        public JobQueue()
        {
            Queue = new ConcurrentQueue<IJob>();
        }

        public void AddJob(IJob job)
        {
            Queue.Enqueue(job);

            if (!(JobThread?.IsAlive == true))
            {
                JobThread = new Thread(() => DoJobs());
                JobThread.IsBackground = true;
                JobThread.Start();
            }
        }

        public void DoJobs()
        {
            while (Queue.Count > 0)
            {
                Queue.TryDequeue(out IJob job);

                job.Run();
            }
        }
    }

    public interface IJob
    {
        void Run();
    }

}
