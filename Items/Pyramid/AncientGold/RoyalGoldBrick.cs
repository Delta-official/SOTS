using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SOTS.Items.Pyramid.AncientGold
{
	public class RoyalGoldBrick : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Gold Brick");
			Tooltip.SetDefault("'You can feel the regalness'");
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.rare = ItemRarityID.LightRed;
			item.consumable = true;
			item.createTile = mod.TileType("RoyalGoldBrickTile");
		}
	}
	public class RoyalGoldBrickTile : ModTile
	{
		public override void SetDefaults()
		{
			TileID.Sets.DrawsWalls[Type] = true;
			Main.tileBrick[Type] = false;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			drop = ModContent.ItemType<RoyalGoldBrick>();
			AddMapEntry(new Color(180, 150, 20));
			mineResist = 1.0f;
			minPick = 0;
			soundType = SoundID.Tink;
			soundStyle = 2;
			dustType = DustID.GoldCoin;
			TileID.Sets.GemsparkFramingTypes[Type] = Type;
		}
		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
		{
			Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
			return false;
		}
	}
}