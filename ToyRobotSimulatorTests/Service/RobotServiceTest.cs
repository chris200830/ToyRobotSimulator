using DTO.Entities;
using DTO.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Implementations;
using Service.Interfaces;
using System;

namespace ToyRobotSimulatorTests.Service
{
    [TestClass]
    public class RobotServiceTest
    {
        private RobotService robotService;
        private Mock<IInputService> inputServiceMock;
        public RobotServiceTest()
        {
            inputServiceMock = new Mock<IInputService>();
            robotService = new RobotService(inputServiceMock.Object);
        }

        [TestMethod]
        public void Test_PlaceRobot_WhenCoordinatesAreInsideBounds_OK()
        {
            //Arrange
            var testRobot = new Robot();
            int expectedX = 1;
            int expectedY = 2;
            DirectionType expectedDirection = DirectionType.East;
            var inputServiceRobot = new Robot();
            inputServiceRobot.Position.X = expectedX;
            inputServiceRobot.Position.Y = expectedY;
            inputServiceRobot.Direction = expectedDirection;
            inputServiceMock.Setup(x => x.GetRobotFromFile(It.IsAny<string>())).Returns(inputServiceRobot);

            //Act
            robotService.PlaceRobot(testRobot, "filePath");

            //Assert
            Assert.AreEqual(expectedX, testRobot.Position.X);
            Assert.AreEqual(expectedY, testRobot.Position.Y);
            Assert.AreEqual(expectedDirection, testRobot.Direction);
        }

        [TestMethod]
        public void Test_PlaceRobot_WhenXCoordinateIsLowerThanBounds_ThrowsException()
        {
            //Arrange
            var testRobot = new Robot();
            int expectedX = -2;
            int expectedY = 2;
            var inputServiceRobot = new Robot();
            inputServiceRobot.Position.X = expectedX;
            inputServiceRobot.Position.Y = expectedY;
            inputServiceMock.Setup(x => x.GetRobotFromFile(It.IsAny<string>())).Returns(inputServiceRobot);

            //Act & Assert
            Assert.ThrowsException<Exception>(() => robotService.PlaceRobot(testRobot, "filePath"));
        }

        [TestMethod]
        public void Test_PlaceRobot_WhenXCoordinateIsHigherThanBounds_ThrowsException()
        {
            //Arrange
            var testRobot = new Robot();
            int expectedX = 7;
            int expectedY = 2;
            var inputServiceRobot = new Robot();
            inputServiceRobot.Position.X = expectedX;
            inputServiceRobot.Position.Y = expectedY;
            inputServiceMock.Setup(x => x.GetRobotFromFile(It.IsAny<string>())).Returns(inputServiceRobot);

            //Act & Assert
            Assert.ThrowsException<Exception>(() => robotService.PlaceRobot(testRobot, "filePath"));
        }

        [TestMethod]
        public void Test_PlaceRobot_WhenYCoordinateIsLowerThanBounds_ThrowsException()
        {
            //Arrange
            var testRobot = new Robot();
            int expectedX = 2;
            int expectedY = -2;
            var inputServiceRobot = new Robot();
            inputServiceRobot.Position.X = expectedX;
            inputServiceRobot.Position.Y = expectedY;
            inputServiceMock.Setup(x => x.GetRobotFromFile(It.IsAny<string>())).Returns(inputServiceRobot);

            //Act & Assert
            Assert.ThrowsException<Exception>(() => robotService.PlaceRobot(testRobot, "filePath"));
        }

        [TestMethod]
        public void Test_PlaceRobot_WhenYCoordinateIsHigherThanBounds_ThrowsException()
        {
            //Arrange
            var testRobot = new Robot();
            int expectedX = 2;
            int expectedY = 7;
            var inputServiceRobot = new Robot();
            inputServiceRobot.Position.X = expectedX;
            inputServiceRobot.Position.Y = expectedY;
            inputServiceMock.Setup(x => x.GetRobotFromFile(It.IsAny<string>())).Returns(inputServiceRobot);

            //Act & Assert
            Assert.ThrowsException<Exception>(() => robotService.PlaceRobot(testRobot, "filePath"));
        }

        [TestMethod]
        public void Test_MoveRobot_WhenDirectionIsNorthAndCoordinateIsWithinBounds_OK()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Position.Y = 1;
            testRobot.Direction = DirectionType.North;

            int expectedY = 2;

            //Act
            robotService.MoveRobot(testRobot);

