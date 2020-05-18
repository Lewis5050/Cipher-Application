using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptex
{
    class PortaEncryptor
    {
        public char[,] LetterTable = new char[13, 26];

        public PortaEncryptor()
        {
            PopulateLetterTable();
        }

        /// <summary>
        /// Encrypts the message using the Porta Cipher and the 
        /// key word provided.
        /// </summary>
        public string Encrypt(string MessageToEncrypt, string KeyWord)
        {
            MessageToEncrypt = MessageToEncrypt.ToLower();
            KeyWord = KeyWord.ToLower();

            // Removes punctuation.
            MessageToEncrypt = RemovePunctuation(MessageToEncrypt);

            // Resizes the KeyWord.
            KeyWord = Resize(MessageToEncrypt, KeyWord);

            StringBuilder encryptedMessage = new StringBuilder();
            for (int i = 0; i < MessageToEncrypt.Length; i++)
            {
                if (MessageToEncrypt[i] == ' ')
                {
                    encryptedMessage.Append(' ');
                }
                else
                {
                    for (int j = 0; j < LetterTable.GetLength(0); j++)
                    {
                        /* Matches the integer ASCII value of each letter in the key word, until it finds the row
                        in the table where it finds a match for the letter.

                        As there's only 13 rows (due to the fact there's 2 letters per row). Accounting for 
                        the letters 'index' in the alphabet (a=1 ect.) and the 'off by one' problem, the 
                        below code checks the row based on ASCII value. */

                        if ((((int)KeyWord[i] - 'a') + 1) == (j + j + 1) || (((int)KeyWord[i] - 'a') + 1) == (j + j + 2))
                        {
                            for (int k = 0; k < LetterTable.GetLength(1); k++)
                            {
                                if (LetterTable[j, k] == MessageToEncrypt[i])
                                {
                                    encryptedMessage.Append((char)(97 + k));
                                }
                            }
                        }
                    }
                }
            }
            return encryptedMessage.ToString();
        }

        /// <summary>
        /// Decrypts the message using the key. Assuming it has
        /// been encrypted with the Porta Cipher.
        /// </summary>
        public string Decrypt(string EncryptedMessage, string KeyWord)
        {
            KeyWord = KeyWord.ToLower();
            KeyWord = Resize(EncryptedMessage, KeyWord);

            StringBuilder decryptedMessage = new StringBuilder();
            for (int i = 0; i < EncryptedMessage.Length; i++)
            {
                if (EncryptedMessage[i] == ' ')
                {
                    decryptedMessage.Append(' ');
                }
                else
                {
                    for (int j = 0; j < LetterTable.GetLength(1); j++)
                    {
                        // Checks the ASCII value to find the correct column.
                        if ((int)(EncryptedMessage[i] - 'a') == j)
                        {
                            for (int k = 0; k < LetterTable.GetLength(0); k++)
                            {
                                // Checks the row for the keyword, to ensure the correct
                                // letter is appended to the decryptionMessage

                                if ((char)(97 + (k + k)) == KeyWord[i])
                                {
                                   decryptedMessage.Append(LetterTable[k,j]);
                                } 
                                else if ((char)(97 + (k + k + 1)) == KeyWord[i])
                                {
                                    decryptedMessage.Append(LetterTable[k,j]);
                                }
                            }
                        }
                    }
                }
            }
            return decryptedMessage.ToString();
        }

        /// <summary>
        /// Resizes the key word to make it the same
        /// length as the message by repeating itself.
        /// </summary>
        private string Resize(string MessageToEncrypt, string KeyWord)
        {
            if (KeyWord.Length > MessageToEncrypt.Length)
            {
                throw new ArgumentException("Key cannot be greater than the message.");
            }

            for (int i = 0; i < KeyWord.Length; i++)
            {
                if (KeyWord[i] == ' ' || Char.IsPunctuation(KeyWord[i]))
                {
                    throw new ArgumentException("Key cannot contain spaces or punctuation");
                }
            }
           
            /* Increases the length of the KeyWord by repeating itself
             * in order to match the length of the Message. This also 
             * accounts for spaces with the Message. */

            if (KeyWord.Length < MessageToEncrypt.Length)
            {
                // Matches the length of the Message
                StringBuilder newKey = new StringBuilder(KeyWord);
                for (int i = 0; i < (MessageToEncrypt.Length - KeyWord.Length); i++)
                {
                    newKey.Append(newKey[i]);
                }

                /* If the Message contains spaces, we'll add them into the KeyWord here
                 * and remove any 'overflowing' characters in the KeyWord as a result of
                 * this change, in order to match the length of the Message. */

                if (MessageToEncrypt.Contains(' '))
                {
                    for (int i = 0; i < MessageToEncrypt.Length; i++)
                    {
                        if (MessageToEncrypt[i] == ' ')
                        {
                            newKey.Insert(i, ' ');
                        }
                    }

                    /* Removes overflowing characters here, that would
                    make the KeyWord bigger than the message as a result of
                    adding spaces */

                    while (newKey.Length > MessageToEncrypt.Length)
                    {
                        //Removes the last character.
                        newKey.Remove(newKey.Length - 1, 1);
                    }
                }
                KeyWord = newKey.ToString();
            }
            return KeyWord;
        }

        /// <summary>
        /// Removes punctation from the message, and returns it.
        /// </summary>
        public string RemovePunctuation(string Message)
        {
            /* Checks for and removes any punctuation.
            Retains any space characters ' ' in the message. */

            StringBuilder messageCleanser = new StringBuilder();
            foreach (char letter in Message)
            {
                if (letter == ' ' || !Char.IsPunctuation(letter))
                {
                    messageCleanser.Append(letter);
                }
                else
                {
                    continue;
                }
            }

            return messageCleanser.ToString();
        }

        /// <summary>
        /// Populates the LetterGrid in a specific way
        /// custom to the Porta Cipher
        /// :: http://practicalcryptography.com/ciphers/classical-era/porta/
        /// </summary>
        private void PopulateLetterTable()
        {
            string alphabet =
                "nopqrstuvwxyzabcdefghijklm " +
                "opqrstuvwxyznmabcdefghijkl " +
                "pqrstuvwxyznolmabcdefghijk " +
                "qrstuvwxyznopklmabcdefghij " +
                "rstuvwxyznopqjklmabcdefghi " +
                "stuvwxyznopqrijklmabcdefgh " +
                "tuvwxyznopqrshijklmabcdefg " +
                "uvwxyznopqrstghijklmabcdef " +
                "vwxyznopqrstufghijklmabcde " +
                "wxyznopqrstuvefghijklmabcd " +
                "xyznopqrstuvwdefghijklmabc " +
                "yznopqrstuvwxcdefghijklmab " +
                "znopqrstuvwxybcdefghijklma";

            string[] alphabetGrid = alphabet.Split(' ');

            string temp;
            for (int i = 0; i < LetterTable.GetLength(0); i++)
            {
                for (int j = 0; j < LetterTable.GetLength(1); j++)
                {
                    temp = alphabetGrid[i];
                    LetterTable[i, j] = temp[j]; //Alphabet[j];
                }
            }
        }
    }
}
