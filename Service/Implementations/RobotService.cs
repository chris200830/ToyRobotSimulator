using DTO.Entities;
using DTO.Enums;
using Service.Interfaces;
using System;
using System.Text;

namespace Service.Implementations
{
    public class RobotService : IRobotService
    {
        private IInputService inputService;
        public RobotService(IInputService inputService)
        {
            this.inputService = inputService;
        }

        private static string ErrorMessage => "Error: Robot outside established Grid values!";

        /// <summary>
        /// Places the Robot at the position and with the direction defined in a file.
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="placeFilePath"></param>
        public void PlaceRobot(Robot robot, string placeFilePath)
        {
            var newRobot = inputService.GetRobotFromFile(placeFilePath);

            if ((newRobot.Position.X < 0 || newRobot.Position.X > 4) || (newRobot.Position.Y < 0 || newRobot.Position.Y > 4))
            {
                throw new Exception(ErrorMessage);
            }
            robot.Position = newRobot.Position;
            robot.Direction = newRobot.Direction;
        }

        /// <summary>
        /// Moves the Robot by 1 in a given direction
        /// </summary>
        /// <param name="robot"></param>
        public void MoveRobot(Robot robot)
        {
            switch (robot.Direction)
            {
                case DirectionType.North:
                    robot.Position.Y = GetNewForwardPosition(robot.Position.Y, 4, true);
                    break;
                case DirectionType.East:
                    robot.Position.X = GetNewForwardPosition(robot.Position.X, 4, true);
                    break;
                case DirectionType.South:
                    robot.Position.Y = GetNewForwardPosition(robot.Position.Y, 0, false);
                    break;
                case DirectionType.West:
                    robot.Position.X = GetNewForwardPosition(robot.Position.X, 0, false);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Rotates the Robot counter clockwise.
        /// </summary>
        /// <param name="robot"></param>
        public void TurnLeft(Robot robot)
        {
            robot.Direction = GetNewDirectionClockwiseOrCounterClockwise(robot.Direction, false);
        }

        /// <summary>
        /// Rotoates the Robot clockwise.
        /// </summary>
        /// <param name="robot"></param>
        public void TurnRight(Robot robot)
        {
            robot.Direction = GetNewDirectionClockwiseOrCounterClockwise(robot.Direction, true);
        }

        /// <summary>
        /// Creates a string with information regarding the Robot's current status.
        /// <para>Example: (2,3,NORTH)</para>
        /// </summary>
        /// <param name="robot"></param>
        /// <returns></returns>
        public string GetStatusReport(Robot robot)
        {
            var statusReport = new StringBuilder();
            statusReport.Append(robot.Position.X + ",");
            statusReport.Append(robot.Position.Y + ",");
            statusReport.Append(robot.Direction.ToString().ToUpper());
            return statusReport.ToString();
        }

        /// <summary>
        /// Verifies if the Robot is within bounds to move in a direction (X, Y) by value of 1.
        /// <para>The sign parameter checks if the Robot moves forwards or backwards.</para>
        /// <para>True if forwards and False if backwards.</para>
        /// </summary>
        /// <param name="position"></param>
        /// <param name="bounds"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        private int GetNewForwardPosition(int position, int bounds, bool sign)
        {
            if (position == bounds)
            {
                throw new Exception(ErrorMessage);
            }
            return sign ? ++position : --position;
        }

        /// <summary>
        /// Changes the direction of the Robot by value of 1.
        /// <para>The sign parameter checks if the direction is clockwise or counter clockwise.</para>
        /// <para>True if clockwise and False if counter clockwise.</para>
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        private DirectionType GetNewDirectionClockwiseOrCounterClockwise(DirectionType direction, bool sign)
        {
            int nextDirection;
            int currentDirection = (int)direction;

            if (currentDirection == 3 && sign)
            {
                nextDirection = 0;
            }
            else if (currentDirection == 0 && !sign)
            {
                nextDirection = 3;
            }
            else
            {
                nextDirection = sign ? ++currentDirection : --currentDirection;
            }
            return (DirectionType)nextDirection;
        }
    }
}
