using System.Management;

namespace InsightPC
{
    public partial class MainPage : ContentPage
    {
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

            RAMLabel.Text = hardwareInfo.GetRAMInfo();
            RAMSpeedLabel.Text = hardwareInfo.GetMemorySpeed();
            RAMManufacturerLabel.Text = hardwareInfo.GetMemoryManufacturer();

            // Disk Information
            DiskLabel.Text = hardwareInfo.GetDiskInfo();
            DiskInterfaceLabel.Text = hardwareInfo.GetDiskInterfaceType();
            DiskPartitionsLabel.Text = hardwareInfo.GetDiskPartitions();
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

        public string GetRAMInfo()
        {
            string ramInfo = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            foreach (ManagementObject obj in searcher.Get())
            {
                ramInfo += $"{Convert.ToInt64(obj["Capacity"]) / 1024 / 1024} MB\n";
            }
            return ramInfo;
        }

        public string GetMemorySpeed()
        {
            string memorySpeed = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Speed from Win32_PhysicalMemory");
            foreach (ManagementObject obj in searcher.Get())
            {
                memorySpeed += obj["Speed"].ToString() + " MHz\n";
            }
            return memorySpeed;
        }

        public string GetMemoryManufacturer()
        {
            string manufacturer = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Manufacturer from Win32_PhysicalMemory");
            foreach (ManagementObject obj in searcher.Get())
            {
                manufacturer += obj["Manufacturer"].ToString() + "\n";
            }
            return manufacturer;
        }

        public string GetDiskInfo()
        {
            string diskInfo = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive");
            foreach (ManagementObject obj in searcher.Get())
            {
                diskInfo += $"{obj["Model"]}, {obj["Size"]} bytes\n";
            }
            return diskInfo;
        }

        public string GetDiskInterfaceType()
        {
            string interfaceType = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select InterfaceType from Win32_DiskDrive");
            foreach (ManagementObject obj in searcher.Get())
            {
                interfaceType += obj["InterfaceType"].ToString() + "\n";
            }
            return interfaceType;
        }

        public string GetDiskPartitions()
        {
            string partitions = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Partitions from Win32_DiskDrive");
            foreach (ManagementObject obj in searcher.Get())
            {
                partitions += obj["Partitions"].ToString() + "\n";
            }
            return partitions;
        }

    }

}
