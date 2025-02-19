namespace GamerOSD.Data.Models
{
	public class DeviceData
	{

		public DeviceData()
		{
			CPUCoreTemperature = new List<float?>();
			CPUCoreClock = 0f;
			CPUCorePercentLoad = new List<float?>();
            
            TotalCpuLoad = 0;

        }

		//CPU
		public List<float?> CPUCoreTemperature;
		public float? CPUCoreClock;
		public List<float?> CPUCorePercentLoad;
        public float? TotalCpuLoad = 0;
        public float? CpuPackageTemperature;

        public string? CpuCompleteName = null;

        //GPU
        public List<double?> GPUTemperature = new List<double?>();
        public List<double?> GPUClock = new List<double?>();
        public List<double?> GPULoad = new List<double?>();
        
        public string? GpuCompleteName = null;

        //RAM
        public List<double?> RAMTemperature = new List<double?>();
        public List<double?> RAMClock = new List<double?>();
        public List<double?> RAMLoad = new List<double?>();

        public void Clear()
        {
            CPUCoreTemperature.Clear();
            CPUCoreClock = 0f;
            CPUCorePercentLoad.Clear();
            TotalCpuLoad = 0;
            CpuCompleteName = null;
            GPUTemperature.Clear();
            GPUClock.Clear();
            GPULoad.Clear();
            GpuCompleteName = null;
            RAMTemperature.Clear();
            RAMClock.Clear();
            RAMLoad.Clear();
            RAMTemperature.Clear();
        }
        
		
		public double V12;
		public double V5;
		public double V3_3;
	}
}
