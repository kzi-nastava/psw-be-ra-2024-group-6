namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface IRequiredEquipmentRepository
{
    public RequiredEquipment Create(RequiredEquipment requiredEquipment);
    public void Delete(long id);
    public ICollection<RequiredEquipment> GetAllByTourId(int tourId);
}