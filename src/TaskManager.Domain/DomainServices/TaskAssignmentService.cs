using TaskManager.Domain.DomainErrors;
using TaskManager.Domain.Primitives;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects.Projects;
using TaskManager.Domain.ValueObjects.Tasks;

namespace TaskManager.Domain.DomainServices;

public sealed class TaskAssignmentService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public TaskAssignmentService(ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    public Result<Entities.Task> CreateTaskAndAssignToProject(string title, ProjectId projectId)
    {
        var project = _projectRepository.FindById(projectId);
        if (project is null)
        {
            return ProjectErrors.NotFound;
        }

        var createTaskResult = Entities.Task.Create(title, projectId);
        if(createTaskResult.IsFailure)
        {
            return createTaskResult.Error!;
        }

        var task = createTaskResult.Value!;

        _taskRepository.Add(task);

        var addTaskResult = project.AddTask(task.Id);
        if(addTaskResult.IsFailure)
        {
            return addTaskResult.Error!;
        }

        return task;
    }

    public Result DeleteTaskAndUnassignFromProject(TaskId taskId)
    {
        var task = _taskRepository.FindById(taskId);
        if(task is null)
        {
            return TaskErrors.NotFound;
        }

        var project = _projectRepository.FindById(task.ProjectId);
        if (project is null)
        {
            return ProjectErrors.NotFound;
        }

        var removeTaskResult = project.RemoveTask(task.Id);
        if (removeTaskResult.IsFailure)
        {
            return removeTaskResult.Error!;
        }

        _taskRepository.Remove(task);
        _projectRepository.Update(project);

        return Result.Success();
    }
}
