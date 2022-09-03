namespace GamerOSD.Data.Models
{
	public class DeviceData
	{

		public DeviceData()
		{
			CPUCoreTemperature = new List<float?>();
			CPUCoreClock = 0f;
			CPUCorePercentLoad = new List<float?>();
            
			GPUClock = new List<float?>();
            GPULoad = new List<float?>();
            GPUTemperature = new List<float?>();
            
            RAMTemperature = new List<float?>();
            RAMLoad = new List<float?>();
            RAMClock = new List<float?>();
            
            TotalCpuLoad = 0;

        }

		//CPU
		public List<float?> CPUCoreTemperature;
		public float? CPUCoreClock;
		public List<float?> CPUCorePercentLoad;
        public float? TotalCpuLoad = 0;
        
        public string? CpuCompleteName = null;

        //GPU
        public List<float?> GPUTemperature;
        public List<float?> GPUClock;
        public List<float?> GPULoad;
        
        public string? GpuCompleteName = null;

        //RAM
        public List<float?> RAMTemperature;
        public List<float?> RAMClock;
        public List<float?> RAMLoad;

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
