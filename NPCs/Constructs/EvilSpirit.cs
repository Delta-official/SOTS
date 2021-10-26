using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOTS.Dusts;
using SOTS.Items.Fragments;
using SOTS.Projectiles.Evil;
using SOTS.Projectiles.Tide;
using SOTS.Void;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace SOTS.NPCs.Constructs
{
	public class EvilSpirit : ModNPC
	{	
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 1;
			DisplayName.SetDefault("Evil Spirit");
			NPCID.Sets.TrailCacheLength[npc.type] = 5;  
			NPCID.Sets.TrailingMode[npc.type] = 0;
		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(phase);
			writer.Write(counter);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			phase = reader.ReadInt32();
			counter = reader.ReadInt32();
		}
		public override void SetDefaults()
		{
			npc.aiStyle = 10;
            npc.lifeMax = 2500; 
            npc.damage = 80; 
            npc.defense = 0;   
            npc.knockBackResist = 0f;
            npc.width = 58;
            npc.height = 58;
            npc.value = Item.buyPrice(0, 10, 0, 0);
            npc.npcSlots = 7f;
            npc.boss = false;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.netAlways = false;
			npc.rarity = 2;
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.damage = 80;
			npc.lifeMax = 4000;
		}
		List<EvilEye> eyes = new List<EvilEye>();
		private int InitiateHealth = 8000;
		private float ExpertHealthMult = 1.5f;
		int phase = 1;
		int counter = 0;
		int counter2 = 0;
		public const int range = 128;
		public void UpdateEyes(bool draw = false, int ring = -2)
		{
			Player player = Main.player[npc.target];
			for (int i = 0; i < eyes.Count; i++)
            {
				EvilEye eye = eyes[i];
				float mult = 256f / (eye.offset.Length() + 24);
				int direction = (((int)(eye.offset.Length() + 0.5f) % (2 * range)) / range) % 2 == 0 ? -1 : 1;
				float rotation = (npc.rotation + MathHelper.ToRadians(counter2 * direction)) * mult;
				if (draw)
				{
					eye.Draw(npc.Center, rotation);
				}
				else
				{
					int ringNumber = (int)(eye.offset.Length() + 0.5f - range) / range;
					if(ringNumber == ring)
					{
						eye.Fire(player.Center);
                    }
					eye.Update(npc.Center, rotation);
				}
            }
        }
		public override void AI()
		{
			Lighting.AddLight(npc.Center, (255 - npc.alpha) * 0.15f / 255f, (255 - npc.alpha) * 0.25f / 255f, (255 - npc.alpha) * 0.65f / 255f);
			Player player = Main.player[npc.target];
			UpdateEyes();
			if (phase == 3)
			{
				npc.dontTakeDamage = false;
				npc.velocity *= 0.95f;
				if (npc.ai[0] >= 0)
				{
					int damage = npc.damage / 2;
					if (Main.expertMode)
					{
						damage = (int)(damage / Main.expertDamage);
					}
					int counterR = (int)(npc.ai[0]);
					this.counter2 = counterR;
					if(counterR % 6 == 0 && counterR < 180)
                    {
						Main.PlaySound(SoundID.Item, (int)npc.Center.X, (int)npc.Center.Y, 30, 0.7f, -0.4f);
					}
					if(counterR < 120)
					{
						if (counterR < 60)
						{
							if (counterR % 5 == 0)
							{
								Vector2 circular = new Vector2(3 * range, 0).RotatedBy(MathHelper.ToRadians(counterR * 4f));
								eyes.Add(new EvilEye(circular, damage));
								circular = new Vector2(-2 * range, 0).RotatedBy(MathHelper.ToRadians(counterR * 4f));
								eyes.Add(new EvilEye(circular, damage));
							}
						}
						else
						{
							if (counterR % 4 == 0)
							{
								Vector2 circular = new Vector2(5 * range, 0).RotatedBy(MathHelper.ToRadians(counterR * 4f));
								eyes.Add(new EvilEye(circular, damage));
								circular = new Vector2(-4 * range, 0).RotatedBy(MathHelper.ToRadians(counterR * 4f));
								eyes.Add(new EvilEye(circular, damage));
							}
						}
                    }
					else
					{
						if (counterR % 3 == 0 && counterR < 180)
						{
							Vector2 circular = new Vector2(7 * range, 0).RotatedBy(MathHelper.ToRadians(counterR * 4f));
							eyes.Add(new EvilEye(circular, damage));
							circular = new Vector2(-6 * range, 0).RotatedBy(MathHelper.ToRadians(counterR * 4f));
							eyes.Add(new EvilEye(circular, damage));
						}
						Vector2 toPlayer = player.Center - npc.Center;
						float speed = 12 + toPlayer.Length() * 0.01f;
						if (counterR % 180 == 120)
						{
							npc.velocity += toPlayer.SafeNormalize(Vector2.Zero) * speed;
						}
						if(counterR % 180 == 0)
						{
							npc.ai[1]++;
							if (npc.ai[1] > 6)
							{
								npc.ai[1] = 0;
							}
							if (npc.ai[1] > 0)
                            {
								int ring = (int)npc.ai[1];
								UpdateEyes(false, ring);
                            }
						}
						if (counterR % 180 == 40)
						{
							if(npc.ai[1] > 0)
							{
								Main.PlaySound(SoundID.Item, (int)npc.Center.X, (int)npc.Center.Y, 46, 1.1f, -0.15f);
							}
						}
						float sin = (float)Math.Sin(MathHelper.ToRadians(counterR * 2));
						Vector2 additional = new Vector2(0, sin * 0.1f);
						npc.velocity += additional;
						npc.rotation += npc.velocity.X * 0.005f;
					}
				}
				npc.ai[0]++;
			}
			if (phase == 2)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
					npc.netUpdate = true;
				npc.dontTakeDamage = false;
				npc.aiStyle = -1;
				npc.ai[0] = 0;
				npc.ai[1] = 0;
				npc.ai[2] = 0;
				npc.ai[3] = 0;
				phase = 3;
			}
			else if(phase == 1)
			{
				counter++;
			}
			if(Main.player[npc.target].dead)
			{
				counter++;
			}
			if(counter >= 1440)
			{
				if (Main.netMode != 1)
				{
					npc.netUpdate = true;
				}
				phase = 1;
				npc.aiStyle = -1;
				npc.velocity.Y -= 0.014f;
				npc.dontTakeDamage = true;
			}
			int dust2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 267);
			Dust dust = Main.dust[dust2];
			dust.color = new Color(VoidPlayer.EvilColor.R, VoidPlayer.EvilColor.G, VoidPlayer.EvilColor.B);
			dust.noGravity = true;
			dust.fadeIn = 0.1f;
			dust.scale *= 2f;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.npcTexture[npc.type];
			Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
			for (int k = 0; k < npc.oldPos.Length; k++) {
				Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
				Color color = npc.GetAlpha(Color.Black) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
				spriteBatch.Draw(texture, drawPos, null, color * 0.5f, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
			}
			return false;
		}	
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				for(int i = 0; i < 50; i ++)
				{
					Dust dust = Dust.NewDustDirect(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 267);
					dust.color = new Color(VoidPlayer.EvilColor.R, VoidPlayer.EvilColor.G, VoidPlayer.EvilColor.B);
					dust.noGravity = true;
					dust.fadeIn = 0.1f;
					dust.scale *= 2f;
					dust.velocity *= 5f;
				}
				if(phase == 1)
				{
					phase = 2;
					npc.lifeMax = (int)(InitiateHealth * (Main.expertMode ? ExpertHealthMult : 1));
					npc.life = (int)(InitiateHealth * (Main.expertMode ? ExpertHealthMult : 1));
				}
			}
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[npc.type];
			Color color = VoidPlayer.EvilColor * 1.3f;
			Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
			for (int k = 0; k < 7; k++)
			{
				Main.spriteBatch.Draw(texture,
				npc.Center + Main.rand.NextVector2Circular(4f, 4f) - Main.screenPosition,
				null, color, 0f, drawOrigin, 1f, SpriteEffects.None, 0f);
			}
			if(Main.netMode != NetmodeID.Server) //pretty sure drawcode doesn't run in multiplayer anyways but may as well
				UpdateEyes(true);
			base.PostDraw(spriteBatch, drawColor);
		}
		public override void NPCLoot()
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DissolvingUmbra>(), 1);	
		}	
	}
	public class EvilEye
    {
		public Texture2D texture;
		public Texture2D texturePupil;
		public Vector2 offset;
		public Vector2 fireTo;
		public float counter = 0;
		public float shootCounter = 0;
		public bool firing = false;
		public int damage;
		public EvilEye(Vector2 offset, int damage)
        {
			texture = ModContent.GetTexture("SOTS/NPCs/Constructs/EvilEye");
			texturePupil = ModContent.GetTexture("SOTS/NPCs/Constructs/EvilEyePupil");
			this.offset = offset;
			this.damage = damage;
        }
		public void Fire(Vector2 fireAt)
        {
			fireTo = fireAt;
			firing = true;
        }
		public void Update(Vector2 center, float rotation)
        {
			if (firing)
			{
				shootCounter++;
				if (shootCounter >= 40)
				{
					if(Main.netMode != NetmodeID.MultiplayerClient)
                    {
						Vector2 toPosition = fireTo - offset.RotatedBy(rotation) - center;
						Projectile.NewProjectile(center + offset.RotatedBy(rotation), toPosition.SafeNormalize(Vector2.Zero) * 1f, ModContent.ProjectileType<EvilBolt>(), damage, 0, Main.myPlayer);
                    }
					firing = false;
				}
			}
			else if (shootCounter > 0)
				shootCounter--;
			if(counter == 0)
			{
				for(int i = 0; i < 3; i++)
                {
					Dust dust = Dust.NewDustDirect(center + offset - new Vector2(4), 0, 0, ModContent.DustType<CopyDust4>());
					dust.color = new Color(VoidPlayer.EvilColor.R, VoidPlayer.EvilColor.G, VoidPlayer.EvilColor.B, 100);
					dust.alpha = 100;
					dust.noGravity = true;
					dust.fadeIn = 0.1f;
					dust.velocity *= 0.9f;
					dust.scale *= 1.5f;
				}
			}
			if(counter < 40)
				counter++;
        }
		public void Draw(Vector2 center, float rotation)
		{
			Color color = VoidPlayer.EvilColor;
			color.A = 20;
			Vector2 drawPosition = center + offset.RotatedBy(rotation) - Main.screenPosition;
			Vector2 origin = texture.Size() / 2;
			float mult = 1.2f + 0.3f * shootCounter / 40f;
			float alpha2 = shootCounter / 20f;
			if (alpha2 > 1)
				alpha2 = 1;
			float alpha = 0.5f * counter / 40f + 0.5f * alpha2;
			if (alpha > 1)
				alpha = 1;
			for(int i = 0; i < 5; i++)
			{
				int length = 0;
				if (i != 0)
					length = 1;
				Vector2 circular = new Vector2(length, 0).RotatedBy(i * MathHelper.Pi / 2f);
				Main.spriteBatch.Draw(texture, drawPosition + circular, null, color * alpha, 0f, origin, mult, SpriteEffects.None, 0f);
			}
			color = new Color(VoidPlayer.EvilColor.R * 2, VoidPlayer.EvilColor.G * 2, VoidPlayer.EvilColor.B * 2);
			for (int i = 0; i < 4; i++)
			{
				int length = 1;
				Vector2 circular = new Vector2(length, 0).RotatedBy(i * MathHelper.Pi / 2f);
				Main.spriteBatch.Draw(texturePupil, drawPosition + circular, null, color * alpha2, 0f, origin, mult, SpriteEffects.None, 0f);
			}
		}
    }
}
