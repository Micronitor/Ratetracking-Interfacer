using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Timers;
using System.Text.RegularExpressions;

namespace Ratetracking_Interfacer
{
    public partial class Form_main : Form
    {
        public Form_main()
        {
            InitializeComponent();

            //Initialize Sercom
            Sercom.SerialInitialize(this, new EventArgs(), comList, Baudrate);


            //Sercom.serialPort.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bt_serialConnect_Click(object sender, EventArgs e)
        {
            Sercom.serialConnect_Click(this, e, bt_serialConnect, bt_send, bt_Clear_ConsoleText, sendMessage, comList, Baudrate, ComStatus, SERCOM_statusbar, ConsoleText);
        }

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

        private void update_ComStatus(object sender, EventArgs e)
        {
            ComStatus.Text = "Port: " + Sercom.serialPort.PortName
            + "\nBaudrate: " + Sercom.serialPort.BaudRate.ToString()
            + "\nParity: " + Sercom.serialPort.Parity
            + "\nDataBits: " + Sercom.serialPort.DataBits.ToString()
            + "\nStopBits: " + Sercom.serialPort.StopBits
            + "\nHandshake: " + Sercom.serialPort.Handshake;
        }

        private void Baudrate_TextChanged(object sender, EventArgs e)
        {
            if (Baudrate.Text.Length != 0)
                Sercom.serialPort.BaudRate = Int32.Parse(Baudrate.Text);
            update_ComStatus(this, e);
        }

        private void bt_send_Click(object sender, EventArgs e)
        {
            Sercom.serial_Write(sendMessage.Text, Sent);
        }

        private void bt_Clear_ConsoleText_Click(object sender, EventArgs e)
        {
            ConsoleText.Clear();
        }

        private void Form_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sercom.continue_receiving = false;
        }
    }

}
