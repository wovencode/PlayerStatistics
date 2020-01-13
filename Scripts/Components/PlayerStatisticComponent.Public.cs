// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using wovencode;

namespace wovencode {
	
	// ===================================================================================
	// PlayerStatisticComponent
	// ===================================================================================
	public partial class PlayerStatisticComponent
	{
		
		// -------------------------------------------------------------------------------
		// TrackStatistic
		// -------------------------------------------------------------------------------
		[Server]
		public void TrackStatistic(string _name, long _value, string _category="")
		{
			
			for (int i = 0; i < syncData.Count; i++)
			{
				if (syncData[i].GetMatch(_name, _category))
				{
					StatisticSyncStruct entry = syncData[i];
					entry.value += _value;
					syncData[i] = entry;
					return;
				}
			}
			
			AddEntry(_name, _category, _value);
			
		}
		
		// -------------------------------------------------------------------------------
	
	}

}