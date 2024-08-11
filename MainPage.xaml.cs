using System.Management;

namespace InsightPC
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            LoadHardwareInfo();
        }

        private void LoadHardwareInfo()
        {
            HardwareInfoService hardwareInfo = new HardwareInfoService();

            // Processor Information
            ProcessorLabel.Text = hardwareInfo.GetProcessorInfo();
            CoresLabel.Text = hardwareInfo.GetProcessorCores();
            ClockSpeedLabel.Text = hardwareInfo.GetProcessorClockSpeed();
        }
    }

    public class HardwareInfoService
    {
        public string GetProcessorInfo()
        {
            try
            {
                string processorInfo = string.Empty;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor");
                foreach (ManagementObject obj in searcher.Get())
                {
                    processorInfo = obj["Name"].ToString();
                }
                Console.WriteLine($"Processor: {processorInfo}");
                return processorInfo;
            }
            catch (Exception ex)
            {
                // Log or display the exception
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public string GetProcessorCores()
        {
            string cores = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select NumberOfCores from Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                cores = obj["NumberOfCores"].ToString();
            }
            return cores;
        }

        public string GetProcessorClockSpeed()
        {
            string clockSpeed = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select MaxClockSpeed from Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                clockSpeed = obj["MaxClockSpeed"].ToString() + " MHz";
            }
            return clockSpeed;
        }
    }

}
