using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Infrastructure.Repositories.Mock;

public sealed class InMemoryProjectRepository : IProjectRepository
{
    public void Add(Project project)
    {
        throw new NotImplementedException();
    }

    public Project? FindById(ProjectId projectId)
    {
        throw new NotImplementedException();
    }

    public void Remove(Project project)
    {
        throw new NotImplementedException();
    }

    public void Update(Project project)
    {
        throw new NotImplementedException();
    }
}
