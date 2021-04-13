using System;

namespace Elevator
{
    enum Directions
    {
        down = -1,
        up = 1
    }

    class lift
    {
        Random random = new Random();
        private const int FLOORS = 10;
        private const int MAX_OCCUPANCY = 8;
        private int Direction = 1;
        private static bool[] Calls = new bool[FLOORS];
        private bool[] Destinations = new bool[FLOORS];
        private int[] Destinationints = new int[FLOORS];
        private int Location = 0;
        private int occupancy = 0;
        private int seek_counter = 1;

        public lift(int location, Directions direction, int[] floors)
        {
            Location = location;
            Direction = (int)direction;
            Console.WriteLine("Starting on floor {0}", location);
            Console.WriteLine("Going {0}", direction.ToString());
            Console.WriteLine("locations : {0}", String.Join(",", floors));
            foreach (int floor in floors)
            {
                Calls[floor] = true;
            }
        }
        public void Seek()
        {
            for (int floor = Location; floor < FLOORS && floor >= 0; floor += Direction)
            {

                if ((Calls[floor] && occupancy < MAX_OCCUPANCY) || Destinations[floor])
                {
                    seek_counter = 1;
                    Move(floor);
                }
            }
            CheckOtherDirection();
        }

        private void CheckOtherDirection()
        {
            Direction *= -1;
            Directions dir = Direction < 0 ? Directions.down : Directions.up;
            Console.WriteLine("Going {0}", dir.ToString());
            seek_counter++;
            if (seek_counter < 3)
            {
                Seek();
                return;
            }
            else
            {
                Console.WriteLine("Journeys complete");
                int waitFloor = FLOORS / 2;
                Direction = (waitFloor - Location) >= 0 ? 1 : -1;
                Move(waitFloor);
                seek_counter = 1;
                return;
            }
        }

        private void Move(int TargetFloor)
        {
            Console.WriteLine("moving to floor {0}", TargetFloor);
            for (int floor = Location; floor < FLOORS && floor >= 0; floor += Direction)
            {
                Location = floor;
                Console.WriteLine("on floor {0}", floor);
                if (floor == TargetFloor)
                {
                    Console.WriteLine("Ding!");
                    Handle_calls(floor);
                    Handle_destinations(floor);
                    Console.WriteLine("There are {0} people on the elevator", occupancy);
                    return;
                }
            }
        }

        private void Handle_destinations(int floor)
        {
            if (Destinations[floor])
            {
                Destinations[floor] = false;
                occupancy -= Destinationints[floor];
                Destinationints[floor] = 0;
            }
            return;
        }

        private void Handle_calls(int floor)
        {
            if (Calls[floor])
            {
                int getting_on = random.Next(1, 3);
                occupancy += getting_on;
                for (int i = 0; i < getting_on; i++)
                {
                    int dest = random.Next(0, FLOORS);
                    Destinations[dest] = true;
                    Destinationints[dest] += 1;
                }
                Calls[floor] = false;
                Console.WriteLine("Destinations: {0}", String.Join(",", Destinationints));
            }
            return;
        }
    }
}
