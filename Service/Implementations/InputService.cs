using DTO.Entities;
using DTO.Enums;
using Service.Interfaces;
using System;
using System.IO;

namespace Service.Implementations
{
    public class InputService : IInputService
    {
        private static string ErrorMessage => "Error: Could not find place.properties File!\nLocation:";

        /// <summary>
        /// Reads values from a Text file and creates a new Robot instance with them.
        /// <para>Text file format example: 0,0,North</para>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Robot GetRobotFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception(ErrorMessage + filePath);
            }
            string fileContent = File.ReadAllText(filePath);
            string[] values = fileContent.Split(',');
            var poisition = new Position() { X = int.Parse(values[0]), Y = int.Parse(values[1]) };
            var direction = (DirectionType)Enum.Parse(typeof(DirectionType), values[2]);
            return new Robot() { Position = poisition, Direction = direction};
        }
    }
}
