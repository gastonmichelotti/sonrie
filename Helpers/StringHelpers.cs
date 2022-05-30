using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace netCoreNew.Helpers
{
    public static class StringHelpers
    {
        public static string TryTrim(this string texto)
        {
            if(texto.Length <= 50)
            {
                return texto;
            }

            return texto.Substring(0, 50);
        }

        public static bool IsValidEmail(this string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return false;
                }

                var addr = new System.Net.Mail.MailAddress(email);

                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static List<string> ToEmailList(this string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new List<string>();
            }

            var delimiters = new[] { ',', ';' };

            var listado = email.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Distinct();

            var fix = new List<string>();

            foreach (var item in listado)
            {
                if (IsValidEmail(item))
                {
                    fix.Add(item);
                }
            }

            return fix;
        }

        public static string StripHTML(this string input)
        {
            input = Regex.Replace(input, @"</h?\d>|</p>|<br>|</br>|<hr>|</hr>|&nbsp;", Environment.NewLine, RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"<b>|</b>|<strong>|</strong>", "*", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "<.*?>", String.Empty);

            return input;
        }

        public static string ToMoney0(this int valor)
        {
            return valor.ToString("C0");
        }

        public static string ToMoney2(this int valor)
        {
            return valor.ToString("C2");
        }

        public static string ToMoney0(this double valor)
        {
            return valor.ToString("C0");
        }

        public static string ToMoney2(this double valor)
        {
            return valor.ToString("C2");
        }

        public static string ToDateSmall(this DateTime valor)
        {
            return valor.ToString("d"); //10/1/2008
        }

        public static string ToDateMes(this DateTime valor)
        {
            return valor.ToString("dd MMM yy").ToUpper(); //11 OCT 18
        }

        public static string ToDateLong(this DateTime valor)
        {
            return valor.ToString("D"); //miércoles, 01 de octubre de 2008
        }

        public static string ToDateTime(this DateTime valor)
        {
            return valor.ToString("g"); //01/10/2008 17:04
        }

        public static string ToTime(this DateTime valor)
        {
            return valor.ToString("t");
        }

        public static string URLFriendly(string text, int maxLength = 0)
        {
            // Return empty value if text is null
            if (text == null)
            {
                return "";
            }

            var normalizedString = text
                // Make lowercase
                .ToLowerInvariant()
                // Normalize the text
                .Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            var stringLength = normalizedString.Length;
            var prevdash = false;
            var trueLength = 0;
            char c;
            for (int i = 0; i < stringLength; i++)
            {
                c = normalizedString[i];
                switch (CharUnicodeInfo.GetUnicodeCategory(c))
                {
                    // Check if the character is a letter or a digit if the character is a
                    // international character remap it to an ascii valid character
                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (c < 128)
                        {
                            stringBuilder.Append(c);
                        }
                        else
                        {
                            stringBuilder.Append(RemapInternationalCharToAscii(c));
                        }

                        prevdash = false;
                        trueLength = stringBuilder.Length;
                        break;
                    // Check if the character is to be replaced by a hyphen but only if the last character wasn't
                    case UnicodeCategory.SpaceSeparator:
                    case UnicodeCategory.ConnectorPunctuation:
                    case UnicodeCategory.DashPunctuation:
                    case UnicodeCategory.OtherPunctuation:
                    case UnicodeCategory.MathSymbol:
                        if (!prevdash)
                        {
                            stringBuilder.Append('-');
                            prevdash = true;
                            trueLength = stringBuilder.Length;
                        }
                        break;
                }
                // If we are at max length, stop parsing
                if (maxLength > 0 && trueLength >= maxLength)
                {
                    break;
                }
            }
            // Trim excess hyphens
            var result = stringBuilder.ToString().Trim('-');
            // Remove any excess character to meet maxlength criteria
            return maxLength <= 0 || result.Length <= maxLength ? result : result.Substring(0, maxLength);
        }

        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }
    }
}
