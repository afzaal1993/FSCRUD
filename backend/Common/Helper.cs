using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Helper
    {
        public static string GetDateAndTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        public static string GetFileExtension(byte[] fileData)
        {
            Dictionary<byte[], string> magicNumbers = new Dictionary<byte[], string>
    {
        { new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }, ".jpg" },
        { new byte[] { 0x89, 0x50, 0x4E, 0x47 }, ".png" },
        { new byte[] { 0x47, 0x49, 0x46, 0x38 }, ".gif" },
        // Add more file types and their magic numbers as needed
    };

            foreach (var magicNumber in magicNumbers)
            {
                if (StartsWith(fileData, magicNumber.Key))
                {
                    return magicNumber.Value;
                }
            }

            return string.Empty; // No matching file extension found
        }

        private static bool StartsWith(byte[] source, byte[] prefix)
        {
            if (source.Length < prefix.Length)
            {
                return false;
            }

            for (int i = 0; i < prefix.Length; i++)
            {
                if (source[i] != prefix[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
