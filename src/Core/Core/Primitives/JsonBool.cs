using System;

namespace Pocket.Json
{
    internal static class JsonBool
    {
        public static bool Read(ref StringSpan json)
        {
            var ch = json.Source[json.Start++];
            
            if (ch == '1') return true;
            if (ch == '0') return false;

            throw new ArgumentException($"Specified string \"{json.ToString()}\" must be either \"1\" or \"0\".",
                nameof(json));
        }
        
        public static void Write(bool value, StringBuffer buffer)
        {
            buffer.Write(value ? '1' : '0');
        }
    }
}