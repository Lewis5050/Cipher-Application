using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cryptex
{
    class BifidEncryptor
    {
        // Static Alphabet Array
        private static char[] Alphabet = new char[]

            { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
             'i', 'k', 'l', 'm', 'n', 'o', 'p',
             'q', 'r', 's', 't', 'u', 'v', 'w',
             'x', 'y', 'z' };

        // Character Grid. (5x5)
        public readonly char[,] charGrid;

        // Constructor
        public BifidEncryptor()
        {
            charGrid = new char[5, 5];
        }

        /// <summary>
        /// Randomly populates charGrid
        /// </summary>
        public char[,] PopulateCharGrid()
        {
            Random randomNumberGenerator = new Random();

            // Creates a list based off of Alphabet.
            List<char> seedingAlphabet = Alphabet.ToList();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // Generates a number between 0, and seedingAlphabet's length.
                    var generatedNumber = randomNumberGenerator.Next(0, seedingAlphabet.Count);
                    this.charGrid[i, j] = seedingAlphabet[generatedNumber];

                    // Removes element at generatedNumber's index to prevent multiple occurances of the same letter.
                    seedingAlphabet.RemoveAt(generatedNumber);
                }
            }
            return charGrid;
        }

        /// <summary>
        /// Manually populates charGrid via a string input.
        /// </summary>
        public char[,] PopulateCharGrid(string letterString)
        {
            // Checks string is correct length. // MIGHT BE UNEEDED DUE TO UI VALIDATION
            if (letterString.Length != 25)
            {
                throw new ArgumentException("Invalid input length. Must be equal to 25.");
            }

            // Checks for repeating characters, as well as J (J is excluded for the Bifid cipher to work).
            if (!OnlyOnceCheck(letterString) || letterString.Contains("j"))
            {
                throw new ArgumentException("There can be no repeating letters / and there can be no 'J'");
            }

            // Checks primarily for characters.
            for (int i = 0; i < letterString.Length; i++)
            {
                if (!Char.IsLetter(letterString[i]))
                {
                    throw new ArgumentException("Only letters are accepted.");
                }
            }

            // IEnuerator to start where the process left off each time.
            // Populates the charGrid using the string input.
            IEnumerator<char> customGrid = letterString.GetEnumerator();

            for (int i = 0; i < charGrid.GetLength(0); i++)
            {
                for (int j = 0; j < charGrid.GetLength(1); j++)
                {
                    customGrid.MoveNext();
                    charGrid[i, j] = customGrid.Current;
                }
            }
            return charGrid;
        }

        /// <summary>
        /// Gets the 'coordinates' of a letter within the charGrid
        /// </summary>
        private Tuple<int, int> GetCoords(char MessageLetter)
        {
            for (int i = 0; i < charGrid.GetLength(0); i++)
            {
                for (int o = 0; o < charGrid.GetLength(1); o++)
                {
                    if (charGrid[i, o] == MessageLetter)
                    {
                        return Tuple.Create(i, o);
                    }
                }
            }

            return Tuple.Create(0, 0);
        }

        /// <summary>
        /// Encrypts the MessageToEncrypt parameter using the Bifid Cipher. Uses the values in
        /// the charGrid[,] to do this, along with the Spacing parameter.
        /// </summary>
        public string Encrypt(string MessageToEncrypt, int Spacing)
        {
            // Spacing cannot exceed the message length, otherwise Spacing == 0, meaning the cipher is pointless.
            if (Spacing > MessageToEncrypt.Length)
            {
                throw new ArgumentException("Spacing cannot be greater than the length of the message.");
            }
            
            // Removes special characters/spaces from the message.
            StringBuilder stringCleanser = new StringBuilder();
            for (int i = 0; i < MessageToEncrypt.Length; i++)
            {
                if (MessageToEncrypt[i] >= 'A' && MessageToEncrypt[i] <= 'z')
                {
                    stringCleanser.Append(MessageToEncrypt[i]);
                }
            }

            MessageToEncrypt = stringCleanser.ToString().ToLower();

            int[] x = new int[MessageToEncrypt.Length]; // X coordinates for each letter (In terms of charGrid).
            int[] y = new int[MessageToEncrypt.Length]; // Y coordinate for each letter (In terms of charGrid).

            // Gets the 'coordinates' for each letter 
            for (int i = 0; i < MessageToEncrypt.Length; i++)
            {
                var coords = GetCoords(MessageToEncrypt[i]);
                y[i] = coords.Item1;
                x[i] = coords.Item2;
            }

            // Turns the arrays into strings.
            string Ycoords = string.Join(string.Empty, y);
            string Xcoords = string.Join(string.Empty, x);
            
            // Add spaces to the string of 'coordinates' specified by the Spacing parameter.
            int currentPosition = 0;
            StringBuilder XafterSpaces = new StringBuilder();
            StringBuilder YafterSpaces = new StringBuilder();
            while (currentPosition + Spacing < Xcoords.Length)
            {
                XafterSpaces.Append(Xcoords.Substring(currentPosition, Spacing)).Append(' ');
                YafterSpaces.Append(Ycoords.Substring(currentPosition, Spacing)).Append(' ');
                currentPosition += Spacing;
            }
            if (currentPosition < Xcoords.Length)
            {
                XafterSpaces.Append(Xcoords.Substring(currentPosition));
                YafterSpaces.Append(Ycoords.Substring(currentPosition));
            }

            Xcoords = XafterSpaces.ToString();
            Ycoords = YafterSpaces.ToString();

            // Makes each block a substring as part of an array, by splitting at the spaces.
            string[] Ysubstrings = Ycoords.Split(' '); 
            string[] Xsubstrings = Xcoords.Split(' '); 

            // Join each X and Y block together.
            StringBuilder coordinateJoiner = new StringBuilder();
            for (int i = 0; i < Ysubstrings.Length; i++)
            {
                coordinateJoiner.Append(Ysubstrings[i]);
                coordinateJoiner.Append(Xsubstrings[i]);
            }

            coordinateJoiner.Replace(" ", string.Empty);

            //Converts every 2 numbers in the final concatenated int array to a letter using the grid.
            int count = 0;
            StringBuilder output = new StringBuilder();

            while (count < coordinateJoiner.Length)
            {
                int row = (int)char.GetNumericValue(coordinateJoiner[count]);
                int col = (int)char.GetNumericValue(coordinateJoiner[count + 1]);

                output.Append(charGrid[row, col]);

                count = count + 2;
            }

            return output.ToString();
        }

        /// <summary>
        /// Decrypts the EncryptedMessage parameter using the Bifid Cipher. Results are 
        /// only accurate when the Spacing parameter, and charGrid arrangement are the same
        /// as when encrypted.
        /// </summary>
        public string Decrypt(string EncryptedMessage, int Spacing)
        {
            // Spacing cannot exceed the message length, otherwise Spacing == 0, meaning the cipher is pointless.
            if (Spacing > EncryptedMessage.Length)
            {
                throw new ArgumentException("Spacing cannot be greater than the length of the message.");
            }

            // Converts the letters back into numbers.
            List<int> encryptedMessageCoordinates = new List<int>();
            for (int i = 0; i < EncryptedMessage.Length; i++)
            {
                var coords = GetCoords(EncryptedMessage[i]);
                encryptedMessageCoordinates.Add(coords.Item1);
                encryptedMessageCoordinates.Add(coords.Item2);
            }

            // Turns the list of coordinates into a string for seperation.
            StringBuilder encryptedMessageCoordinatesString = new StringBuilder();
            foreach (int number in encryptedMessageCoordinates)
            {
                encryptedMessageCoordinatesString.Append(number);
            }

            string finalNumbers = encryptedMessageCoordinatesString.ToString();

            /* Because spacing during encryption is done within the X and Y coordinate arrays,
             * it is possible for spacing to occur with odd numbers if the MessageToEncrypt.Length is odd,
             * this means that there may be a an uneven amount of numbers in some of the 'blocks' created as 
             * a result of adding spaces. And because each X and Y 'block' are joined together in the encryption 
             * process, the final few 'blocks' will in theory be irregular compared to the rest because they have 
             * less numbers than the others.
             * 
             * This causes issues during decryption, as the spacing needs to be the same to get accurate decryption results because 
             * it needs to have the X,Y coordinates for the charGrid. The original problem was that adding spacing to the finalNumbers 
             * string was that it is double the characters of the encryptedMessage, meaning it will be even. As a result the spacing 
             * unless exact will create a 'block' half the size of the 'irregular' blocks we talked about above at the end.
             * 
             * In order to fix this, we can remove what would be the uneven blocks in the X and Y arrays and store them seperately. Now
             * the 'blocks' created by adding spaces should mirror the same 'blocks' in the encryption process. Then, we add back the
             * uneven 'blocks' later in the program. Doing so will mirror the X and Y arrays after spacing has been added in the encryption 
             * process, allowing accurate decryption. */

            int remainder = EncryptedMessage.Length % Spacing;

            // Removes the what would be the 'uneven' blocks from the finalNumbers string.
            string[] overflow = new string[2];
            overflow[0] = finalNumbers.Substring(finalNumbers.Length - (remainder * 2), remainder);
            overflow[1] = finalNumbers.Substring(finalNumbers.Length - remainder, remainder);

            // Updates the finalNumbers string by removing the above overflow[0] and [1].
            finalNumbers = finalNumbers.Substring(0, finalNumbers.Length - (remainder * 2));

            /* The following code seperates the long list of numbers out by adding the spaces
             * specified in the Decrypt() parameters, which then (if the spacing is the same as when
             * encrypted) shows each Xcoord and Ycoord block. Which can then be assinged to their own array. 
             * I've had to use this cumbersome technique, as Regex.replace() wasn't working as it did in the
             * encrypt method. */

            StringBuilder resultAfterSpacesAdded = new StringBuilder();

            int currentPosition = 0;
            while (currentPosition + Spacing < finalNumbers.Length)
            {
                resultAfterSpacesAdded.Append(finalNumbers.Substring(currentPosition, Spacing)).Append(' ');
                currentPosition += Spacing;
            }
            if (currentPosition < finalNumbers.Length)
            {
                resultAfterSpacesAdded.Append(finalNumbers.Substring(currentPosition, Spacing));
            }

            finalNumbers = resultAfterSpacesAdded.ToString();

            // Splits the finalNumbers string into an array of strings, by splitting at the spaces added above.
            string[] coordinatesSubStringArray = finalNumbers.Split(' ');
            List<string> Xsubstrings = new List<string>();
            List<string> Ysubstrings = new List<string>();

            // Alternates to assign X and Y 'coordinate' blocks to each array.
            bool alternator = false;
            for (int i = 0; i < coordinatesSubStringArray.Length; i++)
            {
                if (alternator)
                {
                    Xsubstrings.Add(coordinatesSubStringArray[i]);
                    alternator = false;
                } 
                else
                {
                    Ysubstrings.Add(coordinatesSubStringArray[i]);
                    alternator = true;
                }
            }

            // Add the removed 'overflow' 
            Xsubstrings.Add(overflow[1]);
            Ysubstrings.Add(overflow[0]);

            // Turns the substring lists to an array.
            Xsubstrings.ToArray();
            Ysubstrings.ToArray();

            // Turns the values in this array into a string.
            StringBuilder x = new StringBuilder();
            StringBuilder y = new StringBuilder();
            for (int i = 0; i < Xsubstrings.Count; i++)
            {
                x.Append(Xsubstrings[i]);
                y.Append(Ysubstrings[i]);
            }

            // Turns these strings into a char array so we can use as index values for charGrid.
            char[] Xcoords = x.ToString().ToCharArray();
            char[] Ycoords = y.ToString().ToCharArray();

            // Gets the letter from the charGrid using the decrypted 'coordinates'
            StringBuilder decryptedOutput = new StringBuilder();
            for (int i = 0; i < EncryptedMessage.Length; i++)
            {
                /* As casting a char to an int will return the ASCII value of that character, I've removed the ASCII value of '0' from the 
                cast, as this will give the 'literal' cast. '1' == 1 which can be used to access the index. Otherwise an index out of 
                bounds error will be thrown. */

                decryptedOutput.Append(charGrid[((int)(Ycoords[i]) - '0'), ((int)(Xcoords[i]) - '0')]);
            }
            return decryptedOutput.ToString();
        }

        /// <summary>
        /// Checks if the charGrid is populated.
        /// </summary>
        /// <returns></returns>
        public bool CheckIfEmpty()
        {
            if ((int)charGrid[0, 0] == 0)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks for repeating characters within the string
        /// used in conjunction with the manual population of
        /// the charGrid
        /// </summary>
        /// 
        private static bool OnlyOnceCheck(string input)
        {
            var set = new HashSet<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (!set.Add(input[i])) 
                {
                    return false;
                }
                    
            }
            return true;
        }
    }
}
