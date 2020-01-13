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
	// PlayerStatisticManager
	// ===================================================================================
	public partial class PlayerStatisticManager
	{
		
		// -------------------------------------------------------------------------------
		// TrackStatistic
		// -------------------------------------------------------------------------------
		[Server]
		public void TrackStatistic(string _name, long _value, string _category="")
		{
			
			for (int i = 0; i < syncStatistics.Count; i++)
			{
				if (syncStatistics[i].GetMatch(_name, _category))
				{
					StatisticSyncStruct entry = syncStatistics[i];
					entry.value += _value;
					syncStatistics[i] = entry;
					return;
				}
			}
			
			AddEntry(_name, _category, _value);
			
		}
		
		// -------------------------------------------------------------------------------
	
	}

}