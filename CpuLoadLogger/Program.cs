using LibreHardwareMonitor.Hardware;
using System.Diagnostics;
using System.IO;
using System.Management;


LogTemp();
LogLoad();
LogSum();

//C:\Users\User\Desktop\temp






void LogSum() 
{
    File.WriteAllText("C:\\Users\\User\\Desktop\\temp\\test.txt", $"Load: {SumLoad()}\nTemp: {SumTemp()}");

}

double SumLoad() 
{
    DirectoryInfo directory = new DirectoryInfo("C:\\Users\\User\\Desktop\\temp\\Load");
    double sum = 0;
    int index = 0;
    if (directory.GetFiles().Count() == 0) sum = 0;
    else
    {
        foreach (var file in directory.GetFiles())
        {
            try
            {
                index++;
                sum += Convert.ToDouble(File.ReadAllText(file.FullName));
            }
            catch { }
        }
    }
    return sum / index;
}
double SumTemp()
{
    DirectoryInfo directory = new DirectoryInfo("C:\\Users\\User\\Desktop\\Temp");
    double sum = 0;
    int index = 0;
    if (directory.GetFiles().Count() == 0) sum = 0;
    else
    {
        foreach (var file in directory.GetFiles())
        {
            try
            {
                index++;
                sum += Convert.ToDouble(File.ReadAllText(file.FullName));
            }
            catch { }
        }
    }
    return sum / index;
}




void LogLoad() 
{
    DirectoryInfo directory = new("C:\\Users\\User\\Desktop\\temp\\Load");

    int index = 0;
    if (directory.GetFiles().Count() == 0) index = 0;
    else
    {
        int max = 0;
        foreach (var file in directory.GetFiles())
        {
            try
            {
                if (Convert.ToInt32(GetName(file)) > max) max = Convert.ToInt32(GetName(file));
            }
            catch { }
        }
        index = max + 1;
    }

    File.WriteAllText(directory.FullName + $"/{index}.txt", CPU_Load().ToString());
}


void LogTemp()
{
    DirectoryInfo directory = new("C:\\Users\\User\\Desktop\\Temp");

    int index = 0;
    if (directory.GetFiles().Count() == 0) index = 0;
    else
    {
        int max = 0;
        foreach (var file in directory.GetFiles())
        {
            try
            {
                if (Convert.ToInt32(GetName(file)) > max) max = Convert.ToInt32(GetName(file));
            }
            catch { }
        }
        index = max + 1;
    }

    File.WriteAllText(directory.FullName + $"/{index}.txt", CPU_Temp().ToString());
}



static double CPU_Load()
{
    Computer computer = new Computer() { IsCpuEnabled = true };
    computer.Open();
    foreach (var hardware in computer.Hardware)
    {
        if (hardware.HardwareType == HardwareType.Cpu)
        {
            hardware.Update();
            foreach (var sensor in hardware.Sensors)
            {
                if (sensor.SensorType == SensorType.Load)
                {
                    return sensor.Value ?? 0;
                }
            }
        }
    }
    return 0;
}
double CPU_Temp() 
{
    Computer computer = new Computer() {IsCpuEnabled = true };
    computer.Open();
    foreach (var hardware in computer.Hardware)
    {
        if (hardware.HardwareType == HardwareType.Cpu)
        {
            hardware.Update();
            foreach (var sensor in hardware.Sensors)
            {
                if (sensor.SensorType == SensorType.Temperature)
                {
                    return sensor.Value ?? 0;
                }
            }
        }
    }
    return 0;
}
string GetName(FileInfo file)
{
    string result = string.Empty;
    foreach (var c in file.Name)
    {
        if (c != '.') result += c;
        else break;
    }
    return result;
}