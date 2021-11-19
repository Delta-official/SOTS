using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Items.Celestial
{
	public class CelestialBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}
		public override void SetDefaults()
		{
			item.width = 44;
			item.height = 34;
			item.value = 0;
			item.rare = 6;
			item.expert = true;
			item.maxStack = 999;
			item.consumable = true;
		}
		public override int BossBagNPC => mod.NPCType("CelestialSerpentHead");
		public override bool CanRightClick()
		{
			return true;
		}
		public override void OpenBossBag(Player player)
		{

			player.QuickSpawnItem(mod.ItemType("AngelicCatalyst"));
			player.QuickSpawnItem(mod.ItemType("StarShard") ,Main.rand.Next(16, 30));
			
			if(Main.rand.Next(10) == 0)
				player.QuickSpawnItem(mod.ItemType("StrangeFruit"), 1);
			/*	
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("GelWings"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodParasite"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodHelix"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodCrystal"),Main.rand.Next(200, 500));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodHook"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodCollapse"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodScepter"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodStaff"));
		
			if(Main.rand.Next(12) == 0)
			player.QuickSpawnItem(mod.ItemType("WormWoodSpike"));
			*/
		}
	}
}
