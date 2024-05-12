using System;
using System.Text;

namespace SimpleTGBot
{
    public class Functions
    {
        public static string TranslateMessage(string message, string sep, string left_frame, string right_frame)
        {
            var sb = new StringBuilder();
            foreach (var x in message)
            {
                if ("йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ ".Contains(x))
                {
                    if (x == 'й' || x == 'Й')
                    {
                        sb.Append("ù");
                    }
                    if (x == 'ц' || x == 'Ц')
                    {
                        sb.Append("U,");
                    }
                    if (x == 'у')
                    {
                        sb.Append("y");
                    }
                    if (x == 'У')
                    {
                        sb.Append("Y");
                    }
                    if (x == 'к')
                    {
                        sb.Append("k");
                    }
                    if (x == 'К')
                    {
                        sb.Append("K");
                    }
                    if (x == 'е')
                    {
                        sb.Append("e");
                    }
                    if (x == 'Е')
                    {
                        sb.Append("E");
                    }
                    if (x == 'н' || x == 'Н')
                    {
                        sb.Append("H");
                    }
                    if (x == 'г' || x == 'Г')
                    {
                        sb.Append("r");
                    }
                    if (x == 'ш' || x == 'Ш')
                    {
                        sb.Append("LLl");
                    }
                    if (x == 'щ' || x == 'Щ')
                    {
                        sb.Append("LLl,");
                    }
                    if (x == 'з' || x == 'З')
                    {
                        sb.Append("3");
                    }
                    if (x == 'х')
                    {
                        sb.Append("x");
                    }
                    if (x == 'Х')
                    {
                        sb.Append("X");
                    }
                    if (x == 'ъ' || x == 'Ъ')
                    {
                        sb.Append("b");
                    }
                    if (x == 'ф' || x == 'Ф')
                    {
                        sb.Append("qp");
                    }
                    if (x == 'ы' || x == 'Ы')
                    {
                        sb.Append("bl");
                    }
                    if (x == 'в' || x == 'В')
                    {
                        sb.Append("B");
                    }
                    if (x == 'а')
                    {
                        sb.Append("a");
                    }
                    if (x == 'А')
                    {
                        sb.Append("A");
                    }
                    if (x == 'п')
                    {
                        sb.Append("n");
                    }
                    if (x == 'П')
                    {
                        sb.Append("/7");
                    }
                    if (x == 'р')
                    {
                        sb.Append("p");
                    }
                    if (x == 'Р')
                    {
                        sb.Append("P");
                    }
                    if (x == 'о')
                    {
                        sb.Append("o");
                    }
                    if (x == 'О')
                    {
                        sb.Append("О");
                    }
                    if (x == 'л' || x == 'Л')
                    {
                        sb.Append("λ");
                    }
                    if (x == 'д' || x == 'Д')
                    {
                        sb.Append("D");
                    }
                    if (x == 'ж' || x == 'Ж')
                    {
                        sb.Append("}l{");
                    }
                    if (x == 'э')
                    {
                        sb.Append("e");
                    }
                    if (x == 'Э')
                    {
                        sb.Append("€");
                    }
                    if (x == 'я' || x == 'Я')
                    {
                        sb.Append("9l");
                    }

                    if (x == 'ч' || x == 'Ч')
                    {
                        sb.Append("4");
                    }
                    if (x == 'с')
                    {
                        sb.Append("c");
                    }
                    if (x == 'С')
                    {
                        sb.Append("c");
                    }
                    if (x == 'м')
                    {
                        sb.Append("m");
                    }
                    if (x == 'М')
                    {
                        sb.Append("M");
                    }
                    if (x == 'и' || x == 'И')
                    {
                        sb.Append("u");
                    }
                    if (x == 'т' || x == 'Т')
                    {
                        sb.Append("T");
                    }
                    if (x == 'ь' || x == 'Ь')
                    {
                        sb.Append("b");
                    }
                    if (x == 'б' || x == 'Б')
                    {
                        sb.Append("6");
                    }
                    if (x == 'ю' || x == 'Ю')
                    {
                        sb.Append("l0");
                    }
                    if (x == 'ё')
                    {
                        sb.Append("ē");
                    }
                    if (x == 'Ё')
                    {
                        sb.Append('Ē');
                    }
                    if (x == ' ')
                    {
                        sb.Append(sep);
                    }
                }
                else
                    sb.Append(x);
            }
            return left_frame + sb.ToString() + right_frame;

        }
    }
}
