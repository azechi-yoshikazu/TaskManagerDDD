namespace TaskManager.Domain.Repositories;

public interface IProjectRepository
{
    void Add(Entities.Project project);
    void Update(Entities.Project project);
    void Remove(Entities.Project project);
    Entities.Project? FindById(ValueObjects.ProjectId projectId);
}
