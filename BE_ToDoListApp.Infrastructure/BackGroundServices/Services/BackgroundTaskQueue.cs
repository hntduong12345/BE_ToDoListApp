using BE_ToDoListApp.Application.Interfaces;
using System.Threading.Channels;

namespace BE_ToDoListApp.Infrastructure.BackGroundServices.Services
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<BackgroundWorkItem> _queue = Channel.CreateUnbounded<BackgroundWorkItem>();

        public void QueueBackgroundWorkItem(BackgroundWorkItem workItem)
        {
            if (workItem == null)
                throw new ArgumentNullException(nameof(workItem));

            _queue.Writer.TryWrite(workItem);
        }

        public async Task<BackgroundWorkItem> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
