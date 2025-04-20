using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Interfaces
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(BackgroundWorkItem workItem);
        Task<BackgroundWorkItem> DequeueAsync(CancellationToken cancellationToken);
    }

    public delegate Task BackgroundWorkItem(CancellationToken token, IUnitOfWork unitOfWork);
}
