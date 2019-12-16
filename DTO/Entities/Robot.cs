using DTO.Enums;

namespace DTO.Entities
{
    public class Robot
    {
        public Position Position { get; set; }
        public DirectionType Direction { get; set; }

        public Robot()
        {
            Position = new Position();
        }
    }
}
