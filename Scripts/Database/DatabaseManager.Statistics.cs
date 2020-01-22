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
using Wovencode;
using Wovencode.Database;

namespace Wovencode.Database
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
	   	void Init_Statistics()
	   	{
	   		CreateTable<TablePlayerStatistics>();
        	CreateIndex(nameof(TablePlayerStatistics), new []{"owner", "name"});
	   	}
	   
	   	// -------------------------------------------------------------------------------
	   	// CreateDefaultDataPlayer_Statistics
	   	// -------------------------------------------------------------------------------
	   	[DevExtMethods("CreateDefaultDataPlayer")]
	   	void CreateDefaultDataPlayer_Statistics(GameObject player)
	   	{
	   		/*
	   			there are no statistics by default, they are added while players
	   			progress through the game
	   		*/
	   	}
	   	
	   	// -------------------------------------------------------------------------------
	   	// LoadDataPlayer_Statistics
		// -------------------------------------------------------------------------------
		[DevExtMethods("LoadDataPlayer")]
		void LoadDataPlayer_Statistics(GameObject player)
		{
	   		PlayerStatisticComponent manager = player.GetComponent<PlayerStatisticComponent>();
	   		
	   		foreach (TablePlayerStatistics row in Query<TablePlayerStatistics>("SELECT * FROM "+nameof(TablePlayerStatistics)+" WHERE owner=?", player.name))
				manager.AddEntry(row.name, row.category, row.value);
			
		}
	   	
	   	// -------------------------------------------------------------------------------
	   	// SaveDataPlayer_Statistics
		// -------------------------------------------------------------------------------
		[DevExtMethods("SaveDataPlayer")]
		void SaveDataPlayer_Statistics(GameObject player, bool isOnline)
		{
	   		
	   		// you should delete all data of this player first, to prevent duplicates
	   		DeleteDataPlayer_Statistics(player.name);
	   		
	   		PlayerStatisticComponent manager = player.GetComponent<PlayerStatisticComponent>();
	   		
	   		foreach (StatisticSyncStruct entry in manager.GetEntries(false))
	   		{
	   			InsertOrReplace(new TablePlayerStatistics{
                	owner 			= player.name,
                	name 			= entry.name,
                	category 		= entry.category,
                	value 			= entry.value
            	});
	   		}
		}
	   	
	   	// -------------------------------------------------------------------------------
	   	// DeleteDataPlayer_Statistics
	   	// -------------------------------------------------------------------------------
	   	[DevExtMethods("DeleteDataPlayer")]
	   	void DeleteDataPlayer_Statistics(string _name)
	   	{
	   		Execute("DELETE FROM "+nameof(TablePlayerStatistics)+" WHERE owner=?", _name);
	   	}
	   	
		// -------------------------------------------------------------------------------
		
	}

}

// =======================================================================================