using GamerOSD.Data.Models;
using System.Timers;
using OpenHardwareMonitor.Hardware;
using System.Runtime.Intrinsics.Arm;

namespace GamerOSD.Data.Services
{
	public class GenericDeviceDataGrabber : IDeviceDataGrabber
	{
		public GenericDeviceDataGrabber()
		{

			thispc.Open();
			thispc.CPUEnabled = true;
            refreshTimer = new System.Timers.Timer(time_interval);
			refreshTimer.Elapsed += RefreshTimer_Elapsed;
			refreshTimer.Start();

			ThisPC = new DeviceData();
            
            thispc.Open();
            thispc.CPUEnabled = true;
            thispc.GPUEnabled = true;
            thispc.RAMEnabled = true;
            thispc.MainboardEnabled = true;
        }

		private void RefreshTimer_Elapsed(object? sender, ElapsedEventArgs e)
		{
			{	if (sender == null) return;
				var timer = (System.Timers.Timer)sender;
				timer?.Stop();
				SystemVisitor updateVisitor = new SystemVisitor();

				try
				{

                    Console.WriteLine("Fetching data...");
                    thispc.Accept(updateVisitor);
                    ThisPC.Clear();
					EvaulateHardware();
					
					NewDataCollected?.Invoke(this, new EventArgs());
				}
				catch(Exception ex)
				{
					Console.WriteLine("Error grabbing information");
				}
				finally
				{					
					timer?.Start();
				}

				
			};
		}

		#region Attributes
		private Computer thispc = new Computer();
		private System.Timers.Timer refreshTimer;
        private double time_interval = 500;
		#endregion

		#region Properties
		public DeviceData ThisPC { get; set; }
		#endregion


		public void SetDataCollectingInterval(double ms)
		{
            time_interval = ms;
		}

		private void EvaulateHardware()
		{
			for (int i = 0; i < thispc.Hardware.Length; i++)
			{
				switch (thispc.Hardware[i].HardwareType)
				{
					case HardwareType.CPU:
						EvaluateCPU(thispc.Hardware[i]);
						break;

                    case HardwareType.GpuNvidia:
                        EvaluateGpuNvidia(thispc.Hardware[i]);
                        break;

                    case HardwareType.GpuAti:
                        EvaluateGpuAti(thispc.Hardware[i]);
                        break;
                    case HardwareType.RAM:
                        EvaluateRAM(thispc.Hardware[i]);
                        break;

                }
			}
		}
        private void EvaluateCPU(IHardware cpu)
		{
            ThisPC.CpuCompleteName = cpu.Name;
            
            for (int j = 0; j < cpu.Sensors.Length; j++)
            {
                if (cpu.Sensors[j].SensorType == SensorType.Temperature && cpu.Sensors[j].Value.HasValue)
                {
                    ThisPC.CPUCoreTemperature.Add(cpu.Sensors[j].Value);

                }
                if (cpu.Sensors[j].SensorType == SensorType.Load && cpu.Sensors[j].Value.HasValue)
                {
                    ThisPC.CPUCorePercentLoad.Add(cpu.Sensors[j].Value);
                }
                if (cpu.Sensors[j].SensorType == SensorType.Load && cpu.Sensors[j].Name.Contains("Total") && cpu.Sensors[j].Value.HasValue)
                {
                    ThisPC.TotalCpuLoad = cpu.Sensors[j].Value;
                }
            }
        }
        private void EvaluateGpuNvidia(IHardware gpu)
        {
            ThisPC.GpuCompleteName = gpu.Name;
            for (int j = 0; j < gpu.Sensors.Length; j++)
            {
                if (gpu.Sensors[j].SensorType == SensorType.Temperature && gpu.Sensors[j].Value.HasValue)
                {
                    ThisPC.GPUTemperature.Add(gpu.Sensors[j].Value);
                }
                if (gpu.Sensors[j].SensorType == SensorType.Clock && gpu.Sensors[j].Value.HasValue)
                {
                    ThisPC.GPUClock.Add(gpu.Sensors[j].Value);
                }
                if (gpu.Sensors[j].SensorType == SensorType.Load && gpu.Sensors[j].Value.HasValue)
                {
                    ThisPC.GPULoad.Add(gpu.Sensors[j].Value);
                }
            }
        }
        private void EvaluateGpuAti(IHardware gpu)
        {
            ThisPC.GpuCompleteName = gpu.Name;
            
            for (int j = 0; j < gpu.Sensors.Length; j++)
            {
                if (gpu.Sensors[j].SensorType == SensorType.Temperature)
                {
                    ThisPC.GPUTemperature.Add(gpu.Sensors[j].Value);
                }
                if (gpu.Sensors[j].SensorType == SensorType.Clock)
                {
                    ThisPC.GPUClock.Add(gpu.Sensors[j].Value);
                }
                if (gpu.Sensors[j].SensorType == SensorType.Load)
                {
                    ThisPC.GPULoad.Add(gpu.Sensors[j].Value);
                }
            }
        }
        private void EvaluateRAM(IHardware ram)
        {
            for (int j = 0; j < ram.Sensors.Length; j++)
            {
                if (ram.Sensors[j].SensorType == SensorType.Load && ram.Sensors[j].Value.HasValue)
                {
                    ThisPC.RAMLoad.Add(ram.Sensors[j].Value);
                }
                if (ram.Sensors[j].SensorType == SensorType.Clock && ram.Sensors[j].Value.HasValue)
                {
                    ThisPC.RAMClock.Add(ram.Sensors[j].Value);
                }
                if (ram.Sensors[j].SensorType == SensorType.Temperature && ram.Sensors[j].Value.HasValue)
                {
                    ThisPC.RAMTemperature.Add(ram.Sensors[j].Value);
                }
            }
        }

        #region Events
        public event EventHandler? NewDataCollected;
		#endregion
	}

	public class SystemVisitor : IVisitor
	{
		public void VisitComputer(IComputer computer) { computer.Traverse(this); }

		public void VisitHardware(IHardware hardware)
		{
			hardware.Update();
			foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
		}

		public void VisitSensor(ISensor sensor) { }
		public void VisitParameter(IParameter parameter) { }
	}
}
