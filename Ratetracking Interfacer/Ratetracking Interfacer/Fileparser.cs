using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratetracking_Interfacer
{
    class Fileparser
    {
        public StreamWriter Logg_Target;

        private StreamWriter initializeLoggFile(string CarrierAddress, string CarrierSensorData)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            FileStream fs;
            StreamWriter file;
            string startUpPath;
            string currentLogFileName;
            string logfolder;
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            //TODO Check if file and folders excists
            currentLogFileName = string.Format("{0:yyyy-MM-dd-HH-mm-ss}", DateTime.Now) + "Carrier@" + CarrierAddress + CarrierSensorData + ".txt";// There are following custom format specifiers y (year), M (month), d (day), h (hour 12), H (hour 24), m (minute), s (second), f (second fraction), F (second fraction, trailing zeroes are trimmed), t (P.M or A.M) and z (time zone).
            startUpPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "log");
            logfolder = Path.Combine(startUpPath, currentLogFileName);
            fs = File.Create(logfolder);
            file = new StreamWriter(fs);
            return file;
        }

        public static void ReadSDCard(TextBox Sent)
        {
            try
            {
                Sercom.serial_Write("MM23", Sent);
                Sercom.serial_Write("CW", Sent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
