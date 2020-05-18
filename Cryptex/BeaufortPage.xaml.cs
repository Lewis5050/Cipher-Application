using System;
using System.Collections.Generic;
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
    /// Interaction logic for BeaufortPage.xaml
    /// </summary>
    public partial class BeaufortPage : Page
    {
        // BeaufortEncryptor Object used in encryption/decryption.
        BeaufortEncryptor beaufortEncryptorObj = new BeaufortEncryptor();
        public BeaufortPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Encrypts the input using the Beaufort cipher.
        /// </summary>
        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Encrypts plaintext entered, using the key, and the Beaufort obj.
                outputTextBox.Text = beaufortEncryptorObj.Encrypt(plainTextInputTextBox.Text, keyInputTextBox.Text);
            }
            catch(Exception ex)
            {
                // Shows any exceptions caught in the encryption process.
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Decrypts the input using the Beaufort cipher.
        /// </summary>
        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Decrypts encrypted text entered, using the key, and the Beaufort obj.
                outputTextBox.Text = beaufortEncryptorObj.Decrypt(plainTextInputTextBox.Text, keyInputTextBox.Text);
            }
            catch (Exception ex)
            {
                // Shows any exceptions caught in the encryption process.
                MessageBox.Show(ex.Message);
            }
        }
    }
}
