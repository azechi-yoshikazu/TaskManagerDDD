using TaskManager.Domain.Entities;
using TaskManager.Domain.Primitives;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects.Projects;

namespace TaskManager.Domain.DomainServices;

public sealed class ProjectCompletionService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITaskRepository _taskRepository;

    public ProjectCompletionService(IProjectRepository projectRepository, ITaskRepository taskRepository)
    {
        _projectRepository = projectRepository;
        _taskRepository = taskRepository;
    }

    public Result<Project> CompleteProjectWithAllTasks(ProjectId projectId)
    {
        var findProjectResult = _projectRepository.FindById(projectId);
        if (findProjectResult is null)
        {
            return DomainErrors.ProjectErrors.NotFound;
        }

        var project = findProjectResult;

        var projectCompleteResult = project.Complete();
        if (projectCompleteResult.IsFailure)
        {
            return projectCompleteResult.Error!;
        }

        foreach (var taskId in project.Tasks)
        {
            var task = _taskRepository.FindById(taskId);
            if(task is not null)
            {
                var taskCompleteResult = task.Complete();
                if (taskCompleteResult.IsFailure)
                {
                    return taskCompleteResult.Error!;
                }

                _taskRepository.Update(task);
            }
        }

        _projectRepository.Update(project);

        return project;
    }
}