            //Assert
            Assert.AreEqual(expectedY, testRobot.Position.Y);
        }

        [TestMethod]
        public void Test_MoveRobot_WhenDirectionIsNorthAndCoordinateIsOutsideBounds_ThrowsException()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Position.Y = 4;
            testRobot.Direction = DirectionType.North;
            //Assert
            Assert.ThrowsException<Exception>(() => robotService.MoveRobot(testRobot));
        }

        [TestMethod]
        public void Test_MoveRobot_WhenDirectionIsEastAndCoordinateIsWithinBounds_OK()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Position.X = 1;
            testRobot.Direction = DirectionType.East;

            int expectedX = 2;

            //Act
            robotService.MoveRobot(testRobot);

            //Assert
            Assert.AreEqual(expectedX, testRobot.Position.X);
        }

        [TestMethod]
        public void Test_MoveRobot_WhenDirectionIsEastAndCoordinateIsOutsideBounds_ThrowsException()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Position.X = 4;
            testRobot.Direction = DirectionType.East;
            //Assert
            Assert.ThrowsException<Exception>(() => robotService.MoveRobot(testRobot));
        }

        [TestMethod]
        public void Test_MoveRobot_WhenDirectionIsSouthAndCoordinateIsWithinBounds_OK()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Position.Y = 1;
            testRobot.Direction = DirectionType.South;

            int expectedY = 0;

            //Act
            robotService.MoveRobot(testRobot);

            //Assert
            Assert.AreEqual(expectedY, testRobot.Position.Y);
        }

        [TestMethod]
        public void Test_MoveRobot_WhenDirectionIsSouthAndCoordinateIsOutsideBounds_ThrowsException()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Position.Y = 0;
            testRobot.Direction = DirectionType.South;
            //Assert
            Assert.ThrowsException<Exception>(() => robotService.MoveRobot(testRobot));
        }

        [TestMethod]
        public void Test_MoveRobot_WhenDirectionIsWestAndCoordinateIsWithinBounds_OK()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Position.X = 1;
            testRobot.Direction = DirectionType.West;

            int expectedX = 0;

            //Act
            robotService.MoveRobot(testRobot);

            //Assert
            Assert.AreEqual(expectedX, testRobot.Position.X);
        }

        [TestMethod]
        public void Test_MoveRobot_WhenDirectionIsWestAndCoordinateIsOutsideBounds_ThrowsException()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Position.X = 0;
            testRobot.Direction = DirectionType.West;
            //Assert
            Assert.ThrowsException<Exception>(() => robotService.MoveRobot(testRobot));
        }

        [TestMethod]
        public void Test_TurnLeft_WhenDirectionIsBetweenLimits_OK()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Direction = DirectionType.South;
            var expectedDirection = DirectionType.East;

            //Act
            robotService.TurnLeft(testRobot);

            //Assert
            Assert.AreEqual(expectedDirection, testRobot.Direction);
        }

        [TestMethod]
        public void Test_TurnLeft_WhenDirectionIsAtLimit_OK()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Direction = DirectionType.North;
            var expectedDirection = DirectionType.West;

            //Act
            robotService.TurnLeft(testRobot);

            //Assert
            Assert.AreEqual(expectedDirection, testRobot.Direction);
        }

        [TestMethod]
        public void Test_TurnRight_WhenDirectionIsBetweenLimits_OK()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Direction = DirectionType.North;
            var expectedDirection = DirectionType.East;

            //Act
            robotService.TurnRight(testRobot);

            //Assert
            Assert.AreEqual(expectedDirection, testRobot.Direction);
        }

        [TestMethod]
        public void Test_TurnRight_WhenDirectionIsAtLimit_OK()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Direction = DirectionType.West;
            var expectedDirection = DirectionType.North;

            //Act
            robotService.TurnRight(testRobot);

            //Assert
            Assert.AreEqual(expectedDirection, testRobot.Direction);
        }

        [TestMethod]
        public void Test_GetStatusReport_OK()
        {
            //Arrange
            var testRobot = new Robot();
            testRobot.Position.X = 0;
            testRobot.Position.Y = 0;
            testRobot.Direction = DirectionType.North;
            string expectedStatusReport = "0,0,NORTH";

            //Act
            var actualStatusReport = robotService.GetStatusReport(testRobot);

            //Assert
            Assert.AreEqual(expectedStatusReport, actualStatusReport);
        }
    }
}
