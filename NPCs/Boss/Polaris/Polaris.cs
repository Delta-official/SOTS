using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOTS.Items.IceStuff;
using SOTS.Projectiles.Celestial;
using SOTS.Projectiles.Permafrost;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.NPCs.Boss.Polaris
{	[AutoloadBossHead]
	public class Polaris : ModNPC
	{	
		int despawn = 0;
		private float AICycle
		{
			get => npc.ai[0];
			set => npc.ai[0] = value;
		}
		private float AICycle2
		{
			get => npc.ai[1];
			set => npc.ai[1] = value;
		}
		private float AICycle3
		{
			get => npc.ai[2];
			set => npc.ai[2] = value;
		}
		private float transition
		{
			get => npc.ai[3];
			set => npc.ai[3] = value;
		}
        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Polaris");
		}
		public override void SetDefaults()
		{
            npc.lifeMax = 36000;
            npc.damage = 80; 
            npc.defense = 28;  
            npc.knockBackResist = 0f;
            npc.width = 162;
            npc.height = 162;
            Main.npcFrameCount[npc.type] = 1;
            npc.value = 100000;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Polaris");
			npc.netAlways = true;
            npc.buffImmune[BuffID.Frostburn] = true;
			npc.buffImmune[BuffID.Ichor] = true;
			npc.buffImmune[BuffID.OnFire] = true;
			bossBag = ModContent.ItemType<PolarisBossBag>();
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = ModContent.GetTexture("SOTS/Projectiles/Celestial/SubspaceLingeringFlame");
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
			for (int i = 0; i < particleListRed.Count; i++)
			{
				Color color = new Color(250, 100, 100, 0);
				Vector2 drawPos = particleListRed[i].position - Main.screenPosition;
				color = color * (0.3f + 0.7f * particleListRed[i].scale);
				for (int j = 0; j < 2; j++)
				{
					float x = Main.rand.NextFloat(-2f, 2f);
					float y = Main.rand.NextFloat(-2f, 2f);
					Main.spriteBatch.Draw(texture, drawPos + new Vector2(x, y), null, color, particleListRed[i].rotation, drawOrigin, particleListRed[i].scale * 1.0f, SpriteEffects.None, 0f);
				}
			}
			for (int i = 0; i < particleListBlue.Count; i++)
			{
				Color color = new Color(200, 250, 250, 0);
				Vector2 drawPos = particleListBlue[i].position - Main.screenPosition;
				color = color * (0.3f + 0.7f * particleListBlue[i].scale);
				for (int j = 0; j < 2; j++)
				{
					float x = Main.rand.NextFloat(-2f, 2f);
					float y = Main.rand.NextFloat(-2f, 2f);
					Main.spriteBatch.Draw(texture, drawPos + new Vector2(x, y), null, color, particleListBlue[i].rotation, drawOrigin, particleListBlue[i].scale * 1.0f, SpriteEffects.None, 0f);
				}
			}
			return base.PreDraw(spriteBatch, drawColor);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = mod.GetTexture("NPCs/Boss/Polaris/PolarisThruster");
			Vector2 drawOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
			for (int i = 0; i < 4; i++)
			{
				int direction = (i % 2) * 2 - 1;
				int yDir = 1;
				if(i >= 2)
                {
					yDir = -1;
				}
				Vector2 rotationOrigin = new Vector2(-3f * -direction, 6f) - npc.velocity * 2.4f;
				float overrideRotation = rotationOrigin.ToRotation() - MathHelper.ToRadians(90);
				Vector2 fromBody = npc.Center + new Vector2(direction * 32, 32 * yDir).RotatedBy(npc.rotation);
				Vector2 drawPos = fromBody - Main.screenPosition + new Vector2(0, 2);
				spriteBatch.Draw(texture, drawPos, null, Color.White, npc.rotation + overrideRotation, drawOrigin, 0.9f, direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			}
			base.PostDraw(spriteBatch, drawColor);
		}
		List<FireParticle> particleListRed = new List<FireParticle>();
		List<FireParticle> particleListBlue = new List<FireParticle>();
		public void cataloguePos()
		{
			for (int i = 0; i < particleListBlue.Count; i++)
			{
				FireParticle particle = particleListBlue[i];
				particle.Update();
				if (!particle.active)
				{
					particleListBlue.RemoveAt(i);
					i--;
				}
			}
			for (int i = 0; i < particleListRed.Count; i++)
			{
				FireParticle particle = particleListRed[i];
				particle.Update();
				if (!particle.active)
				{
					particleListRed.RemoveAt(i);
					i--;
				}
			}
		}
		public override void PostAI()
		{
			for (int i = 0; i < 4; i++)
			{
				int direction = (i % 2) * 2 - 1;
				int yDir = 1;
				if (i >= 2)
				{
					yDir = -1;
				}
				Vector2 rotationOrigin = new Vector2(-3f * -direction, 6f) - npc.velocity * 2.4f;
				float overrideRotation = rotationOrigin.ToRotation();
				Vector2 dustVelo = new Vector2(6.0f, 0).RotatedBy(overrideRotation);
				Vector2 fromBody = npc.Center + new Vector2(direction * 32, 32 * yDir).RotatedBy(npc.rotation);
				if(i >= 2)
					particleListBlue.Add(new FireParticle(fromBody + dustVelo * npc.scale, dustVelo, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(0.8f, 1.1f)));
				else
					particleListRed.Add(new FireParticle(fromBody + dustVelo * npc.scale, dustVelo, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(0.8f, 1.1f)));
			}
			cataloguePos();
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.63889f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.75f);  //boss damage increase in expermode
        }
		public override void BossLoot(ref string name, ref int potionType)
		{ 
			SOTSWorld.downedAmalgamation = true;
			potionType = ItemID.GreaterHealingPotion;
			if(Main.expertMode)
				npc.DropBossBags();
			else 
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AbsoluteBar"), Main.rand.Next(26, 35)); 
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FrostCore, Main.rand.Next(2) + 1); 
			}
		}
		public void SpawnShard(int amt = 1)
		{
			Player player = Main.player[npc.target];
			Main.PlaySound(SoundID.Item44, (int)npc.Center.X, (int)npc.Center.Y);
			if (Main.netMode != 1)
			{
				int damage = npc.damage / 2;
				if (Main.expertMode)
				{
					damage = (int)(damage / Main.expertDamage);
				}
				for (int i = 0; i < amt; i++)
				{
					float max = 250 + 100 * i;
					Projectile.NewProjectile(npc.Center + new Vector2(Main.rand.NextFloat(max), 0).RotatedBy(Main.rand.NextFloat(2f * (float)Math.PI)), Vector2.Zero, ModContent.ProjectileType<PolarMortar>(), (int)(damage * 0.8f), 0, Main.myPlayer, player.Center.X + Main.rand.NextFloat(-100, 100), player.Center.Y - Main.rand.NextFloat(100));
				}
			}
		}
		public void SpawnCannons()
		{
			Main.PlaySound(SoundID.Item50, (int)npc.Center.X, (int)npc.Center.Y);
			if (Main.netMode != 1)
				for (int i = 0; i < 4; i++)
				{
					NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<PolarisCannon>(), 0, i, npc.whoAmI);
				}
		}
		public void SpawnDragon()
		{
			Player player = Main.player[npc.target];
			Main.PlaySound(SoundID.Item119, (int)(npc.Center.X), (int)(npc.Center.Y));
			if (Main.netMode != 1)
			{
				Vector2 vectorToPlayer = player.Center - npc.Center;
				vectorToPlayer = vectorToPlayer.SafeNormalize(Vector2.Zero) * -1200;
				vectorToPlayer += npc.Center;
				NPC.NewNPC((int)vectorToPlayer.X, (int)vectorToPlayer.Y, ModContent.NPCType<BulletSnakeHead>());
			}
		}
		float variance = 0;
		public void MovetoPlayer()
		{
			variance++;
			float idleAnim = new Vector2(1, 0).RotatedBy(MathHelper.ToRadians(variance * 3)).Y;
			Player player = Main.player[npc.target];
			npc.velocity *= 0.725f;
			Vector2 vectorToPlayer = player.Center - npc.Center;
			float yDist = vectorToPlayer.Y * 1.35f;
			float xDist = vectorToPlayer.X;
			float length = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
			float speedMult = -9.5f + idleAnim + (float)Math.Pow(length, 1.035) * 0.014f;
			if(speedMult < 0)
            {
				speedMult *= 0.5f;
            }
			npc.velocity += vectorToPlayer.SafeNormalize(Vector2.Zero) * speedMult * 0.6f;
		}
		public override void AI()
		{
			MovetoPlayer();
			Lighting.AddLight(npc.Center, (255 - npc.alpha) * 0.9f / 255f, (255 - npc.alpha) * 0.1f / 255f, (255 - npc.alpha) * 0.3f / 255f);
			Player player = Main.player[npc.target];
			if(player.dead)
			{
				despawn++;
			}
			if(despawn >= 600)
			{
				npc.active = false;
			}
			float shardRate = 120;
			AICycle++;
			if(npc.life < npc.lifeMax * 0.45f)
			{
				AICycle = 0;
				if(transition > 300)
				{
					AICycle2++;
					AICycle3++;
					shardRate *= (float)(npc.life + 500) / (float)(npc.lifeMax * 0.5f + 2500);
				}
				else
				{
					transition++;
				}
			}
			if(!player.ZoneSnow || Main.expertMode)
			{
				shardRate *= 0.8f;
			}
			if (AICycle3 >= shardRate)
			{
				SpawnShard(1);
				AICycle3 = 0;
			}
			if (transition > 0 && transition < 250)
			{
				npc.velocity *= 0.9f;
				npc.dontTakeDamage = true;
				if(transition % 30 == 0)
				{
					int index = (int)(transition / 30);
					if(Main.netMode != 1)
						NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<PolarisSpike>(), 0, index, npc.whoAmI);
					Main.PlaySound(SoundID.Item50, (int)(npc.Center.X), (int)(npc.Center.Y));
				}
			}
			else
			{
				npc.dontTakeDamage = false;
			}
			if (AICycle == 400)
			{
				if (!NPC.AnyNPCs(ModContent.NPCType<PolarisCannon>()))
					SpawnCannons();
				int extra = Main.expertMode ? 1 : 0;
				SpawnShard(2 + extra);
			}
			if (AICycle >= 800 && AICycle <= 1200 && AICycle % 100 == 0)
			{
				int extra = Main.expertMode ? 1 : 0;
				SpawnShard(2 + extra);
			}
			if (AICycle == 1600)
			{
				if (!NPC.AnyNPCs(ModContent.NPCType<PolarisCannon>()))
					SpawnCannons();
				else if (!NPC.AnyNPCs(ModContent.NPCType<BulletSnakeHead>()))
					SpawnDragon();
				else
				{
					int extra = Main.expertMode ? 1 : 0;
					SpawnShard(2 + extra);
				}
			}
			if (AICycle >= 1900 && AICycle <= 2000 && AICycle % 50 == 0)
			{
				int extra = Main.expertMode ? 1 : 0;
				SpawnShard(2 + extra);
			}
			if (AICycle >= 2400 && AICycle <= 2600)
			{
				npc.rotation = MathHelper.ToRadians(Main.rand.NextFloat(360));
				if (AICycle % 20 == 0)
				{
					int extra = Main.expertMode ? 1 : 0;
					SpawnShard(2 + extra);
				}
			}
			else
            {
				npc.rotation = npc.velocity.X * 0.08f;
				npc.direction = 1;
				npc.spriteDirection = 1;
            }
			if(AICycle >= 2600)
			{
				if (!NPC.AnyNPCs(ModContent.NPCType<BulletSnakeHead>()))
					SpawnDragon();
				AICycle = 0;
			}
			if(AICycle2 == 360)
			{
				if (!NPC.AnyNPCs(ModContent.NPCType<PolarisCannon>()))
					SpawnCannons();
			}
			if(AICycle2 == 900)
			{
				if (Main.netMode != 1)
					NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<PolarisLaser>(), 0, 0, npc.whoAmI);
			}
			if(AICycle2 == 1200)
			{
				if (!NPC.AnyNPCs(ModContent.NPCType<BulletSnakeHead>()))
					SpawnDragon();
			}
			if(AICycle2 >= 1400)
			{
				AICycle2 = 0;
			}
		}
	}
}





















