using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Items.Fragments
{
	public class BorealisIcosahedron : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Borealis Icosahedron");
			Tooltip.SetDefault("Critical strikes may frostburn enemies");
		}
		public override void SetDefaults()
		{
            item.width = 26;     
            item.height = 26;  
            item.value = Item.sellPrice(0, 1, 50, 0);
            item.rare = 7;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SOTSPlayer modPlayer = (SOTSPlayer)player.GetModPlayer(mod, "SOTSPlayer");	
			modPlayer.CritFrost = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DissolvingAurora", 1);
			recipe.AddIngredient(ItemID.FrostCore, 1);
			recipe.AddIngredient(null, "AbsoluteBar", 6);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
