using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;


namespace SOTS.Items.ChestItems
{
	public class ShieldofStekpla : ModItem
	{ 	int critbonus = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shield of Stekpla");
			Tooltip.SetDefault("More is More\nGrants 1% bonus crit chance for every 2 full inventory slots");
		}
		public override void SetDefaults()
		{
            
            item.width = 34;     
            item.height = 32;     
            item.value = 50000;
            item.rare = 4;
			item.accessory = true;
			
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			critbonus = 0;
			for(int i = 0; i < 50; i++)
			{
			Item inventoryItem = player.inventory[i];
				if(inventoryItem.type != 0)
				{
					critbonus++;
					
					
				}
			}
			player.meleeCrit += (int)(critbonus * 0.5);
			player.rangedCrit += (int)(critbonus * 0.5);
			player.magicCrit += (int)(critbonus * 0.5);
			player.thrownCrit += (int)(critbonus * 0.5);
			
		}
	}
}
