namespace tesztpizza.Tests
{
    public static class Util
    {
        public static string StringToHex(string str)
        {
            string output = "";
            for (int i = 0; i < (str.Length - 1); i++)
            {
                char c = str[i];
                string hexValue = ((int)c).ToString("X2");
                output += hexValue + ":";
            }
            output += ((int)str[str.Length - 1]).ToString("X2");
            return output;
        }
    }
}
