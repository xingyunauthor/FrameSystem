using System.Management;

namespace Frame.Utils
{
    public class SystemInfo
    {
        public static string GetCpuId()
        {
            try
            {
                var cpuId = "";//cpu序列号 
                var mc = new ManagementClass("Win32_Processor");
                var moc = mc.GetInstances();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    cpuId = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc.Dispose();
                mc.Dispose();
                return cpuId;
            }
            catch
            {
                return "";
            }
        }
    }
}
