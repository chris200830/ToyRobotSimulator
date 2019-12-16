namespace View.Interfaces.Handlers
{
    public interface IViewHandler
    {
        void HandlePlaceRobot(object obj);
        void HandleMoveRobot(object obj);
        void HandleTurnLeft(object obj);
        void HandleTurnRight(object obj);
        void HandleReportStatus(object obj);
        bool VerifyIfRobotIsPlacedOnGrid(object sender);
        void CreateGrid();
    }
}
