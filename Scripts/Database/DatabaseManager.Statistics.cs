// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using UnityEngine;
using Mirror;
using System;
using System.IO;
using System.Collections.Generic;
using SQLite;
using UnityEngine.AI;
using wovencode;

namespace wovencode
{
	
	// ===================================================================================
	// DatabaseManager
	// ===================================================================================
	public partial class DatabaseManager
	{
	
		// -------------------------------------------------------------------------------
		// Init_Statistics
	   	// -------------------------------------------------------------------------------
	   	[DevExtMethods("Init")]
	   	public void Init_Statistics()
	   	{
	   		CreateTable<TableStatistics>();
        	CreateIndex(nameof(TableStatistics), new []{"owner", "name"});
	   	}
	   
	   	// -------------------------------------------------------------------------------
	   	// CreateDefaultData_Statistics
	   	// -------------------------------------------------------------------------------
	   	[DevExtMethods("CreateDefaultData")]
	   	public void CreateDefaultData_Statistics(GameObject player)
	   	{
	   		/*
	   			there are no statistics by default, they are added while players
	   			progress through the game
	   		*/
	   	}
	   	
	   	// -------------------------------------------------------------------------------
	   	// LoadData_Statistics
		// -------------------------------------------------------------------------------
		[DevExtMethods("LoadData")]
		public void LoadData_Statistics(GameObject player)
		{
	   		StatisticManager manager = player.GetComponent<StatisticManager>();
	   		
	   		foreach (TableStatistics row in Query<TableStatistics>("SELECT * FROM TableStatistics WHERE owner=?", player.name))
				manager.AddEntry(row.name, row.category, row.value);
			
		}
	   	
	   	// -------------------------------------------------------------------------------
	   	// SaveData_Statistics
		// -------------------------------------------------------------------------------
		[DevExtMethods("SaveData")]
		public void SaveData_Statistics(GameObject player)
		{
	   		
	   		// you should delete all data of this player first, to prevent duplicates
	   		DeleteData_Statistics(player.name);
	   		
	   		StatisticManager manager = player.GetComponent<StatisticManager>();
	   		
	   		foreach (StatisticSyncStruct entry in manager.GetEntries(false))
	   		{
	   			InsertOrReplace(new TableStatistics{
                	owner 			= player.name,
                	name 			= entry.name,
                	category 		= entry.category,
                	value 			= entry.value
            	});
	   		}
		}
	   	
	   	// -------------------------------------------------------------------------------
	   	// DeleteData_Statistics
	   	// -------------------------------------------------------------------------------
	   	[DevExtMethods("DeleteData")]
	   	void DeleteData_Statistics(string _name)
	   	{
	   		Execute("DELETE FROM TableStatistics WHERE owner=?", _name);
	   	}
	   	
		// -------------------------------------------------------------------------------
		
	}

}

// =======================================================================================