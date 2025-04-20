using AutoMapper;
using BE_ToDoListApp.Application.DTOs.ToDoTaskDTO;
using BE_ToDoListApp.Application.Interfaces;
using BE_ToDoListApp.Application.Services.Interfaces;
using BE_ToDoListApp.Application.Utils;
using BE_ToDoListApp.Domain.Entities;
using BE_ToDoListApp.Domain.Enums.ToDoTaskEnums;
using BE_ToDoListApp.SharedLibrary.Utils;
using Microsoft.AspNetCore.Http;

namespace BE_ToDoListApp.Application.Services.Implements
{
    public class ToDoTaskService : BaseService<ToDoTaskService>, IToDoTaskService
    {
        private readonly IBackgroundTaskQueue _taskQueue;

        public ToDoTaskService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IBackgroundTaskQueue taskQueue)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            _taskQueue = taskQueue;
        }

        public async Task<List<ToDoTaskDTO>> GetDateTasks(string userId, DateOnly date)
        {
            Guid realUserId = Guid.Parse(EncryptUtil.Decrypt(userId));

            List<ToDoTask> tasks = (List<ToDoTask>)await _unitOfWork.GetRepository<ToDoTask>()
                .GetListAsync(predicate: t => t.UserId == realUserId &&
                                              t.CreateDate == date);

            var res = _mapper.Map<List<ToDoTaskDTO>>(tasks);

            return res;
        }

        public async Task ModifyTasks(List<ToDoTaskDTO> data, string userId, DateOnly date)
        {
            Guid realUserId = Guid.Parse(EncryptUtil.Decrypt(userId));

            List<ToDoTask> updatedTasks = _mapper.Map<List<ToDoTask>>(data.Where(d => d.State == "Updated"));
            List<ToDoTask> deletedTasks = _mapper.Map<List<ToDoTask>>(data.Where(d => d.State == "Deleted"));
            List<ToDoTask> createdTasks = _mapper.Map<List<ToDoTask>>(data.Where(d => d.State == "Created"));

            //Add new
            createdTasks.ForEach(x => x.UserId = realUserId);
            await _unitOfWork.GetRepository<ToDoTask>().InsertRangeAsync(createdTasks);

            //Update
            updatedTasks.ForEach(x => x.UserId = realUserId);
            _unitOfWork.GetRepository<ToDoTask>().UpdateRangeAsync(updatedTasks);

            //Delete
            _unitOfWork.GetRepository<ToDoTask>().DeleteRangeAsync(deletedTasks);
            await _unitOfWork.CommitAsync();

            //if (!check) throw new Exception("An error occured when modify tasks");


            //Execute BG service for updating Statistic
            _taskQueue.QueueBackgroundWorkItem(async (token, uow) =>
            {
                await Task.Delay(3000, token);
                await ChangeStatistic(realUserId, date, uow);
            });
        }

        private async Task ChangeStatistic(Guid userId, DateOnly date, IUnitOfWork unitOfWork)
        {
            bool isUpdate = true;
            TaskStatistic statistic = await unitOfWork.GetRepository<TaskStatistic>()
                .GetAsync(predicate: t => t.UserId == userId && t.CalcDate == date);

            if (statistic == null)
            {
                isUpdate = false;
                statistic = new TaskStatistic()
                {
                    Id = Guid.CreateVersion7(),
                    CalcDate = date,
                    Completed = 0,
                    InProgress = 0,
                    NotStarted = 0,
                    UserId = userId
                };
            }

            List<ToDoTask> todoTasks = (List<ToDoTask>)await unitOfWork.GetRepository<ToDoTask>().
                GetListAsync(predicate: t => t.UserId == userId && t.CreateDate == date);

            int totalTasks = todoTasks.Count();
            if (totalTasks == 0)
            {
                statistic.Completed = 0;
                statistic.InProgress = 0;
                statistic.NotStarted = 0;
            }
            else
            {
                int totalCompleted = todoTasks.Where(t => t.Status == (byte)ToDoTaskStatusEnum.Completed).Count();
                int totalinProgress = todoTasks.Where(t => t.Status == (byte)ToDoTaskStatusEnum.InProgress).Count();
                int totalNotStarted = todoTasks.Where(t => t.Status == (byte)ToDoTaskStatusEnum.NotStarted).Count();

                statistic.Completed = Math.Round((decimal)totalCompleted / totalTasks, 3);
                statistic.InProgress = Math.Round((decimal)totalinProgress / totalTasks, 3);
                statistic.NotStarted = Math.Round((decimal)totalNotStarted / totalTasks, 3);
            }

            if (isUpdate)
                unitOfWork.GetRepository<TaskStatistic>().UpdateAsync(statistic);
            else
                await unitOfWork.GetRepository<TaskStatistic>().InsertAsync(statistic);

            await unitOfWork.CommitAsync();
        }

    }
}
