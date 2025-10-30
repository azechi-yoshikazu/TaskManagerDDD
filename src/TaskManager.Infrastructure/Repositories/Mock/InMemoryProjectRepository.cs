using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects.Projects;

namespace TaskManager.Infrastructure.Repositories.Mock;

public sealed class InMemoryProjectRepository : IProjectRepository
{
    private Dictionary<ProjectId, Project> _projects = new();

    public void Add(Project project)
    {
        if(!_projects.ContainsKey(project.Id))
        {
            _projects[project.Id] = project;
        }
    }

    public Project? FindById(ProjectId projectId)
    {
        if (_projects.TryGetValue(projectId, out var project))
        {
            return project;
        }
        return null;
    }

    public void Remove(Project project)
    {
        if (_projects.ContainsKey(project.Id))
        {
            _projects.Remove(project.Id);
        }
    }

    public void Update(Project project)
    {
        if (_projects.ContainsKey(project.Id))
        {
            _projects[project.Id] = project;
        }
    }
}
