using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Items.Pyramid
{
	public class AcediaPlating : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acedia Portal Plating");
			Tooltip.SetDefault("'It bares striking resemblance to luminite'");
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
			item.rare = ItemRarityID.Cyan;
			item.consumable = true;
			item.createTile = mod.TileType("AcediaPlatingTile");
		}
	}
	public class AcediaPlatingTile : ModTile
	{
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.2f;
			g = 0.1f;
			b = 0.2f;
			base.ModifyLight(i, j, ref r, ref g, ref b);
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			//float uniquenessCounter = Main.GlobalTime * -100 + (i + j) * 5;
			Tile tile = Main.tile[i, j];
			Texture2D texture = mod.GetTexture("Items/Pyramid/AcediaPlatingTileGlow");
			Rectangle frame = new Rectangle(tile.frameX, tile.frameY, 16, 16);
			Color color;
			color = WorldGen.paintColor((int)Main.tile[i, j].color()) * (100f / 255f);
			color.A = 0;
			float alphaMult = 0.1f; // + 0.45f * (float)Math.Sin(MathHelper.ToRadians(uniquenessCounter));
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			for (int k = 0; k < 2; k++)
			{
				Vector2 pos = new Vector2((i * 16 - (int)Main.screenPosition.X), (j * 16 - (int)Main.screenPosition.Y)) + zero;
				Vector2 offset = new Vector2(Main.rand.NextFloat(-1, 1f), Main.rand.NextFloat(-1, 1f)) * 0.1f * k;
				Main.spriteBatch.Draw(texture, pos + offset, frame, color * alphaMult * 1f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
		}
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			//Main.tileBrick[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			drop = mod.ItemType("AcediaPlating");
			AddMapEntry(new Color(44, 12, 62));
			mineResist = 2f;
			minPick = 250;
			soundType = 21;
			soundStyle = 2;
			dustType = mod.DustType("AcedianDust");
			TileID.Sets.GemsparkFramingTypes[Type] = Type;
		}
		public override bool CanExplode(int i, int j)
		{
			return false;
		}
		public override bool Slope(int i, int j)
		{
			return false;
		}
		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
		{
			Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
			return false;
		}
	}
}