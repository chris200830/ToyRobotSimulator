using Service.Implementations;
using Service.Interfaces;
using View.ViewModels;
using View.Views;
using Unity;
using View.Interfaces.Handlers;
using View.Implementations.Handlers;

namespace View
{
    internal class AppInitializer
    {
        private UnityContainer unityContainer = new UnityContainer();

        /// <summary>
        /// Initializes the Application.
        /// </summary>
        public void Initialize()
        {
            RegisterDependencies();
            InitializeScreen();
        }

        /// <summary>
        /// Initializes View and ViewModel values.
        /// </summary>
        public void InitializeScreen()
        {
            var tableTopView = new TableTopView();
            var tableTopViewModel = new TableTopViewModel();
            tableTopView.DataContext = tableTopViewModel;
            IViewHandler viewHandler = new ViewHandler(unityContainer.Resolve<IRobotService>(), tableTopViewModel);
            tableTopViewModel.PlaceCommand = new Commands.RelayCommand(viewHandler.HandlePlaceRobot);
            tableTopViewModel.MoveCommand = new Commands.RelayCommand(viewHandler.HandleMoveRobot, viewHandler.VerifyIfRobotIsPlacedOnGrid);
            tableTopViewModel.LeftCommand = new Commands.RelayCommand(viewHandler.HandleTurnLeft, viewHandler.VerifyIfRobotIsPlacedOnGrid);
            tableTopViewModel.RightCommand = new Commands.RelayCommand(viewHandler.HandleTurnRight, viewHandler.VerifyIfRobotIsPlacedOnGrid);
            tableTopViewModel.ReportCommand = new Commands.RelayCommand(viewHandler.HandleReportStatus, viewHandler.VerifyIfRobotIsPlacedOnGrid);
            viewHandler.CreateGrid();
            tableTopView.Show();
        }

        /// <summary>
        /// Registers unity dependencies.
        /// </summary>
        private void RegisterDependencies()
        {
            unityContainer.RegisterType<IRobotService, RobotService>();
            unityContainer.RegisterType<IInputService, InputService>();
        }
    }
}
