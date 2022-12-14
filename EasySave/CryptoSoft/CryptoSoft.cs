using System.Text;


namespace CryptoSoft
{
    static class CryptoSoft
    {
        static string key = "C3sIc3Si";

        static void Main(string[] args)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(key);

            byte[] file = File.ReadAllBytes(args[0]);

            int i = 0;
            int j = 0;

            foreach (byte b in file)
            {
                if (i > key.Length - 1)
                {
                    i = 0;
                }
                file[j] = (byte)(b ^ bytes[i]);

                i++;
                j++;
            }
            File.WriteAllBytes(args[1], file);
        }
    }
}

