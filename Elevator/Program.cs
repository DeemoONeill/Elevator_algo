namespace Elevator
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] floors = new int[] { 0, 1, 5, 6, 7, 9 };

            lift mylift = new lift(3, Directions.down, floors);

            mylift.Seek();

        }
    }
}
