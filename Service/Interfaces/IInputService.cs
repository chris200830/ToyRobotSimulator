using DTO.Entities;

namespace Service.Interfaces
{
    public interface IInputService
    {
        Robot GetRobotFromFile(string filePath);
    }
}
