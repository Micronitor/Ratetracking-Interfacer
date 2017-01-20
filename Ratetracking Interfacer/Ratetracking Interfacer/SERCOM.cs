using System;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;


public class Sercom
{
    /// <summary>
    /// Public serialPort.
    /// TODO minor: Consider making private. Public so that formclosing can signal read worker thread to exit.
    /// </summary>
    public static SerialPort serialPort;

    /// <summary>
    /// Condition variable for the Receive Worker to continue receiving.
    /// </summary>
    public static bool continue_receiving;

    /// <summary>
    /// Sets standard parameters for the SerialPort.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="comList"> 
    /// ListBox to display available COMPORTS to. 
    /// </param>
    /// <param name="Baudrate"> 
    /// Loads the boadrate from a TextBox. 
    /// </param>
    public static void SerialInitialize(object sender, EventArgs e, ListBox comList, TextBox Baudrate)
    {
        serialPort = new SerialPort();
        serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None", true);
        serialPort.DataBits = 8;
        serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One", true);
        serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None", true);
        serialPort.ReadTimeout = 500;
        serialPort.WriteTimeout = 500;
        serialPort.BaudRate = int.Parse(Baudrate.Text);
        serialPort.PortName = " ";
        serialPort.Close();
        foreach (string s in SerialPort.GetPortNames())
        {
            comList.Items.Add(s);
        }
    }


    /// <summary>
    /// Connects and disconnects the SerialPort with one butten.
    /// Enables and disables SerialPort buttons when connecting and disconnecting.
    /// This either starts a serial read worker, or signals serial read worker to stop.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="bt_serialConnect">
    /// This button name is toggled between "Connect" and "Disconnect"
    /// It is this button that calls this function on press.
    /// </param>
    /// <param name="bt_send">
    /// Enabled/Disabled.
    /// </param>
    /// <param name="bt_Clear_ConsoleText">
    /// Enabled/Disabled.
    /// </param>
    /// <param name="SendMessage">
    /// Enabled/Disabled.
    /// </param>
    /// <param name="comList">
    /// Enabled/Disabled.
    /// </param>
    /// <param name="Baudrate">
    /// Enabled/Disabled.
    /// </param>
    /// <param name="ComStatus">
    /// Enabled/Disabled.
    /// </param>
    /// <param name="SERCOM_statusbar">
    /// Visualize the active connection.
    /// Green when connected, grey when disconnected.
    /// </param>
    /// <param name="ConsoleText">
    /// Enabled/Disabled.
    /// </param>
    public static void serialConnect(object sender, EventArgs e, Button bt_serialConnect, Button bt_send, Button bt_Clear_ConsoleText, TextBox SendMessage, ListBox comList, TextBox Baudrate, RichTextBox ComStatus, ProgressBar SERCOM_statusbar, RichTextBox ConsoleText)
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
                serialPort.Open();
                continue_receiving = true;
                bt_serialConnect.Text = "Disconnect";
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
            continue_receiving = false;
            bt_serialConnect.Text = "Connect";
            SendMessage.Enabled = false;
            bt_send.Enabled = false;
            bt_Clear_ConsoleText.Enabled = false;
            comList.Enabled = true;
            Baudrate.Enabled = true;
            ComStatus.Enabled = true;
            SERCOM_statusbar.Value = 0;
        }
        bt_serialConnect.Enabled = true;
    }

    /// <summary>
    /// Serial read worker thread.
    /// Runs until continue_receiving is false.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="ConsoleText">
    /// RichTextBox to display serial data to.
    /// </param>
    /// <param name="SERCOM_statusbar">
    /// Visualize the active connection.
    /// Green when connected, grey when disconnected.
    /// </param>
    public static void serial_Read_Worker(object sender, EventArgs e, RichTextBox ConsoleText, ProgressBar SERCOM_statusbar)
    {
        SERCOM_statusbar.BeginInvoke((Action)(() => SERCOM_statusbar.Value = 100));
        while (continue_receiving)
        {
            try
            {
                string message = serialPort.ReadLine();
                try
                {
                    ConsoleText.BeginInvoke((Action)(() => ConsoleText.AppendText(message.ToString())));
                }
                catch
                {
                    //TODO minor:
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
                    //TODO minor:
                }
                else
                {
                    throw;
                }
            }
        }
        serialPort.Close();

    }


    /// <summary>
    /// Writes data to the SerialPort.
    /// Displays previous sent data to TextBox.
    /// 
    /// NOTE: Control characters is removed before text is sent.
    /// 
    /// </summary>
    /// <param name="text">
    /// String to send on SerialPort.
    /// </param>
    /// <param name="Sent">
    /// TextBox to display sent data.
    /// </param>
    public static void serial_Write(string text, TextBox Sent)
    {
        string txt = RemoveLineEndings(text);
        try
        {
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
        }

    }

    /// <summary>
    /// Helper function that removes control characters.
    /// </summary>
    /// <param name="text">
    /// Input string.
    /// </param>
    /// <returns>
    /// Input string without controll characters.
    /// </returns>
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