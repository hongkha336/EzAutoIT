using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Easy_AutoIT_Helper.Rand
{
    public static class EzRand
    {
        public static Double RandNo(int lenght)
        {
            long myNoNumber = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            String str = myNoNumber.ToString();

            while (lenght > str.Length)
            {
                str = str + str;
            }

            String newNo = String.Empty;
            for(int i = str.Length-1; i > str.Length-1- lenght; i--)
            {
                newNo = newNo + str[i];
            }


            return Convert.ToDouble(newNo);
        }

        public static String RandString(int len)
        {
            String str = String.Empty;
            while (str.Length < len)
            {
                str += GetLetter();
                Thread.Sleep(20);
            }
            return str;
        }

        private static char GetLetter()
        {
            string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
            Random rand = new Random();
            int num = rand.Next(0, chars.Length - 1);
            return chars[num];
        }

        public static String RandFromMeta(String meta)
        {
            List<String> ml = new List<string>();

            String metaBackUp = meta;
            String temp = String.Empty;
            String mark = String.Empty;
            int count = 0;
            String token = String.Empty;
            bool start_cut = false;
            while (meta.Length > 0)
            {
                if (meta[0] == '{')
                {
                    //  temp = "{";
                    temp = String.Empty;
                    start_cut = true;
                    count++;
                    token = "@" + count + "@";



                }
                else
                {
                    if (meta[0] == '}')
                    {
                        temp = temp + "}";
                        ml.Add(temp);
                        start_cut = false;
                    }


                }

                if (start_cut)
                {
                    temp = temp + meta[0];
                }
                else
                {
                    if (meta[0] == '}')
                        mark = mark + token;
                    else
                        mark = mark + meta[0];
                }

                meta = meta.Substring(1, meta.Length - 1);

            }

            int count2 = 0;
            foreach (String str in ml)
            {
                
                count2++;
                String token2 = "@" + count2 + "@";
                mark = Regex.Replace(mark, token2, RandControl(str));

            }

            return mark;

        }

        public static String RandControl(String _RANDKEY)
        {
            try
            {
                _RANDKEY = _RANDKEY.ToUpper();
                _RANDKEY = _RANDKEY.Substring(0, _RANDKEY.Length - 1);


                if (_RANDKEY.StartsWith("{NO9"))
                {
                    Random rnd = new Random();
                    int dice = rnd.Next(1, 10);
                    return dice.ToString();
                }

                if (_RANDKEY.StartsWith("{NO12"))
                {
                    Random rnd = new Random();
                    int dice = rnd.Next(1, 13);
                    return dice.ToString();
                }

                if (_RANDKEY.StartsWith("{NO24"))
                {
                    Random rnd = new Random();
                    int dice = rnd.Next(1, 25);
                    return dice.ToString();
                }

                if (_RANDKEY.StartsWith("{NO60"))
                {
                    Random rnd = new Random();
                    int dice = rnd.Next(1, 61);
                    return dice.ToString();
                }

                int Lengh = -1;
                String temp = String.Empty;
                while (_RANDKEY.Length > 0 && _RANDKEY[_RANDKEY.Length - 1] != ':')
                {
                    temp = _RANDKEY[_RANDKEY.Length - 1] + temp;
                    _RANDKEY = _RANDKEY.Substring(0, _RANDKEY.Length - 1);
                }
                _RANDKEY = _RANDKEY.Trim();

                if (_RANDKEY.Length > 0 && _RANDKEY[_RANDKEY.Length - 1] == ':')
                {
                    _RANDKEY = _RANDKEY.Substring(0, _RANDKEY.Length - 1);
                }

                Lengh = Convert.ToInt32(temp);

                if (_RANDKEY.StartsWith("{STR"))
                {
                    return RandString(Lengh);
                }

                if (_RANDKEY.StartsWith("{NUM"))
                {
                    return RandNo(Lengh).ToString();
                }

                if (_RANDKEY.StartsWith("{NUMLESS"))
                {
                    Random rnd = new Random();
                    int dice = rnd.Next(0, Lengh);
                    return dice.ToString();
                }

                if (_RANDKEY.StartsWith("{NL"))
                {
                    Random rnd = new Random();
                    int dice = rnd.Next(0, Lengh);
                    return dice.ToString();
                }
            }
            catch
            {
                return _RANDKEY;
            }

            return _RANDKEY;
        }
    }
}
