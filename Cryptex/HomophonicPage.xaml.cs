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
    /// Interaction logic for HomophonicPage.xaml
    /// </summary>
    public partial class HomophonicPage : Page
    {
        // HomophonicEncryptor Object used in encryption/decryption.
        HomophonicSubstitution homophonicEncryptorObj = new HomophonicSubstitution();
        bool substitutionSet = false;

        public HomophonicPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the subsitutions for the cipher.
        /// </summary>
        private void setSubstitutionsButton_Click(object sender, RoutedEventArgs e)
        {
            // Easy way to set the values.
            int columnCount = 0;
            foreach(TextBox charBox in row1.Children.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(charBox.Text))
                {
                    continue;
                }
                else
                {
                    homophonicEncryptorObj.AddHomophone(charBox.Text[0], columnCount);
                    columnCount++;
                }
            }

            columnCount = 0;
            foreach (TextBox charBox in row2.Children.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(charBox.Text))
                {
                    continue;
                }
                else
                {
                    homophonicEncryptorObj.AddHomophone(charBox.Text[0], columnCount);
                    columnCount++;
                }
            }

            substitutionSet = true;
        }

        /// <summary>
        /// Encrypts the plain text using the homophonic substitution cipher with the 
        /// subsitutions set by the user.
        /// </summary>
        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!substitutionSet)
            {
                MessageBox.Show("No/Not all substiutions have been set.");
            }
            else
            {
                // Encrypt plaintext entered, using the key, and the Homophonic obj.
                outputTextBox.Text = homophonicEncryptorObj.Encrypt(plainTextInputTextBox.Text);
            }
        }
    }
}
