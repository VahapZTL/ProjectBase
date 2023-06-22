using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Extensions
{
    public static class StringNormalizer
    {
        public static string ConvertTurkishToEnglish(this string turkishText)
        {
            //string normalizedText = turkishText.Normalize(NormalizationForm.FormKD);
            //StringBuilder englishTextBuilder = new StringBuilder();

            //foreach (char c in normalizedText)
            //{
            //    if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            //    {
            //        englishTextBuilder.Append(c);
            //    }
            //}

            //return englishTextBuilder.ToString();

            string[] turkishChars = { "ç", "ğ", "ı", "i", "ö", "ş", "ü", "Ç", "Ğ", "İ", "Ö", "Ş", "Ü" };
            string[] englishChars = { "c", "g", "i", "i", "o", "s", "u", "C", "G", "I", "O", "S", "U" };

            StringBuilder englishTextBuilder = new StringBuilder();

            for (int i = 0; i < turkishText.Length; i++)
            {
                char currentChar = turkishText[i];

                // Türkçe karakterin dönüşüm tablosunda olup olmadığını kontrol et
                int index = Array.IndexOf(turkishChars, currentChar.ToString());

                if (index >= 0)
                {
                    // Türkçe karakteri İngilizce karakterle değiştir
                    englishTextBuilder.Append(englishChars[index]);
                }
                else
                {
                    // Türkçe karakter dönüştürülemezse doğrudan ekle
                    englishTextBuilder.Append(currentChar);
                }
            }

            return englishTextBuilder.ToString().Replace(" ", string.Empty);
        }
    }
}
