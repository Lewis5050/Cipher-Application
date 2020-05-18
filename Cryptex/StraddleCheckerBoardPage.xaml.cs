using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cryptex
{
    /// <summary>
    /// Interaction logic for StraddleCheckerBoardPage.xaml
    /// </summary>
    public partial class StraddleCheckerBoardPage : Page
    {
        StraddleCheckerboardEncryptor straddleEncryptorObj = new StraddleCheckerboardEncryptor();

        public StraddleCheckerBoardPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Encrypts the input using the Straddle Checker Board Cipher.
        /// </summary>
        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            getAlphabet();

            try
            {
                // Encrypt plaintext entered, using the key, and the Straddle obj.
                outputTextBox.Text = straddleEncryptorObj.Encrypt(plainTextInputTextBox.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Gets the Alphabet input from the users.
        /// </summary>
        private void getAlphabet()
        {
            StringBuilder alphabet = new StringBuilder();
            foreach (TextBox charBox in row1.Children.OfType<TextBox>())
            {
                // Checks if the input contains puncutation or numbers

                for (int i = 0; i < charBox.Text.Length; i++)
                {
                    if (char.IsDigit(charBox.Text[i]) || char.IsPunctuation(charBox.Text[i]))
                    {
                        MessageBox.Show("No puncutation or numerical characters allowed.");
                        break;
                    }
                }

                // Checks if all boxes have an input

                if (String.IsNullOrEmpty(charBox.Text))
                {
                    MessageBox.Show("Empty input boxes");
                    break;
                }
                else
                {
                    alphabet.Append(charBox.Text);
                }
            }

            try
            {
                // Populates checkerboard.
                straddleEncryptorObj.PopulateCheckerBoard(alphabet.ToString(), int.Parse(spacingOne.Text), int.Parse(spacingTwo.Text));
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Decrypts the input using the Straddle Checker Board Cipher.
        /// </summary>
        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            getAlphabet();

            try
            {
                // Decrypt plaintext entered, using the key, and the Straddle obj.
                outputTextBox.Text = straddleEncryptorObj.Decrypt(plainTextInputTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
