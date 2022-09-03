namespace StudentTask
{
    class Program
    {
        public static void Main()
        {
            string input = "Lorem ipsum dolor sit amet";
            Console.WriteLine(FirstMeeting.Loop(input));
            Console.WriteLine(FirstMeeting.Linq(input));
        }
    }
}
