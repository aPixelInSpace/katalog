using System;

namespace FunctionHasNoName
{
    public class NoName
    {
        public static string FunctionHasNoName(string input)
        {
            int k = 0;
            int j = input.Length;
            int m = 1;
            int i = 100;
            string l = "";
            string q = "";
            int h = 4 * j;
            int u = 0;
            int v = 0;
            int s = 0;
            int t = 0;
            bool b = false;
            int g = h / 2;

            if (j <= k)
            {
                return "";
            }

            if (input.Length == m)
            {
                return input.Substring(0, 1);
            }

            for (i = 1; i < h / 2; i++)
            {
                q = "";
                u = Math.DivRem(i, 2, out int rest) - 1;
                v = Math.DivRem(i, 2, out rest);

                if (i % 2 == m)
                {
                    q = (input[v].ToString());
                    v += 1;
                    s = q.Length;
                    t = l.Length + q.Length;

                    if (s > t - s)
                    {
                        l = q;
                    }

                }

                b = false;
                while (u > -1 && v < g / 2 && !b)
                {
                    if (input[u] == input[v])
                    {
                        q = input[u] + q + input[v];
                        u -= 1;
                        v += 1;
                        s = q.Length;
                        t = l.Length + q.Length;

                        if (s > t - s)
                        {
                            l = q;
                        }
                    }
                    else
                    {
                        b = true;
                    }
                }
            }

            return l;
        }
    }
}
