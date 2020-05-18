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
    /// Interaction logic for PortaPage.xaml
    /// </summary>
    public partial class PortaPage : Page
    {
        // PortaEncryptor Object used in encryption/decryption.
        PortaEncryptor portaEncryptorObj = new PortaEncryptor();
        public PortaPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Encrypts the input using the Porta cipher.
        /// </summary>
        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(keyInputTextBox.Text))
            {
                MessageBox.Show("No keyword input.");
            }
            else
            {
                try
                {
                    // Encrypt plaintext entered, using the key, and the Porta obj.
                    outputTextBox.Text = portaEncryptorObj.Encrypt(plainTextInputTextBox.Text, keyInputTextBox.Text);
                } 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
            }
        }

        /// <summary>
        /// Decrypts the input using the Porta cipher
        /// </summary>
        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(keyInputTextBox.Text))
            {
                MessageBox.Show("No keyword input.");
            }
            else
            {
                try
                {
                    // Decrypt plaintext entered, using the key, and the Porta obj.
                    outputTextBox.Text = portaEncryptorObj.Decrypt(plainTextInputTextBox.Text, keyInputTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
