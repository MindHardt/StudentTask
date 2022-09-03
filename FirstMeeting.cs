using System.Text;

namespace StudentTask
{
    /// <summary>
    /// Task: take a string, and return this string but with only even digits.
    /// </summary>
    public static class FirstMeeting
    {
        /// <summary>
        /// This solution uses <see langword="for"/> loop with step of 2 and <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Loop(string input)
        {
            StringBuilder sb = new(input.Length / 2);

            for (int i = 0; i < input.Length; i += 2)
            {
                sb.Append(input[i]);
            }

            return sb.ToString();
        } 

        /// <summary>
        /// This solution uses <see cref="System.Linq"/>. Is is shorter but less optimized.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Linq(string input)
        {
            int i = 0;
            return new string(input.Where(c => (i++ % 2) == 0).ToArray());
        }
    }
}
