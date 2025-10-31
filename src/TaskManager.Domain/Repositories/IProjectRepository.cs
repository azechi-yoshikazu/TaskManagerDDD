using TaskManager.Domain.ValueObjects.Projects;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Domain.Repositories;

public interface IProjectRepository
{
    void Add(Entities.Project project);
    void Update(Entities.Project project);
    void Remove(Entities.Project project);
    Entities.Project? FindById(ProjectId projectId);
    IEnumerable<Entities.Project> FindByOwnerId(UserId userId);
}
