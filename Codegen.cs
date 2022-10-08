using System.Text;
using System.Text.RegularExpressions;

namespace StudentTask
{
    public class Codegen
    {
        public static void Basic(string fileName, string saveTo)
        {
            string[] fileContents = File.ReadAllLines(fileName);

            StringBuilder sb = new();

            sb.AppendLine("namespace SillyProgram");
            sb.AppendLine("{");
            sb.Append("\tpublic class ");

            sb.AppendLine(GetClassName(fileContents[0]));

            sb.AppendLine("\t{");

            string[] propsData = fileContents[1].Replace("Поля: ", string.Empty).Split(", ");
            string[] props = GetAllProperties(propsData);

            sb.AppendLine(string.Join('\n', props));

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            File.WriteAllText(saveTo, sb.ToString());
        }
        /// <summary>
        /// Gets class name and inheritance, if any.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string GetClassName(string line)
            => line.Replace("наследует", ":").Remove(0, 7);
        /// <summary>
        /// Gets declaration of one property based on <paramref name="description"/>.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        private static string GetProperty(string description)
        {
            string[] words = description.Split(' ');

            bool isPrivate = 
                words[0] == "скрытый" ||
                words[0] == "cкрытая" ||
                words[0] == "скрытое";

            //Public props start with capital letter, private don't
            StringBuilder propNameBuilder = new(words[words.Length - 1]);
            propNameBuilder[0] = isPrivate ?
                char.ToLower(propNameBuilder[0]) :
                char.ToUpper(propNameBuilder[0]) ;

            string propName = propNameBuilder.ToString();
            string type = words[words.Length - 2] switch
            {
                "число" => "int",
                "строка" => "string",
                "символ" => "char",
                "флаг" => "bool",
                _ => throw new NotSupportedException()
            };
            string publicity = isPrivate ? "private" : "public";
            string accessors = isPrivate ? ";" : " { get; set; }";

            return $"\t\t{publicity} {type} {propName}{accessors}";
        }
        /// <summary>
        /// Parses all the lines to property declatartions and sorts them so that
        /// public props go first.
        /// </summary>
        /// <param name="descriptions"></param>
        /// <returns></returns>
        private static string[] GetAllProperties(string[] descriptions)
        {
            string[] result = new string[descriptions.Length];
            for (int i = 0; i < descriptions.Length; i++)
            {
                string prop = GetProperty(descriptions[i]);

                result[i] = prop;
            }

            Array.Sort(result);
            Array.Reverse(result);
            return result;
        }
    }
}
