using LongestCommonSubsequence;
using Microsoft.VisualStudio.TestTools.UnitTesting;


Random rnd = new Random();


Assert.AreEqual("BCDEF", LCS.GetLCS("ABCDEF", "BCDEF"));
Assert.AreEqual("B", LCS.GetLCS("FEDCBA", "BCDEF"));
Assert.AreNotEqual("CBA", LCS.GetLCS("FEDCBA", "DCBA"));
Assert.AreEqual("DCBA", LCS.GetLCS("FEDCBA", "DCBA"));



//string Z = GetRandomString(1000);

int numberOfMatch = 0;
for(int i =0; i < 100; i++)
{
    string Z = GetRandomString(1000);
    string X = GetRandomStringFromSequenceZ(100,Z);
    string Y = GetRandomStringFromSequenceZ(100,Z);
    if (Z == LCS.GetLCS(X, Y)) numberOfMatch++;
}

Console.WriteLine($"Number of match: {numberOfMatch}");



string GetRandomString(int iter)
{
    string str = "ABCDEFGHIJKLMNOPQRSTUVWYZabcdefghijklmnopqrstuvwyz1234567890";
    string res = "";
    for (int i = 0; i < iter; i++)
    {
        int n = rnd.Next(str.Length);
        res = res + str[n];
    }
    return res;
}

string GetRandomStringFromSequenceZ(int iter, string str)
{
    List<char> chars = str.ToList();
    for (int i = 0; i < iter; i++)
        chars.Insert(rnd.Next(chars.Count), chars[rnd.Next(chars.Count)]);
    return new string(chars.ToArray());
}
