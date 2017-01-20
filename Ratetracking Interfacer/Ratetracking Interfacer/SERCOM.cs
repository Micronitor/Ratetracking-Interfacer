using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;


public class Sercom
{
public static SerialPort serialPort;
public static bool continue_receiving;

private Object serialLock = new Object();
private static Mutex serialMutex = new Mutex();
string serialData = " ";
//bool CanSend = true;

public static void SerialInitialize(object sender, EventArgs e,ListBox comList, TextBox Baudrate)
{
    serialPort = new SerialPort();
    serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None", true);
    serialPort.DataBits = 8;
    serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One", true);
    serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None", true);
    serialPort.ReadTimeout = 500;
    serialPort.WriteTimeout = 500;
    serialPort.BaudRate = int.Parse(Baudrate.Text);//227790; //455580 456.6
    serialPort.PortName = " ";
    serialPort.Close();
    foreach (string s in SerialPort.GetPortNames())
    {
        comList.Items.Add(s);
    }
    //update_ComStatus(this, new EventArgs());
}


public static void serialConnect_Click(object sender, EventArgs e, Button bt_serialConnect, Button bt_send, Button bt_Clear_ConsoleText, TextBox SendMessage, ListBox comList, TextBox Baudrate, RichTextBox ComStatus, ProgressBar SERCOM_statusbar, RichTextBox ConsoleText)
{
    if (serialPort.PortName.Equals(" "))
    {
        MessageBox.Show("Please select COM port");
        return;
    }
    bt_serialConnect.Enabled = false;
    StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
    if (!continue_receiving)
    {
        try
        {
            //Console.Clear();
            serialPort.Open();
            continue_receiving = true;
            //ReceiveWorker.RunWorkerAsync();
            bt_serialConnect.Text = "Disconnect";
                //statusStrip1.Text = "Connected";
                //SendWorker.RunWorkerAsync();
                //bt_Reload_Carrier_List.Enabled = true;
                SendMessage.Enabled = true;
                bt_send.Enabled = true;
                bt_Clear_ConsoleText.Enabled = true;
            comList.Enabled = false;
            Baudrate.Enabled = false;
            ComStatus.Enabled = false;
            var serial_Read_WorkerThread = new Thread(() => serial_Read_Worker(new object(), new EventArgs(), ConsoleText, SERCOM_statusbar));
            serial_Read_WorkerThread.Start();
            }
        catch (Exception ex)
        {
            if (ex is InvalidOperationException)
            {
                MessageBox.Show("BUSY!");
            }
            if (ex is System.IO.IOException)
            {
                MessageBox.Show("Invalid Connection Parameters.");
            }
            if (ex is System.UnauthorizedAccessException)
            {
                MessageBox.Show("Access to serial port denied.");
            }
            else
            {
                //TODO minor: IOExeption is thrown twice for some reason.
                //MessageBox.Show(ex.Message);
            }
        }
    }
    else
    {
        //serialPort.Close();
        continue_receiving = false;
        bt_serialConnect.Text = "Connect";
            //bt_Reload_Carrier_List.Enabled = false;
            //CarrierList.Enabled = false;
            //Carrier_Parameters_Enabled(false);
            SendMessage.Enabled = false;
            bt_send.Enabled = false;
            bt_Clear_ConsoleText.Enabled = false;
            comList.Enabled = true;
        Baudrate.Enabled = true;
        ComStatus.Enabled = true;
        SERCOM_statusbar.Value = 0;
            //statusStrip1.Text = "Disconnected";
            //backgroundWorker1.CancelAsync();

            SERCOM_statusbar.BeginInvoke((Action)(() => SERCOM_statusbar.Value = 0));
        }
    bt_serialConnect.Enabled = true;
}

    public static void serial_Read_Worker(object sender, EventArgs e, RichTextBox ConsoleText, ProgressBar SERCOM_statusbar)
    {
        SERCOM_statusbar.BeginInvoke((Action)(() => SERCOM_statusbar.Value = 100));
        while (continue_receiving)
        {
            try
            {
                string message = serialPort.ReadLine();
                //serialData = message.ToString();
                try
                {
                    ConsoleText.BeginInvoke((Action)(() => ConsoleText.AppendText(message.ToString())));
                    //Invoke(new Action(() => ConsoleText.Text = message.ToString()));
                    //parse(this, e);
                }
                catch
                {
                    //TODO
                }

            }
            catch (Exception ex)
            {
                if (ex is ArgumentOutOfRangeException)
                {
                    MessageBox.Show("ArgumentOutOfRangeException");
                }
                if (ex is TimeoutException)
                {
                    //CanSend = true;
                    //MessageBox.Show("TimeoutException!");
                }

                else
                {
                    throw;
                }
            }
        }
        serialPort.Close();

    }


    public static void serial_Write(string text, TextBox Sent)
{
    string txt = RemoveLineEndings(text);
    try
    {
        //set_Console_Text(txt + Environment.NewLine);
        serialPort.WriteLine(txt);
        Sent.AppendText(txt + "\t");
        //Give MCU time to respond
        Thread.Sleep(100);
    }
    catch (Exception ex)
    {
        if (ex is InvalidOperationException)
        {
            MessageBox.Show("No active connection");
        }
        if (ex is ArgumentOutOfRangeException)
        {
            MessageBox.Show("ArgumentOutOfRangeException");
        }
        //else
        //{
        //    throw;
        //}
    }

}

static string RemoveLineEndings(string text)
{
    StringBuilder newText = new StringBuilder();
    for (int i = 0; i < text.Length; i++)
    {
        if (!char.IsControl(text, i))
            newText.Append(text[i]);
    }
    return newText.ToString();
}

}
