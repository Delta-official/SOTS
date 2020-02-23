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


namespace SOTS.Items.Fragments
{
	public class BloodstainedCoin : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloodstained Coin");
			Tooltip.SetDefault("Critical strikes have a 50% chance to deal 30 more damage\nReceiving damage has a 50% chance to bleed you");
		}
		public override void SetDefaults()
		{
            item.width = 28;     
            item.height = 28;  
            item.value = Item.sellPrice(0, 0, 35, 0);
            item.rare = 2;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SOTSPlayer modPlayer = (SOTSPlayer)player.GetModPlayer(mod, "SOTSPlayer");	
			if(Main.rand.Next(2) == 0)
			{
				modPlayer.CritBonusDamage += 15;
				if(modPlayer.onhit == 1)
				{
					player.AddBuff(BuffID.Bleeding, 1020, false); //17 seconds
				}
			}
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Vertebrae, 5);
			recipe.AddIngredient(null, "Goblinsteel", 8);
			recipe.AddIngredient(null, "FragmentOfEvil", 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
