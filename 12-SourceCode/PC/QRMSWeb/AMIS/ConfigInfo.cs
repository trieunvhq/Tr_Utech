using HDLIB;
using HDLIB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS
{
    public class ConfigInfo
    {
		private const string transferStatusName = "TRANSFER_DATA_STATUS";
		/*
		public ConfigInfo()
		{

		}

		public string GET_ASSIGN_WORK_WS_START()
		{
			try
			{
				BPL.Masters.ConfigBPL configBPL = new BPL.Masters.ConfigBPL();
				string cvalue = configBPL.GetConfig("ASSIGN_WORK_WS_START");
				return cvalue;
			}
			catch (Exception ex)
			{
				Logging.LogError(ex);
				return null;
			}
		}

		public string GET_ASSIGN_WORK_WS_TIMER()
		{
			try
			{
				BPL.Masters.ConfigBPL configBPL = new BPL.Masters.ConfigBPL();
				string cvalue = configBPL.GetConfig("ASSIGN_WORK_WS_TIMER");
				return cvalue;
			}
			catch (Exception ex)
			{
				Logging.LogError(ex);
				return null;
			}
		}

		public string GET_STATUS_TRANSFER_DATA()
		{
			try
			{
				BPL.Masters.ConfigBPL configBPL = new BPL.Masters.ConfigBPL();
				string cvalue = configBPL.GetConfig(transferStatusName);
				return cvalue;
			}
			catch (Exception ex)
			{
				Logging.LogError(ex);
				return null;
			}
		}

		public bool SET_STATUS_TRANSFER_DATA(int status)
		{
			try
			{
				BPL.Masters.ConfigBPL configBPL = new BPL.Masters.ConfigBPL();
				return configBPL.SetStatusTransfer(transferStatusName, status);
			}
			catch (Exception ex)
			{
				Logging.LogError(ex);
				return false;
			}
		}
		*/
	}
}
