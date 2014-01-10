using System;
using System.Collections.Generic;
using System.Text;

namespace Say.Ivona
{
    internal class Params : Dictionary<string, string>
    {
        public override string ToString()
        {
            var buffer = new StringBuilder(Count*20);
            foreach (var param in this)
                buffer.Append(param.Key + "=" + param.Value + "&");
            if (buffer.Length > 1)
                return buffer.ToString().Remove(buffer.Length - 1);
            return String.Empty;
        }

        internal static Params Merge(Params p, string token, string md5)
        {
            if (p == null)
                p = new Params();
            p.Add("token", token);
            p.Add("md5", md5);
            return p;
        }
    }
}