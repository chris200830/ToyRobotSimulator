using DTO.Entities;

namespace Service.Interfaces
{
    public interface IRobotService
    {
        void PlaceRobot(Robot robot, string placeFilePath);

        void MoveRobot(Robot robot);

        void TurnLeft(Robot robot);

        void TurnRight(Robot robot);

        string GetStatusReport(Robot robot);
    }
}
