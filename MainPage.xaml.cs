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

        private void OnProcessorInfoTapped(object sender, EventArgs e)
        {
            ProcessorInfoGrid.IsVisible = !ProcessorInfoGrid.IsVisible;
        }

        private void OnRamInfoTapped(object sender, EventArgs e)
        {
            RAMInfoGrid.IsVisible = !RAMInfoGrid.IsVisible;
        }

        private void OnDiskInfoTapped(object sender, EventArgs e)
        {
            DiskInfoGrid.IsVisible = !DiskInfoGrid.IsVisible;
        }

        private void OnGpuInfoTapped(object sender, EventArgs e)
        {
            GPUInfoGrid.IsVisible = !GPUInfoGrid.IsVisible;
        }

        private void OnNetworkInfoTapped(object sender, EventArgs e)
        {
            NetworkInfoGrid.IsVisible = !NetworkInfoGrid.IsVisible;
        }

        private void OnMotherboardInfoTapped(object sender, EventArgs e)
        {
            MotherboardInfoGrid.IsVisible = !MotherboardInfoGrid.IsVisible;
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

            // Motherboard Information
            MotherboardLabel.Text = hardwareInfo.GetMotherboardInfo();

            // GPU Information
            GPULabel.Text = hardwareInfo.GetGPUInfo();

            // Operating System Information
            //OSLabel.Text = hardwareInfo.GetOSInfo();

            // Network Adapter Information
            NetworkLabel.Text = hardwareInfo.GetNetworkAdapterInfo();

            // BIOS Information
            //BIOSLabel.Text = hardwareInfo.GetBIOSInfo();
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
        public string GetMotherboardInfo()
        {
            string motherboardInfo = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Manufacturer, Product, SerialNumber from Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                motherboardInfo = $"Manufacturer: {obj["Manufacturer"]}\nModel: {obj["Product"]}\nSerial Number: {obj["SerialNumber"]}";
            }
            return motherboardInfo;
        }

        public string GetGPUInfo()
        {
            string gpuInfo = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Name, DriverVersion, AdapterRAM from Win32_VideoController");
            foreach (ManagementObject obj in searcher.Get())
            {
                gpuInfo = $"Name: {obj["Name"]}\nDriver Version: {obj["DriverVersion"]}\nVideo Memory: {Convert.ToInt64(obj["AdapterRAM"]) / 1024 / 1024} MB";
            }
            return gpuInfo;
        }

        public string GetOSInfo()
        {
            string osInfo = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Caption, Version, OSArchitecture from Win32_OperatingSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                osInfo = $"Name: {obj["Caption"]}\nVersion: {obj["Version"]}\nArchitecture: {obj["OSArchitecture"]}";
            }
            return osInfo;
        }

        public string GetNetworkAdapterInfo()
        {
            string adapterInfo = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Name, MACAddress, Speed from Win32_NetworkAdapter where MACAddress is not null");
            foreach (ManagementObject obj in searcher.Get())
            {
                adapterInfo += $"Name: {obj["Name"]}\nMAC Address: {obj["MACAddress"]}\nSpeed: {Convert.ToInt64(obj["Speed"]) / 1000000} Mbps\n\n";
            }
            return adapterInfo;
        }

        public string GetBIOSInfo()
        {
            string biosInfo = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Manufacturer, SMBIOSBIOSVersion, ReleaseDate from Win32_BIOS");
            foreach (ManagementObject obj in searcher.Get())
            {
                biosInfo = $"Manufacturer: {obj["Manufacturer"]}\nVersion: {obj["SMBIOSBIOSVersion"]}\nRelease Date: {ManagementDateTimeConverter.ToDateTime(obj["ReleaseDate"].ToString())}";
            }
            return biosInfo;
        }

    }

}
