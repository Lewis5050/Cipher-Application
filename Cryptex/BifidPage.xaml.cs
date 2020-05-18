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
    /// Interaction logic for BifidPage.xaml
    /// </summary>
    public partial class BifidPage : Page
    {
        // BifidEncryptorObj used in the encryption and decryption process.
        BifidEncryptor bifidEncryptorObj = new BifidEncryptor();

        // List of all character input controls on the UI - Makes it easy to gather values for encryption.
        List<TextBox> characterGridList = new List<TextBox>();

        public BifidPage()
        {
            InitializeComponent();
            PopulateList();
        }

        /// <summary>
        /// Encrypts the input using the Bifid cipher.
        /// </summary>
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            // Initial check to ensure all character boxes have a value.
            for (int i = 0; i < characterGridList.Count; i++)
            {
                if (characterGridList[i].Text == null)
                {
                    MessageBox.Show("Not all character boxes have a value");
                    return;
                }
            }

            // Checks there is an appropriate spacing input.
            if (string.IsNullOrEmpty(SpacingInputTextBox.Text) || !Char.IsNumber(SpacingInputTextBox.Text[0]))
            {
                MessageBox.Show("No Spacing input recieved.");
                return;
            }

            try
            {
                OutputTextBox.Text = bifidEncryptorObj.Encrypt(PlainTextInputTextBox.Text, Convert.ToInt32(SpacingInputTextBox.Text));
            } 
            catch (Exception ex)
            {
                // Shows any exceptions caught in the encryption process.
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Decrypts the input using the Bifid cipher.
        /// </summary>
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            // Initial check to ensure all character boxes have a value.
            for (int i = 0; i < characterGridList.Count; i++)
            {
                if (characterGridList[i].Text == null)
                {
                    MessageBox.Show("Not all character boxes have a value");
                    return;
                }
            }

            // Checks there is an appropriate spacing input.
            if (string.IsNullOrEmpty(SpacingInputTextBox.Text) || !Char.IsNumber(SpacingInputTextBox.Text[0]))
            {
                MessageBox.Show("No Spacing input recieved.");
                return;
            }

            try
            {
                OutputTextBox.Text = bifidEncryptorObj.Decrypt(PlainTextInputTextBox.Text, Convert.ToInt32(SpacingInputTextBox.Text));
            }
            catch (Exception ex)
            {
                // Shows any exceptions caught in the decryption process.
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Randomly populates the BifidEncryptorObj's charGrid required
        /// for encryption/decryption.
        /// </summary>
        private void AutoPopulateButton_Click(object sender, RoutedEventArgs e)
        {
            bifidEncryptorObj.PopulateCharGrid();

            // Following code displays the value of the auto-generated charGrid.
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    characterGridList[count].Text = bifidEncryptorObj.charGrid[i, j].ToString();
                    count++;
                }
            }
        }

        /// <summary>
        /// Manually populates the BifidEncryptorObj's charGrid required
        /// for encryption/decryption.
        /// </summary>
        private void ManualPopulateButton_Click(object sender, RoutedEventArgs e)
        {
            // Initial check to ensure all character boxes have a value.
            for (int i = 0; i < characterGridList.Count; i++)
            {
                if (string.IsNullOrEmpty(characterGridList[i].Text))
                {
                    MessageBox.Show("Not all character boxes have a value.");
                    return;
                }
            }

            // Converts the input across all the text boxes to a string to pass as the parameter.
            StringBuilder manualCharGrid = new StringBuilder();
            for (int i = 0; i < characterGridList.Count; i++)
            {
                manualCharGrid.Append(characterGridList[i].Text[0]);
            }

            // Populates the charGrid assuming no exceptions are thrown.
            try
            {
                bifidEncryptorObj.PopulateCharGrid(manualCharGrid.ToString());
            } 
            catch (Exception ex)
            {
                // Shows any exceptions caught in the population process.
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Populates the List that contains all the character input text boxes
        /// </summary>
        private void PopulateList()
        {
            // Adding to List makes the encryption process easier.

            characterGridList.Add(Row1TextBox1);
            characterGridList.Add(Row1TextBox2);
            characterGridList.Add(Row1TextBox3);
            characterGridList.Add(Row1TextBox4);
            characterGridList.Add(Row1TextBox5);
            characterGridList.Add(Row2TextBox1);
            characterGridList.Add(Row2TextBox2);
            characterGridList.Add(Row2TextBox3);
            characterGridList.Add(Row2TextBox4);
            characterGridList.Add(Row2TextBox5);
            characterGridList.Add(Row3TextBox1);
            characterGridList.Add(Row3TextBox2);
            characterGridList.Add(Row3TextBox3);
            characterGridList.Add(Row3TextBox4);
            characterGridList.Add(Row3TextBox5);
            characterGridList.Add(Row4TextBox1);
            characterGridList.Add(Row4TextBox2);
            characterGridList.Add(Row4TextBox3);
            characterGridList.Add(Row4TextBox4);
            characterGridList.Add(Row4TextBox5);
            characterGridList.Add(Row5TextBox1);
            characterGridList.Add(Row5TextBox2);
            characterGridList.Add(Row5TextBox3);
            characterGridList.Add(Row5TextBox4);
            characterGridList.Add(Row5TextBox5);
        }
    }
}
