using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOTS.Items.Pyramid;
using SOTS.NPCs.Boss.Curse;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SOTS.Projectiles.Pyramid
{    
    public class CurseArm : ModProjectile 
    {	          
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Curse");
		}
        public override void SetDefaults()
        {
			projectile.height = 24;
			projectile.width = 24;
			projectile.friendly = false;
			projectile.timeLeft = 480;
			projectile.hostile = true;
			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.netImportant = true;
			projectile.ai[0] = -1f;
			projectile.tileCollide = false;
		}
		Vector2 OwnerPos;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
			width = 4;
			height = 4;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			projectile.velocity *= 0.0f;
            return false;
        }
        public bool DrawLimbs(List<CurseFoam> dustList, Rectangle targetHitbox)
		{
			int parentID = (int)projectile.ai[0];
			if (parentID >= 0)
			{
				NPC npc = Main.npc[parentID];
				if (npc.active && npc.type == ModContent.NPCType<PharaohsCurse>())
				{
					var Limb = this.projectile;
					Vector2 distanceToOwner = Limb.Center - npc.Center;
					float distance = distanceToOwner.Length();
					if (distance == 0)
						return false;
					float distanceModified = 0.8f * (32f - (float)Math.Sqrt(distanceToOwner.Length()));
					if (distanceModified < 0)
						distanceModified = 0;
					int max = 16 + (int)(distance / 20);
					for (float k = 0; k < max;)
					{
						float percent = (float)k / max;
						Vector2 toARM = Limb.Center - npc.Center;
						toARM *= percent;
						Vector2 spiralAddition = new Vector2(distanceModified * (1.5f - 1.0f * percent), 0).RotatedBy(MathHelper.ToRadians(projectile.ai[1] * 3 + k * 24 * (float)Math.Pow((1280.0 / distance), 0.6)));
						spiralAddition = new Vector2(0, spiralAddition.X).RotatedBy(toARM.ToRotation());
						Vector2 finalPosition = npc.Center + toARM + spiralAddition;
						float scale = 0.5f * (1.55f - 0.75f * percent);
						Vector2 rotational = new Vector2(0, (1.45f - 0.55f * percent) / (1.0f * (float)Math.Pow((1280.0 / distance), 0.2))).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(360)));
						Vector2 rotationaPosMod = new Vector2(0, Main.rand.NextFloat(4) * scale).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(360)));
						rotational += Limb.velocity * 0.1f;
						if(targetHitbox.X != 0 || targetHitbox.Y != 0)
                        {
							int width = (int)(12 - 6 * percent);
							Rectangle hitbox = new Rectangle((int)(finalPosition.X - width), (int)(finalPosition.Y - width), width, width);
							if (hitbox.Intersects(targetHitbox))
								return true;
                        }
						else
                        {
							dustList.Add(new CurseFoam(finalPosition + rotationaPosMod.SafeNormalize(Vector2.Zero) * 2 * scale * (0.5f * (float)Math.Pow((1280.0 / distance), 0.2)), rotational, Main.rand.NextFloat(0.9f, 1.1f) * scale, true));
						}
						k += scale;
					}
				}
			}
			return false;
		}
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			int parentID = (int)projectile.ai[0];
			if (parentID >= 0)
			{
				NPC npc = Main.npc[parentID];
				if (npc.active && npc.type == ModContent.NPCType<PharaohsCurse>())
				{
					OwnerPos = npc.Center;
					Vector2 distanceToOwner = projectile.Center - OwnerPos;
					PharaohsCurse curse = npc.modNPC as PharaohsCurse;
					return DrawLimbs(curse.foamParticleList1, targetHitbox);
				}
				else
				{
					OwnerPos = Vector2.Zero;
				}
			}
			return false;
        }
        Vector2 savedVelocity;
        public override bool PreAI()
		{
			projectile.ai[1]++; 
			if (projectile.ai[1] >= 60)
			{
				float veloLength = projectile.velocity.Length();
				if(veloLength < 64f)
					projectile.velocity *= 1.1f;
				if(projectile.ai[1] == 60)
				{
					Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 96, 1.25f, -0.2f);
				}
            }
			if (projectile.ai[1] < 60)
			{
				float mult = 1.0f;
				if (projectile.ai[1] > 20)
					mult = 0.30f;
				projectile.position += (float)Math.Sin(MathHelper.ToRadians(4.5f * projectile.ai[1] + 90)) * projectile.velocity * mult;
			}
			else
            {
				float veloLength = projectile.velocity.Length();
				int length = (int)(veloLength / 14);
				veloLength -= length * 14;
				projectile.position += projectile.velocity.SafeNormalize(Vector2.Zero) * veloLength;
				for(int i = 0; i < length; i++)
				{
					int x = (int)projectile.Center.X / 16;
					int y = (int)projectile.Center.Y / 16;
					Tile tile = Framing.GetTileSafely(x, y);
					if (!WorldGen.InWorld(x, y, 20) || (tile.active() && !Main.tileSolidTop[tile.type] && Main.tileSolid[tile.type] && (tile.type == ModContent.TileType<TrueSandstoneTile>() || tile.wall == ModContent.WallType<TrueSandstoneWallWall>())))
					{
						projectile.velocity *= 0.0f;
					}
					projectile.position += projectile.velocity.SafeNormalize(Vector2.Zero) * 14;
				}
            }
			return base.PreAI();
        }
        public override bool ShouldUpdatePosition()
        {
			return false;
        }
        public override void AI()
		{
			int parentID = (int)projectile.ai[0];
			if(parentID >= 0)
            {
				NPC npc = Main.npc[parentID];
				if(npc.active && npc.type == ModContent.NPCType<PharaohsCurse>())
                {
					OwnerPos = npc.Center;
					Vector2 distanceToOwner = projectile.Center - OwnerPos;
					PharaohsCurse curse = npc.modNPC as PharaohsCurse;
					DrawLimbs(curse.foamParticleList1, new Rectangle(0, 0, 0, 0));
					PharaohsCurse.SpawnPassiveDust(ModContent.GetTexture("SOTS/NPCs/Boss/Curse/CurseHookMask"), projectile.Center, 0.75f, curse.foamParticleList1, 0.2f, 3, 25, distanceToOwner.ToRotation() + MathHelper.ToRadians(90));
				}
				else
                {
					OwnerPos = Vector2.Zero;
                }
            }
		}
	}
}
		