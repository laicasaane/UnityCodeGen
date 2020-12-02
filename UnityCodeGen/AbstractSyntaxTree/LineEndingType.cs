namespace UnityCodeGen.AbstractSyntaxTree
{
    public enum LineEndingType
    {
        LF = 0,
        CRLF = 1
    }

    public static class LineEnding
    {
        public const string LF = "\n";

        public const string CRLF = "\r\n";

        public static string Get(this LineEndingType self)
        {
            return self == LineEndingType.CRLF ? CRLF : LF;
        }
    }
}