using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Cryptex
{
    class StraddleCheckerboardEncryptor
    {
        // The 'grid' used.
        public char[,] Checkerboard = new char[3, 10];

        // Used as reference during encryption, and decryption.
        public int[] Blanks = new int[2];

        /// <summary>
        /// Populates the Checkboard using a permutation of the alphabet
        /// that includes 2 blank spaces.
        /// </summary>
        public void PopulateCheckerBoard(string Alphabet, int Spaces1, int Spaces2)
        {
            if (Spaces1 > 9 || Spaces2 > 9 || Spaces1 == Spaces2 || Spaces1 < 0 || Spaces2 < 0)
            {
                throw new ArgumentException("Spacing cannot exceed 10, nor can they be the same.");
            }

            // Stores the index in which the 'spaces' will be.
            Blanks[0] = Spaces1;
            Blanks[1] = Spaces2;

            if (MoreThanOnceCheck(Alphabet))
            {
                throw new ArgumentException("Alphabet cannot contain more than one of each character.");
            }
            
            // Checks if the length of the parameter is appropriate.
            if (Alphabet.Length > 28)
            {
                throw new ArgumentException("Alphabet cannot exceed the standard 26 character + 2 blank spaces");
            }

            // Gets IEnumerator for continued iteration as it populates the Checkerboard.
            IEnumerator<char> alphabetChar = Alphabet.GetEnumerator();

            // Populates the Checkerboard.
            for (int i = 0; i < Checkerboard.GetLength(0); i++)
            {
                for (int j = 0; j < Checkerboard.GetLength(1); j++)
                {
                    if (i == 0 && (j == Blanks[0] || j == Blanks[1]))
                    {
                        Checkerboard[i, j] = ' ';
                        continue;
                    }
                    alphabetChar.MoveNext();

                    try
                    {
                        Checkerboard[i, j] = alphabetChar.Current;
                    }
                    catch (InvalidOperationException)
                    {
                        // Will break once the enumerator is finished, as 
                        // the Checkboard will not have every index assigned
                        // to a value.

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Encrypts the input using the Straddle Checkerboard Cipher and the Checkerboard[,]
        /// </summary>
        public string Encrypt(string MessageToEncrypt)
        {
            // Removes special characters/ spaces from the message.
            StringBuilder stringCleanser = new StringBuilder();
            for (int i = 0; i < MessageToEncrypt.Length; i++)
            {
                if (MessageToEncrypt[i] >= 'A' && MessageToEncrypt[i] <= 'z')
                {
                    stringCleanser.Append(MessageToEncrypt[i]);
                }
            }

            MessageToEncrypt = stringCleanser.ToString();
            StringBuilder encryptedMessage = new StringBuilder();

            for (int i = 0; i < MessageToEncrypt.Length; i++)
            {
                var coords = GetCoords(MessageToEncrypt[i]);

                if (coords.Item1 == 1 || coords.Item1 == 2)
                {
                    encryptedMessage.Append(Blanks[coords.Item1 - 1].ToString() + coords.Item2.ToString());
                }
                else
                {
                    encryptedMessage.Append(coords.Item2.ToString());
                }
            }

            return encryptedMessage.ToString();
        }

        /// <summary>
        /// Decrypts the message input using the Straddle Checkerboard Cipher,
        /// assuming the 'Checkerboard[,]' has been populated.
        /// </summary>
        public string Decrypt(string MessageToDecrypt)
        {
            StringBuilder decryptedMessage = new StringBuilder();

            for (int i = 0; i < MessageToDecrypt.Length; i++)
            {
                /* Checks the literal value of the character using ASCII as a cast
                to a int from a char returns the ASCII value, instead of the value
                I need. */

                if (((int)MessageToDecrypt[i] - '0') == Blanks[0])
                {
                    decryptedMessage.Append(Checkerboard[1, (((int)MessageToDecrypt[i + 1] - '0'))]);
                    i++;
                } 
                else if (((int)MessageToDecrypt[i] - '0') == Blanks[1])
                {
                    decryptedMessage.Append(Checkerboard[2, (((int)MessageToDecrypt[i + 1] - '0'))]);
                    i++;
                } 
                else
                {
                    decryptedMessage.Append(Checkerboard[0, (((int)MessageToDecrypt[i] - '0'))]);
                }
            }

            return decryptedMessage.ToString();
        }

        /// <summary>
        /// Gets the 'coordinates' on a letter within the Checkerboard.
        /// </summary>
        private Tuple<int, int> GetCoords(char MessageLetter)
        {
            for (int i = 0; i < Checkerboard.GetLength(0); i++)
            {
                for (int j = 0; j < Checkerboard.GetLength(1); j++)
                {
                    if (Checkerboard[i, j] == MessageLetter)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }

            return Tuple.Create(0, 0);
        }

        /// <summary>
        /// Checks if any of the characters that appear within a string
        /// occur more than once.
        /// </summary>
        private bool MoreThanOnceCheck(string AlphabetString)
        {
            for (int i = 0; i < AlphabetString.Length; i++)
            {
                if (Count(AlphabetString, AlphabetString[i]) > 1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the amount of times the specified
        /// character appears within the string.
        /// </summary>
        private int Count(string AlphabetString, char Character)
        {
            int frequency = 0;

            for (int i = 0; i < AlphabetString.Length; i++)
            {
                // checking character in string 
                if (AlphabetString[i] == Character)
                {
                    frequency++;
                }
            }
            return frequency;
        }
    }
}
