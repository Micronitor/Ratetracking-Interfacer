using System;
using System.Windows.Forms;

namespace Ratetracking_Interfacer
{
    public partial class Form_main : Form
    {
        /// <summary>
        /// Initializes the Windows form application.
        /// </summary>
        public Form_main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initalizes the Sercom.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Sercom.SerialInitialize(this, new EventArgs(), comList, Baudrate);
        }

        /// <summary>
        /// Add selected COM port to Sercom serialPort Portname.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Sercom.serialPort.PortName = comList.SelectedItem.ToString();
                update_ComStatus(this, e);
                bt_serialConnect.Enabled = true;
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException)
                {
                    Sercom.serialPort.PortName = "NULL";
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Displays the Sercom SerialPort parameters to interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void update_ComStatus(object sender, EventArgs e)
        {
            ComStatus.Text = "Port: " + Sercom.serialPort.PortName + "\nBaudrate: " + Sercom.serialPort.BaudRate.ToString() + "\nParity: " + Sercom.serialPort.Parity + "\nDataBits: " + Sercom.serialPort.DataBits.ToString() + "\nStopBits: " + Sercom.serialPort.StopBits + "\nHandshake: " + Sercom.serialPort.Handshake;
        }

        /// <summary>
        /// Changes the Sercom SerialPort baudrate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Baudrate_TextChanged(object sender, EventArgs e)
        {
            if (Baudrate.Text.Length != 0)
                Sercom.serialPort.BaudRate = Int32.Parse(Baudrate.Text);
            update_ComStatus(this, e);
        }

        /// <summary>
        /// Initiates the Sercom SerialPort connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_serialConnect_Click(object sender, EventArgs e)
        {
            Sercom.serialConnect(this, e, bt_serialConnect, bt_send, bt_Clear_ConsoleText, sendMessage, comList, Baudrate, ComStatus, SERCOM_statusbar, ConsoleText);
        }

        /// <summary>
        /// Sends a message to the Sercom SerialPort.
        /// This button is only enabled if Sercom SerialPort is open.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_send_Click(object sender, EventArgs e)
        {
            Sercom.serial_Write(sendMessage.Text, Sent);
        }

        /// <summary>
        /// Sends a message if Sendmessage textbox is selected and Enter-key is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bt_send_Click(this, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Clears the ConsoleText window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Clear_ConsoleText_Click(object sender, EventArgs e)
        {
            ConsoleText.Clear();
        }

        /// <summary>
        /// Autoscrolls the Console text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsoleText_TextChanged(object sender, EventArgs e)
        {
            ConsoleText.SelectionStart = ConsoleText.Text.Length;
            ConsoleText.ScrollToCaret();
        }

        /// <summary>
        /// This function runs when the form is closed.
        /// Tells the Receiving thread to stop receiving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sercom.continue_receiving = false;
        }

    }

}