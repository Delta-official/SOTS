using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace SOTS.NPCs.Boss.CelestialSerpent
{   
    public class CelestialSerpentTail : ModNPC
    {	
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Serpent");
		}
        public override void SetDefaults()
        {
            npc.width = 60;             
            npc.height = 60;         
            npc.damage = 40;
            npc.defense = 50;
            npc.lifeMax = 12312412; 
            Main.npcFrameCount[npc.type] = 22;  
            npc.knockBackResist = 0.0f;
            npc.noTileCollide = true;
            npc.boss = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.dontCountMe = true;
            npc.value = 100000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath32;
			music = MusicID.Boss2;
            for (int i = 0; i < Main.maxBuffTypes; i++)
            {
                npc.buffImmune[i] = true;
            }
        }
		float ai2 = 0;
        public override bool PreAI()
        {
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
 
            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }
 
            if (npc.ai[1] < (double)Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = npc.Center;
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - 48) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }
            return false;
        }
		float ai1 = 0;
		int frame = 0;
		public override void FindFrame(int frameHeight) 
		{
			Player player  = Main.player[npc.target];
			frame = frameHeight;
			
            if (Main.npc[(int)npc.ai[3]].velocity.X == 0 && Main.npc[(int)npc.ai[3]].velocity.Y == 0)
			{
				ai1 += 4.75f;
			}
				
			ai1 += 0.75f;
			if (ai1 >= 5f) 
			{
				ai1 -= 5f;
				npc.frame.Y += frame;
				if(npc.frame.Y >= 22 * frame)
				{
					npc.frame.Y = 0;
				}
				if(npc.frame.Y == frame * 10)
				{
					float shootToX = player.Center.X - npc.Center.X;
					float shootToY = player.Center.Y - npc.Center.Y;
					float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

					distance = 5f / distance;
							  
					shootToX *= distance * 5;
					shootToY *= distance * 5;
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, shootToX, shootToY, mod.ProjectileType("BlueCellBlast"), 30, 0f, 0);
				}
				if(npc.frame.Y == 0)
				{
				
					float shootToX = player.Center.X - npc.Center.X;
					float shootToY = player.Center.Y - npc.Center.Y;
					float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

					distance = 5f / distance;
							  
					shootToX *= distance * 5;
					shootToY *= distance * 5;
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, shootToX, shootToY, mod.ProjectileType("PurpleCellBlast"), 30, 0f, 0);
				}
			}
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f + 6, texture.Height * 0.5f / Main.npcFrameCount[npc.type]);
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;       //this make that the npc does not have a health bar
        }
		public override void PostAI()
		{
			Lighting.AddLight(npc.Center, (255 - npc.alpha) * 2.5f / 255f, (255 - npc.alpha) * 1.6f / 255f, (255 - npc.alpha) * 2.4f / 255f);
			npc.timeLeft = 10000000;
		}
    }
}