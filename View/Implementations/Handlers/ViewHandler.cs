using DTO.Entities;
using Service.Interfaces;
using System;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using View.Interfaces.Handlers;
using View.ViewModels;
using View.ViewModels.Components;

[assembly: InternalsVisibleTo("ToyRobotSimulatorTests")]
namespace View.Implementations.Handlers
{
    internal class ViewHandler : IViewHandler
    {
        private static string Error => "Error";

        private static string PlaceFile => "placeFile";

        private IRobotService robotService;

        public TableTopViewModel TableTopViewModel { get; set; }

        public Robot Robot { get; set; }

        public ViewHandler(IRobotService robotService, TableTopViewModel tableTopViewModel)
        {
            TableTopViewModel = tableTopViewModel;
            this.robotService = robotService;
            Robot = new Robot();
        }

        /// <summary>
        /// Handles the command to place the Robot.
        /// </summary>
        /// <param name="obj"></param>
        public void HandlePlaceRobot(object obj)
        {
            var previousPosition = Robot.Position;
            try
            {
                robotService.PlaceRobot(Robot, ConfigurationManager.AppSettings[PlaceFile].ToString());
                SwapGridCells(previousPosition);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            TableTopViewModel.IsRobotPlaced = true;
        }

        /// <summary>
        /// Handles the command to move the Robot.
        /// </summary>
        /// <param name="obj"></param>
        public void HandleMoveRobot(object obj)
        {
            var previousPosition = new Position() { X = Robot.Position.X, Y = Robot.Position.Y };
            try
            {
                robotService.MoveRobot(Robot);
                SwapGridCells(previousPosition);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the command to turn the Robot left.
        /// </summary>
        /// <param name="obj"></param>
        public void HandleTurnLeft(object obj)
        {
            robotService.TurnLeft(Robot);
        }

        /// <summary>
        /// Handles the command to turn the Robot right.
        /// </summary>
        /// <param name="obj"></param>
        public void HandleTurnRight(object obj)
        {
            robotService.TurnRight(Robot);
        }

        /// <summary>
        /// Handles the command to report the Robot's status.
        /// </summary>
        /// <param name="obj"></param>
        public void HandleReportStatus(object obj)
        {
            TableTopViewModel.StatusReport = robotService.GetStatusReport(Robot);
        }

        /// <summary>
        /// Verifies if the Robot has been placed on the TableTop Grid or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool VerifyIfRobotIsPlacedOnGrid(object sender)
        {
            return TableTopViewModel.IsRobotPlaced;
        }

        /// <summary>
        /// Creates a 5x5 Grid.
        /// </summary>
        public void CreateGrid()
        {
            for (int rowIndex = 0; rowIndex < 5; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 5; columnIndex++)
                {
                    var tableTopViewComponent = new TableTopViewComponent
                    {
                        Column = columnIndex,
                        Row = rowIndex,
                        GridCellColor = new SolidColorBrush(Colors.SteelBlue)
                    };
                    TableTopViewModel.TableTopViewComponents.Add(tableTopViewComponent);
                }
            }
            TableTopViewModel.Rows = 5;
            TableTopViewModel.Columns = 5;
        }

        /// <summary>
        /// Performs the swap of grid cell Colors on the TableTop View.
        /// </summary>
        /// <param name="previousPosition"></param>
        public void SwapGridCells(Position previousPosition)
        {
            int gridOffsetY = GetGridOffsetValue(previousPosition.Y);

            TableTopViewModel.TableTopViewComponents.Where(x => x.Row == gridOffsetY && x.Column == previousPosition.X).
                    FirstOrDefault().GridCellColor = new SolidColorBrush(Colors.SteelBlue);
            gridOffsetY = GetGridOffsetValue(Robot.Position.Y);
            TableTopViewModel.TableTopViewComponents.Where(x => x.Row == gridOffsetY && x.Column == Robot.Position.X).
                    FirstOrDefault().GridCellColor = new SolidColorBrush(Colors.Yellow);
        }

        /// <summary>
        /// Offsets the Grid so that the origin lies at the coordinates: (0,0).
        /// </summary>
        /// <param name="gridPositionY"></param>
        /// <returns></returns>
        private int GetGridOffsetValue(int gridPositionY)
        {
            if (gridPositionY < 2)
            {
                return gridPositionY + 4 / (gridPositionY + 1);
            }
            else if (gridPositionY > 2)
            {
                return 3 / gridPositionY;
            }
            return gridPositionY;
        }
    }
}
