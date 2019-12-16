using DTO.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Interfaces;
using System.Linq;
using System.Windows.Media;
using View.Implementations.Handlers;
using View.ViewModels;
using View.ViewModels.Components;

namespace ToyRobotSimulatorTests.View
{
    [TestClass]
    public class ViewHandlerTest
    {
        private Mock<IRobotService> robotServiceMock;
        private ViewHandler viewHandler;

        public ViewHandlerTest()
        {
            robotServiceMock = new Mock<IRobotService>();
            TableTopViewModel tableTopViewModel = new TableTopViewModel();
            viewHandler = new ViewHandler(robotServiceMock.Object, tableTopViewModel);
            InitializeTableTopTestGrid(tableTopViewModel);
            viewHandler.TableTopViewModel = tableTopViewModel;
        }

        private void InitializeTableTopTestGrid(TableTopViewModel tableTopViewModel)
        {
            for (int index = 0; index < 5; index++)
            {
                var tableTopViewComponent = new TableTopViewComponent
                {
                    Column = 0,
                    Row = index,
                    GridCellColor = new SolidColorBrush(Colors.SteelBlue)
                };
                tableTopViewModel.TableTopViewComponents.Add(tableTopViewComponent);
            }
        }

        [TestMethod]
        public void Test_HandlePlaceRobot_OK()
        {
            //Arrange
            viewHandler.Robot.Position.X = 0;
            viewHandler.Robot.Position.Y = 0;
            viewHandler.TableTopViewModel.IsRobotPlaced = false;
            robotServiceMock.Setup(x => x.PlaceRobot(It.IsAny<Robot>(), It.IsAny<string>())).Verifiable();

            //Act
            viewHandler.HandlePlaceRobot(null);

            //Assert
            Assert.IsTrue(viewHandler.TableTopViewModel.IsRobotPlaced);
        }

        [TestMethod]
        public void Test_HandleMoveRobot_OK()
        {
            //Arrange
            viewHandler.Robot.Position.X = 0;
            viewHandler.Robot.Position.Y = 0;
            
            //Verify
            robotServiceMock.Setup(x => x.MoveRobot(It.IsAny<Robot>())).Verifiable();
        }

        [TestMethod]
        public void Test_HandleReportStatus_OK()
        {
            //Arrange
            string expectedStatusReport = "test";
            robotServiceMock.Setup(x => x.GetStatusReport(It.IsAny<Robot>())).Returns(expectedStatusReport);
            
            //Act
            viewHandler.HandleReportStatus(null);

            //Assert
            Assert.AreEqual(expectedStatusReport, viewHandler.TableTopViewModel.StatusReport);
        }

        [TestMethod]
        public void Test_VerifyIfRobotIsPlacedOnGrid_OK()
        {
            //Arrange
            viewHandler.TableTopViewModel.IsRobotPlaced = true;

            //Act
            var result = viewHandler.VerifyIfRobotIsPlacedOnGrid(null);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_SwapGridCells_WhenYCoordinateIsNotEqualToTwo_OK()
        {
            //Arrange
            var previousPositionTest = new Position() { X = 0, Y = 0 };
            viewHandler.Robot.Position.Y = 4;
            var previousGridCellTest = viewHandler.TableTopViewModel.TableTopViewComponents.Where(x => x.Row == 4 && x.Column == 0).FirstOrDefault();
            previousGridCellTest.GridCellColor = new SolidColorBrush(Colors.Azure);
            var nextCellTest = viewHandler.TableTopViewModel.TableTopViewComponents.Where(x => x.Row == 0 && x.Column == 0).FirstOrDefault();
            //Act
            viewHandler.SwapGridCells(previousPositionTest);

            //Assert
            Assert.AreEqual(Colors.SteelBlue, previousGridCellTest.GridCellColor.Color);
            Assert.AreEqual(Colors.Green, nextCellTest.GridCellColor.Color);
        }

        [TestMethod]
        public void Test_SwapGridCells_WhenYCoordinateIsEqualToTwo_OK()
        {
            //Arrange
            var previousPositionTest = new Position() { X = 0, Y = 0 };
            viewHandler.Robot.Position.Y = 2;
            var previousGridCellTest = viewHandler.TableTopViewModel.TableTopViewComponents.Where(x => x.Row == 4 && x.Column == 0).FirstOrDefault();
            previousGridCellTest.GridCellColor = new SolidColorBrush(Colors.Azure);
            var nextCellTest = viewHandler.TableTopViewModel.TableTopViewComponents.Where(x => x.Row == 2 && x.Column == 0).FirstOrDefault();
            //Act
            viewHandler.SwapGridCells(previousPositionTest);

            //Assert
            Assert.AreEqual(Colors.SteelBlue, previousGridCellTest.GridCellColor.Color);
            Assert.AreEqual(Colors.Green, nextCellTest.GridCellColor.Color);
        }

        [TestMethod]
        public void Test_CreateGrid_OK()
        {
            //Arrange
            viewHandler.TableTopViewModel.TableTopViewComponents = new Caliburn.Micro.BindableCollection<TableTopViewComponent>();
            int expectedCount = 25;
            //Act
            viewHandler.CreateGrid();
            //Assert
            Assert.AreEqual(expectedCount, viewHandler.TableTopViewModel.TableTopViewComponents.Count);
        }

        [TestMethod]
        public void Test_HandleTurnLeft_OK()
        {
            //Arrange
            viewHandler.Robot.Position.X = 0;
            viewHandler.Robot.Position.Y = 0;

            //Verify
            robotServiceMock.Setup(x => x.TurnLeft(It.IsAny<Robot>())).Verifiable();
        }

        [TestMethod]
        public void Test_HandleTurnRight_OK()
        {
            //Arrange
            viewHandler.Robot.Position.X = 0;
            viewHandler.Robot.Position.Y = 0;

            //Verify
            robotServiceMock.Setup(x => x.TurnRight(It.IsAny<Robot>())).Verifiable();
        }
    }
}
