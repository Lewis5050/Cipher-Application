using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptex
{
    class BeaufortEncryptor
    {
        // Letter Table represents the letter table used in the Beaufort Cipher.
        private char[,] LetterTable = new char[26, 26]; 
        private List<char> Alphabet = new List<char>();

        // Encrypted Result
        public string EncryptedText;

        // Constuctor
        public BeaufortEncryptor()
        {
            AddAlphabet();
            PopulateLetterTable();
        }

        /// <summary>
        /// Encrypts the input using the Beaufort cipher.
        /// </summary>
        public string Encrypt(string PlainText, string KeyWord)
        {
            PlainText = PlainText.ToLower();
            KeyWord = KeyWord.ToLower();

            // Checks for, and removes punctuation (apart from spaces).
            StringBuilder plainTextCleanser = new StringBuilder();
            foreach (char letter in PlainText)
            {
                if (letter == ' ' || !Char.IsPunctuation(letter))
                {
                   plainTextCleanser.Append(letter);
                } 
                else
                {
                    continue;
                }
              
            }

            PlainText = plainTextCleanser.ToString();
            KeyWord = KeyWordCleanser(KeyWord);

            // Resizes the KeyWord
            KeyWord = Resize(PlainText, KeyWord);

            /* Using the Beaufort Cipher, encrypt the message using the KeyWord and PlainText over the 
             *  'tabula recta' Link: http://practicalcryptography.com/ciphers/classical-era/beaufort/ */

            StringBuilder encryptedMessage = new StringBuilder();
            
            for (int i = 0; i < PlainText.Length; i++)
            {
                // Keeps the space between the words.
                if (PlainText[i] == ' ')
                {
                    encryptedMessage.Append(' ');
                }
                else
                {
                    // Traverse the 'X' coordinate of the LetterTable[,] array.
                    for (int j = 0; j < LetterTable.GetLength(0); j++)
                    {
                        // Check if the 'X' coordinate matches MessageEncrypted[i]
                        if (LetterTable[0, j] == PlainText[i])
                        {
                            // Checks the each letter on the 'Y' coordinate for KeyWord[i] in column 'X'.
                            for (int k = 0; k < LetterTable.GetLength(1); k++)
                            {
                                // When there's a match, get the letter.
                                if (LetterTable[k, j] == KeyWord[i])
                                {
                                    encryptedMessage.Append(LetterTable[k, 0]); //(char)(97 + k));
                                }
                            }
                        }
                    }
                }
            }

            EncryptedText = encryptedMessage.ToString();
            return EncryptedText;
            
        }

        /// <summary>
        /// Decrypts the input that was encrypted with the Beaufort cipher.
        /// </summary>
        public string Decrypt(string MessageEncrypted, string KeyWord)
        {
            MessageEncrypted = MessageEncrypted.ToLower();
            KeyWord = KeyWord.ToLower();

            KeyWord = Resize(MessageEncrypted, KeyWord);

            /* Using the Beaufort Cipher, decrypt the message using the KeyWord by reversing the encryption process via the 
             *  'tabula recta' Link: http://practicalcryptography.com/ciphers/classical-era/beaufort/ */

            StringBuilder decryptedMessage = new StringBuilder();
            for (int i = 0; i < MessageEncrypted.Length; i++)
            {
                // Keeps spaces between words.
                if (MessageEncrypted[i] == ' ')
                {
                    decryptedMessage.Append(' ');
                } 
                else
                {
                    // Traverse the 'Y' coordinate of the LetterTable[,] array.
                    for (int j = 0; j < LetterTable.GetLength(1); j++)
                    {
                        // Check if the 'Y' coordinate matches MessageEncrypted[i]
                        if (LetterTable[j, 0] == MessageEncrypted[i])
                        {
                            // Checks the each letter on the 'X' coordinate for KeyWord[i] on row 'Y'.
                            for (int k = 0; k < LetterTable.GetLength(0); k++)
                            {
                                // When there's a match, get the letter.
                                if (LetterTable[j,k] == KeyWord[i])
                                {
                                    decryptedMessage.Append(LetterTable[0, k]);
                                }
                            }
                        }
                    }
                }
            }
            return decryptedMessage.ToString();
        }

        /// <summary>
        /// Resizes the KeyWord in order to match the length of the 
        /// Message, it accounts of spaces between the words.
        /// </summary>
        private string Resize(string Message, string KeyWord)
        {
            if (KeyWord.Contains(' '))
            {
                throw new ArgumentException("Key cannot contain spaces or punctuation");
            }

            /* Increases the length of the KeyWord by repeating itself
             * in order to match the length of the Message. This also 
             * accounts for spaces within the Message. */

            if (KeyWord.Length < Message.Length)
            {
                // Matches the length of the Message
                StringBuilder newKey = new StringBuilder(KeyWord);

                /* For-Loop doesn't work here if the difference between
                Message.Length and KeyWord.Length > KeyWord.Length */

                int indexer = 0;
                while(newKey.Length != Message.Length)
                {
                    if (indexer == KeyWord.Length)
                    {
                        indexer = 0;
                    } 
                    else
                    {
                        newKey.Append(KeyWord[indexer]);
                        indexer++;
                    }
                }

                /* If the Message contains spaces, we'll add them into the KeyWord here
                 * and remove any 'overflowing' characters in the KeyWord as a result of
                 * this change, in order to match the length of the Message. */

                if (Message.Contains(' '))
                {
                    for (int i = 0; i < Message.Length; i++)
                    {
                        if (Message[i] == ' ')
                        {
                            newKey.Insert(i, ' ');
                        }
                    }

                    // Removes overflow here.
                    while (newKey.Length > Message.Length)
                    {
                        //Removes the last character.
                        newKey.Remove(newKey.Length - 1, 1);
                    }
                }

                KeyWord = newKey.ToString();
            }
           else 
           {
                // Tackles the potential issue of the key, being bigger than the Message. 
                throw new ArgumentException("Key cannot exceed the length of the message.");
           }
            
            return KeyWord;
        }

        /// <summary>
        /// Removes any illgeal characters from the KeyWord.
        /// </summary>
        private string KeyWordCleanser(string KeyWord)
        {
            StringBuilder keyWordCleanser = new StringBuilder();
            for (int i = 0; i < KeyWord.Length; i++)
            {
                if (!Char.IsPunctuation(KeyWord[i]) && !Char.IsNumber(KeyWord[i]))
                {
                    keyWordCleanser.Append(KeyWord[i]);
                }
            }

            return keyWordCleanser.ToString();
        }

        /// <summary>
        /// Populates the LetterTable array to create the table used in the 
        /// Beaufort cipher.
        /// </summary>
        private void PopulateLetterTable()
        {
            /* Populates the LetterTable[,] by creating the table used in the Beaufort
             * Cipher. Each row is the alphabet pushed back by one letter.
             * 
             * E.g Row 0 = A B C D E F ...
             *     Row 1 = B C D E F G ...
             * 
             * It does this by removing the letter at the beginning of the Alphabet List
             * and sending it to the end. */
            
            for (int i = 0; i < LetterTable.GetLength(0); i++)
            {
                for (int j = 0; j < LetterTable.GetLength(1); j++)
                {
                    LetterTable[i, j] = Alphabet[j];
                }

                Alphabet.Add(Alphabet[0]);
                Alphabet.RemoveAt(0);
            }
        }

        /// <summary>
        /// Adds the alphabet to the Alphabet list for use in the 
        /// PopulateLetterTable() method.
        /// </summary>
        private void AddAlphabet()
        {
            char[] Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();
            foreach (char letter in Alpha)
            {
                Alphabet.Add(letter);
            }
        }
    }
}
