using HtmlAgilityPack;
using System;

namespace TrxToHtmlConverter
{
    public abstract class TableCreator
    {
        protected static HtmlDocument CreateTable()
        {
            return new HtmlDocument();
        }
        protected static Func<Test, bool> PredicateCreator<T>(Func<Test, T> selector, T expected) where T : IEquatable<T>
        {
            return t => selector(t).Equals(expected);
        }

        protected static string CreateColoredResult(string result)
        {
            string color = "";
            switch (result)
            {
                case "Passed": color = "green"; break;
                case "Failed": color = "SaddleBrown"; break;
                case "Inconclusive": color = "BlueViolet"; break;
                case "Warning": color = "DarkGoldenrod"; break;
            }

            return $"<font color={color} size=4><strong>{result}</strong></font><br>";
        }

    }
}
