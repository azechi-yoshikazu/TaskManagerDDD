using TaskManager.Domain.Primitives;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Domain.DomainServices;

public sealed class UserAssignmentService
{
    private readonly IUserRepository _userRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public UserAssignmentService(IUserRepository userRepository, ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _userRepository = userRepository;
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    public Result DeleteUserAndUnassignTasks(UserId userId)
    {
        var user = _userRepository.FindById(userId);
        if (user is null)
        {
            return DomainErrors.UserErrors.NotFound;
        }

        // Find projects owned by the user and prevent deletion if any exist
        var projects = _projectRepository.FindByOwnerId(userId).ToArray();
        if(projects.Length > 0)
        {
            return DomainErrors.UserErrors.CannotDeleteUserOwningProjects;
        }

        var tasks = _taskRepository.FindByUserId(userId);
        foreach (var task in tasks)
        {
            var unassignResult = task.AssignUser(null);
            if (unassignResult.IsFailure)
            {
                return unassignResult.Error!;
            }

            _taskRepository.Update(task);
        }

        _userRepository.Remove(user);

        return Result.Success();
    }
}
