using OpenHardwareMonitor.Hardware;
	
namespace GamerOSD.Data.Services
{
	public interface IDeviceDataGrabber
	{
		public void SetDataCollectingInterval(double ms);

		public event EventHandler NewDataCollected;
	}
}
