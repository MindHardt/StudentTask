using System.Text;

namespace StudentTask
{
    public static class FirstMeeting
    {
        public static string Loop(string input)
        {
            StringBuilder sb = new(input.Length / 2);

            for (int i = 0; i < input.Length; i += 2)
            {
                sb.Append(input[i]);
            }

            return sb.ToString();
        }

        public static string Linq(string input)
        {
            int i = 0;
            return new string(input.Where(c => (i++ % 2) == 0).ToArray());
        }
    }
}
