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
using Wovencode;

namespace Wovencode {
	
	// ===================================================================================
	// StatisticSyncStruct
	// ===================================================================================
	[System.Serializable]
	public partial struct StatisticSyncStruct
	{
    	
  		public string 	name;
  		public string	category;
 		public long		value;
 		
 		// -------------------------------------------------------------------------------
		// StatisticSyncStruct (Constructor)
		// -------------------------------------------------------------------------------
		public StatisticSyncStruct(string _name, string _category, long _value)
		{
			name 		= _name;
			category	= _category;
			value 		= Math.Max(0,_value);
		}
		
		// -------------------------------------------------------------------------------
		// Valid
		// In case of statistics, valid means the name is set and the amount is not zero
		// -------------------------------------------------------------------------------
    	public bool Valid
    	{
    		get { return !String.IsNullOrWhiteSpace(name) && value != 0; }
    	}
		
		// -------------------------------------------------------------------------------
		// GetMatch
		// -------------------------------------------------------------------------------
		public bool GetMatch(string _name, string _category)
		{
			return (name == _name && (category == _category || String.IsNullOrWhiteSpace(_category)) );
		}
    
    }
	
	public class SyncListStatisticSyncStruct : SyncList<StatisticSyncStruct> { }
	
}

// =======================================================================================