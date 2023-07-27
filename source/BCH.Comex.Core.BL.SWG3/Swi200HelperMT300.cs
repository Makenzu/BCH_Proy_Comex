﻿using System;

namespace BCH.Comex.Core.BL.SWG3
{
    public static class Swi200HelperMT300
    {
        public static int ConvierteAEntero(ref string monto, ref int valor)
        {
            int j;
            int i;
            string monto_aux = new string(new char[15]);

            i = 0;
            for (j = 0; j < 15; j++)
            {
                if (char.IsDigit(monto[j]))
                {
                    monto_aux = StringFunctions.ChangeCharacter(monto_aux, i, monto[j]);
                    i++;
                }
                else
                {
                    if (monto[j] != '\0')
                    {
                        return (1);
                    }
                    else
                    {
                        j = 16;
                    }
                }
            }
            monto_aux = StringFunctions.ChangeCharacter(monto_aux, i, '\0');
            if (i > 0)
            {
                valor = Convert.ToInt32(monto_aux);
                return (0);
            }
            else
            {
                return (1);
            }
        }

        //----------------------------------------------------------------------------------------
        //	Copyright © 2006 - 2015 Tangible Software Solutions Inc.
        //	This class can be used by anyone provided that the copyright notice remains intact.
        //
        //	This class provides the ability to simulate various classic C string functions
        //	which don't have exact equivalents in the .NET Framework.
        //----------------------------------------------------------------------------------------
        public static class StringFunctions
        {
            //------------------------------------------------------------------------------------
            //	This method simulates the classic C string function 'isxdigit' (and 'iswxdigit').
            //------------------------------------------------------------------------------------
            internal static bool IsXDigit(char character)
            {
                if (char.IsDigit(character))
                    return true;
                else if ("ABCDEFabcdef".IndexOf(character) > -1)
                    return true;
                else
                    return false;
            }

            //------------------------------------------------------------------------------------
            //	This method simulates the classic C string function 'strchr' (and 'wcschr').
            //------------------------------------------------------------------------------------
            internal static string StrChr(string stringToSearch, char charToFind)
            {
                int index = stringToSearch.IndexOf(charToFind);
                if (index > -1)
                    return stringToSearch.Substring(index);
                else
                    return null;
            }

            //------------------------------------------------------------------------------------
            //	This method simulates the classic C string function 'strrchr' (and 'wcsrchr').
            //------------------------------------------------------------------------------------
            internal static string StrRChr(string stringToSearch, char charToFind)
            {
                int index = stringToSearch.LastIndexOf(charToFind);
                if (index > -1)
                    return stringToSearch.Substring(index);
                else
                    return null;
            }

            //------------------------------------------------------------------------------------
            //	This method simulates the classic C string function 'strstr' (and 'wcsstr').
            //------------------------------------------------------------------------------------
            internal static string StrStr(string stringToSearch, string stringToFind)
            {
                int index = stringToSearch.IndexOf(stringToFind);
                if (index > -1)
                    return stringToSearch.Substring(index);
                else
                    return null;
            }

            //------------------------------------------------------------------------------------
            //	This method simulates the classic C string function 'strtok' (and 'wcstok').
            //	Note that the .NET string 'Split' method cannot be used to simulate 'strtok' since
            //	it doesn't allow changing the delimiters between each token retrieval.
            //------------------------------------------------------------------------------------
            private static string activeString;
            private static int activePosition;
            internal static string StrTok(string stringToTokenize, string delimiters)
            {
                if (stringToTokenize != null)
                {
                    activeString = stringToTokenize;
                    activePosition = -1;
                }

                //the stringToTokenize was never set:
                if (activeString == null)
                    return null;

                //all tokens have already been extracted:
                if (activePosition == activeString.Length)
                    return null;

                //bypass delimiters:
                activePosition++;
                while (activePosition < activeString.Length && delimiters.IndexOf(activeString[activePosition]) > -1)
                {
                    activePosition++;
                }

                //only delimiters were left, so return null:
                if (activePosition == activeString.Length)
                    return null;

                //get starting position of string to return:
                int startingPosition = activePosition;

                //read until next delimiter:
                do
                {
                    activePosition++;
                } while (activePosition < activeString.Length && delimiters.IndexOf(activeString[activePosition]) == -1);

                return activeString.Substring(startingPosition, activePosition - startingPosition);
            }

            //------------------------------------------------------------------------------------
            //	This method allows replacing a single character in a string, to help convert
            //	C++ code where a single character in a character array is replaced.
            //------------------------------------------------------------------------------------
            internal static string ChangeCharacter(string sourceString, int charIndex, char changeChar)
            {
                return (charIndex > 0 ? sourceString.Substring(0, charIndex) : "")
                    + changeChar.ToString() + (charIndex < sourceString.Length - 1 ? sourceString.Substring(charIndex + 1) : "");
            }
        }
    }
}

