using TaskManager.Domain.DomainErrors;
using TaskManager.Domain.DomainServices;
using TaskManager.Domain.Primitives;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects.Projects;
using TaskManager.Domain.ValueObjects.Tasks;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Application.ApplicationServices;

public sealed class TaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly TaskAssignmentService _taskAssignmentService;

    public TaskService(ITaskRepository taskRepository, TaskAssignmentService taskAssignmentService)
    {
        _taskRepository = taskRepository;
        _taskAssignmentService = taskAssignmentService;
    }

    public Result<Domain.Entities.Task> CreateTask(string title, ProjectId projectId)
    {
        return _taskAssignmentService.CreateTaskAndAssignToProject(title, projectId);
    }

    public Result UpdateTaskInfo(TaskId taskId, string newTitle, string newDescription)
    {
        var task = _taskRepository.FindById(taskId);
        if (task is null)
        {
            return TaskErrors.NotFound;
        }

        var titleResult = task.UpdateTitle(newTitle);
        if(titleResult.IsFailure)
        {
            return titleResult.Error!;
        }

        var descriptionResult = task.UpdateDescription(newDescription);
        if(descriptionResult.IsFailure)
        {
            return descriptionResult.Error!;
        }

        _taskRepository.Update(task);

        return Result.Success();
    }

    public Result AssignTaskToUser(TaskId taskId, UserId? userId)
    {
        var task = _taskRepository.FindById(taskId);
        if (task is null)
        {
            return TaskErrors.NotFound;
        }

        var assignResult = task.AssignUser(userId);
        if (assignResult.IsFailure)
        {
            return assignResult.Error!;
        }

        _taskRepository.Update(task);

        return Result.Success();
    }

    public Result CompleteTask(TaskId taskId)
    {
        // Move to Domain service if projects are involved in future.
        var task = _taskRepository.FindById(taskId);
        if (task is null)
        {
            return TaskErrors.NotFound;
        }

        var completeResult = task.Complete();
        if (completeResult.IsFailure)
        {
            return completeResult.Error!;
        }
        
        _taskRepository.Update(task);
        
        return Result.Success();
    }

    public Result ReopenTask(TaskId taskId, Domain.ValueObjects.Tasks.TaskStatus taskStatus)
    {
        var task = _taskRepository.FindById(taskId);
        if (task is null)
        {
            return TaskErrors.NotFound;
        }

        var reopenResult = task.ReOpen(taskStatus);
        if (reopenResult.IsFailure)
        {
            return reopenResult.Error!;
        }
        
        _taskRepository.Update(task);
        
        return Result.Success();
    }

    public Result DeleteTask(TaskId taskId)
    {
        return _taskAssignmentService.DeleteTaskAndUnassignFromProject(taskId);
    }
}
