using System.Text;

namespace Commons
{
    public class Global
    {
        public const int CODE_PAGE = 936;
        public static readonly Encoding Encoding = Encoding.GetEncoding(CODE_PAGE);
    }
}