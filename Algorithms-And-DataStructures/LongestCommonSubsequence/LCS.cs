using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestCommonSubsequence
{
    public class LCS
    {
        
        public static string GetLCS(string X, string Y)
        {
            string str = "";
            int m = X.Length, n = Y.Length;
            int[,] b = new int[m, n], c = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++)
                c[i, 0] = 0;
            for (int j = 0; j <= n; j++)
                c[0, j] = 0;
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (X[i - 1] == Y[j - 1]) c[i, j] = c[i - 1, j - 1] + 1;
                    else if (c[i - 1, j] >= c[i, j - 1]) c[i, j] = c[i - 1, j];
                    else c[i, j] = c[i, j - 1];
                }
            }

            while (m > 0 && n > 0)
            {
                if (X[m - 1] == Y[n - 1])
                {
                    str += X[m - 1];
                    m--;
                    n--;
                }
                else if (c[m - 1, n] > c[m, n - 1]) m--;
                else n--;
            }
            return new string( str.ToArray().Reverse().ToArray());
        }   
    }
}
