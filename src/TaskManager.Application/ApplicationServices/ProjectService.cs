using TaskManager.Domain.DomainServices;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Primitives;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects.Projects;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Application.ApplicationServices;

public class ProjectService
{
    private readonly ProjectCompletionService _projectCompletionService;
    private readonly IProjectRepository _projectRepository;

    public ProjectService(ProjectCompletionService projectCompletionService, IProjectRepository projectRepository)
    {
        _projectCompletionService = projectCompletionService;
        _projectRepository = projectRepository;
    }

    public Result<Project> CreateProject(string projectName, UserId ownerId)
    {
        var createResult = Project.Create(projectName, ownerId);
        if (createResult.IsFailure)
        {
            return createResult.Error!;
        }

        var project = createResult.Value!;
        _projectRepository.Add(project);

        return project;
    }

    public Result<Project> UpdateProjectInfo(ProjectId projectId, string newProjectName, string newProjectDescription)
    {
        var project = _projectRepository.FindById(projectId);
        if (project is null)
        {
            return Domain.DomainErrors.ProjectErrors.NotFound;
        }
        var nameResult = project.UpdateName(newProjectName);
        if (nameResult.IsFailure)
        {
            return nameResult.Error!;
        }

        var descriptionResult = project.UpdateDescription(newProjectDescription);
        if (descriptionResult.IsFailure)
        {
            return descriptionResult.Error!;
        }

        _projectRepository.Update(project);

        return project;
    }

    public Result<Project> AddMemberToProject(ProjectId projectId, UserId userId)
    {
        var project = _projectRepository.FindById(projectId);
        if (project is null)
        {
            return Domain.DomainErrors.ProjectErrors.NotFound;
        }

        var addMemberResult = project.AddMember(userId);
        if (addMemberResult.IsFailure)
        {
            return addMemberResult.Error!;
        }

        _projectRepository.Update(project);
        
        return project;
    }

    public Result<Project> RemoveMemberFromProject(ProjectId projectId, UserId userId)
    {
        var project = _projectRepository.FindById(projectId);
        if (project is null)
        {
            return Domain.DomainErrors.ProjectErrors.NotFound;
        }

        var removeMemberResult = project.RemoveMember(userId);
        if (removeMemberResult.IsFailure)
        {
            return removeMemberResult.Error!;
        }
        
        _projectRepository.Update(project);

        return project;
    }

    public Result<Project> CompleteProject(ProjectId projectId)
    {
        return _projectCompletionService.CompleteProjectWithAllTasks(projectId);
    }

    public Result<Project> ArchiveProject(ProjectId projectId)
    {
        var project = _projectRepository.FindById(projectId);
        if (project is null)
        {
            return Domain.DomainErrors.ProjectErrors.NotFound;
        }

        var archiveResult = project.Archive();
        if (archiveResult.IsFailure)
        {
            return archiveResult.Error!;
        }

        _projectRepository.Update(project);

        return project;
    }

    public Result DeleteProject(ProjectId projectId)
    {
        var project = _projectRepository.FindById(projectId);
        if (project is null)
        {
            return Domain.DomainErrors.ProjectErrors.NotFound;
        }

        _projectRepository.Remove(project);
        return Result.Success();
    }
}
