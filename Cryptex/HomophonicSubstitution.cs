using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptex
{
    // The term 'homophone' throughout this file refers to letter substitutes.

    class HomophonicSubstitution
    {
        // Used as reference for adding the alphabet to the HomophoneGrid
        private readonly char[] AlphabetArr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        /* Is central for the encryption process, as it keeps track of the values(homophones)
        assigned to each letter of the alphabet. */
        public char[,] HomophoneGrid = new char[3, 26];

        public HomophonicSubstitution()
        {
            SetHomophoneGrid();
            // TestReady("ELIFQKBMDAXGSYOJWUZCNRVPHT");
        }

        /// <summary>
        /// Sets the Alphabet as HomophoneGrid[0, 0->25],
        /// and sets values to '!' to indicate an empty value.
        /// </summary>
        private void SetHomophoneGrid()
        {
            /* Sets the alphabet as row 0 in the HomophoneGrid[,] matrix. Using
             * AlphabetArr as reference for this. */

            for (int i = 0; i < HomophoneGrid.GetLength(1); i++)
            {
                HomophoneGrid[0, i] = AlphabetArr[i];
            }

            /* Then sets every index not on row 0 to '!'. '!' is used in this case
             * to indicate no value, or an unassigned value(homophone). */

            for (int i = 1; i < HomophoneGrid.GetLength(0); i++)
            {
                for (int j = 0; j < HomophoneGrid.GetLength(1); j++)
                {
                    HomophoneGrid[i, j] = '!';
                }
            }
        }

        /// <summary>
        /// Adds the specified Homophone at the specified index.
        /// The column parameter should be the return of the GetColumn method.
        /// The row parameter is by default: 1.
        /// </summary>
        public void AddHomophone(char Value, int Column, int Row = 1)
        {
            /* As row 0 of the HomophoneGrid[,] matrix is the alphabet, by
             * default we do not need to include 0 as the row starting point. We also
             * don't really NEED to specify a row, as the following code will cycle through
             * each row anyways (hence it is an optional parameter).
             * 
             * The following code checks to see if there's a value(homophone) assigned, and if
             * not assign the value parameter to it, otherwise cycle through all other possible values(homophones)
             * positions for that letter. (Possible values are limited by the size of the HomophoneGrid[,] matrix size. */

            if (HomophoneGrid[Row, Column] == '!')
            {
                HomophoneGrid[Row, Column] = Value;
            } 
            else
            {
               // i = 2, as the if statement above checks what would be i = 1.
               for (int i = 2; i < HomophoneGrid.GetLength(0); i++)
                {
                    if (HomophoneGrid[i, Column] == '!')
                    {
                        HomophoneGrid[i, Column] = Value;
                        // Breaking stops the same letter being assigned endlessly.
                        break;
                    }
               }
            }
        }

        /// <summary>
        /// Removes a homophone from the HomophoneGrid[,] matrix. The column
        /// parameter should be the return from the GetColumn() method.
        /// </summary>
        public void RemoveHomophone(char Homophone, int Column)
        {
            for (int i = 1; i < HomophoneGrid.GetLength(0); i++)
            {
                if (HomophoneGrid[i, Column] == Homophone)
                {
                    HomophoneGrid[i, Column] = '!';

                    /* When a value(homophone) is removed, shift any values(homophones)
                     * below it in the HomophoneGrid[,] matrix upwards to cover what would 
                     * be a gap in the grid. */
              
                    if (i == HomophoneGrid.GetLength(0))
                    {
                        break;
                    }
                    else
                    {
                        // Shuffle all other homophnes down.
                        for (int j = (i + 1); i < HomophoneGrid.GetLength(0); i++)
                        {
                            if (HomophoneGrid[j, Column] != '!')
                            {
                                HomophoneGrid[(j - 1), Column] = HomophoneGrid[j, Column];
                                HomophoneGrid[j, Column] = '!';
                            }
                        }
                    } 
                }
            }
        }

        /// <summary>
        /// Returns the column within the HomophoneGrid matrix
        /// </summary>
        private int GetColumn(char AlphabetLetter)
        {
            /* As the first row in the HomophoneGrid[,] matrix is the alphabet,
             * we can use the first row in order to get the column for the right
             * letter when adding homophones to the matrix.
             * 
             * E.g GetColumn('A') will return 0, and so using this can add homophones 
             * to the 'A' column via the AddHomophone() method. */
             
            int forReturn = 0;
            for (int i = 0; i < HomophoneGrid.GetLength(1); i++)
            {
                if (HomophoneGrid[0, i] == AlphabetLetter)
                {
                    forReturn = i;
                }
            }
            return forReturn;
        }

        /// <summary>
        /// Counts the number of homophones.
        /// </summary>
        private int HomophoneCount(int Column)
        {
            /* Counts the amount of values(homophones) there are for
             * the letter of the alphabet at the column in the parameter.
             * E.g column 0 would be 'A' .... 25 would be 'Z'. */

            // '!' in this case, represents no assigned value(homophone).

            int count = 0;
            for (int i = 0; i < HomophoneGrid.GetLength(0); i++)
            {
                if (HomophoneGrid[i, Column] != '!')
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Selects randomly a homophone for the letter at the column.
        /// Parameter should be the return of the GetColumn method.
        /// </summary>
        private char HomophoneSelector(int Column)
        {
            // New Random Object
            Random rand = new Random();

            // Gets a number between 1, and the amount of homophones in that column.
            int homoNumber = rand.Next(1, HomophoneCount(Column));

            // Returns the homophone at HomophoneGrid[homophoneNumber, column]
            char encryptedLetter = HomophoneGrid[homoNumber, Column];
            return encryptedLetter;
        }

        /// <summary>
        /// Encrypts the string using the Homophonic Substitution Cipher.
        /// </summary>
        public string Encrypt(string MessageToEncrypt)
        {
            /* Converts input to uppercase as the alphabet row in the HomophoneGrid[,] matrix
             used as a reference point is also uppercase and wouldn't work as intended if the
             input was lower case. */

            MessageToEncrypt = MessageToEncrypt.ToUpper();

            /* As you cannot encrypt punctuation in the Homophonic Substitution Cipher, an exception
             * should be thrown if the input contains such. The only piece of punctuation that should be
             * accepted is spaces(' '). The following code does this: */

            MessageToEncrypt = RemovePunctuation(MessageToEncrypt);

            // The encryption process:

            StringBuilder encryptedMessage = new StringBuilder();
            for (int i = 0; i < MessageToEncrypt.Length; i++)
            {
                /* If the character in the input string is a space, add this to the
                encryptedMessage so individual words can still be seen. */

                if (MessageToEncrypt[i] == ' ')
                {
                    encryptedMessage.Append(' ');
                } 
                else
                {
                    encryptedMessage.Append(HomophoneSelector(GetColumn(MessageToEncrypt[i])));
                }
                
            }
            return encryptedMessage.ToString();
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
        /// Used for testing and debug purposes to pre-populate/quickly populate the HomophoneGrid. 
        /// Used in the constructor only.
        /// </summary>
        private void TestReady(string firstRow) // Possible parameter: "ELIFQKBMDAXGSYOJWUZCNRVPHT"
        {
            for (int i = 0; i < AlphabetArr.Length; i++)
            {
                AddHomophone(firstRow[i], GetColumn(AlphabetArr[i]));
            }

            AddHomophone('0', GetColumn('A'));
            AddHomophone('1', GetColumn('B'));
            AddHomophone('2', GetColumn('C'));
        }


    }
}

        

      
    

