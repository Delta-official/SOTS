using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SOTS.Items.Secrets.IceCream
{
	public class IceCreamPlatformTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CoordinateHeights = new[]{ 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 27;
			TileObjectData.newTile.StyleWrapLimit = 27;
			TileObjectData.newTile.UsesCustomCanPlace = false;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(19);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			AddMapEntry(new Color(255, 180, 188));
			dustType = 72;
			drop = mod.ItemType("IceCreamPlatform");
			adjTiles = new int[]{ TileID.Platforms };
			TileID.Sets.Platforms[Type] = true;
		}

		public override void PostSetDefaults()
		{
			Main.tileNoSunLight[Type] = false;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}