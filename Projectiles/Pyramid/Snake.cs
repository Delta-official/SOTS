using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SOTS.Projectiles.Pyramid
{    
    public class Snake : ModProjectile 
    {	
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snakey Boi");
		}
        public override void SetDefaults()
        {
			projectile.aiStyle = 1;
			projectile.width = 50;
			projectile.height = 18;
			Main.projFrames[projectile.type] = 4;
			projectile.penetrate = 10;
			projectile.friendly = true;
			projectile.timeLeft = 3600;
			projectile.tileCollide = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.alpha = 0;
		}
		public override void SendExtraAI(BinaryWriter writer) 
		{
			writer.Write(projectile.rotation);
			writer.Write(projectile.spriteDirection);
			writer.Write(damageCounter);
			writer.Write(latch);
			writer.Write(enemyIndex);
			writer.Write(diffPosX);
			writer.Write(diffPosY);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{	
			projectile.rotation = reader.ReadSingle();
			projectile.spriteDirection = reader.ReadInt32();
			damageCounter = reader.ReadInt32();
			latch = reader.ReadBoolean();
			enemyIndex = reader.ReadInt32();
			diffPosX = reader.ReadSingle();
			diffPosY = reader.ReadSingle();
		}
		int damageCounter = 0;
		bool latch = false;
		int enemyIndex = -1;
		float diffPosX = 0;
		float diffPosY = 0;
		public override void AI()
        {
			Player player = Main.player[projectile.owner];
			projectile.tileCollide = true;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 4;
            }
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
			projectile.spriteDirection = 1;
			if(projectile.velocity.X < 0)
			{
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) - MathHelper.ToRadians(180);
				projectile.spriteDirection = -1;
			}
			
			if(latch && enemyIndex != -1)
			{
				projectile.netUpdate = true;
				NPC target = Main.npc[enemyIndex];
				if(target.active && !target.friendly)
				{
					projectile.aiStyle = 0;
					projectile.position.X = target.Center.X - projectile.width/2 - diffPosX;
					projectile.position.Y = target.Center.Y - projectile.height/2 - diffPosY;
				}
				else
				{
					enemyIndex = -1;
					projectile.aiStyle = 1;
					latch = false;
					projectile.tileCollide = true;
					projectile.friendly = true;
				}
			}
			if(!projectile.friendly)
			{
				damageCounter++;
				if(damageCounter >= 60)
				{
					damageCounter = 0;
					projectile.friendly = true;
				}
			}
			if(projectile.damage <= 1)
			{
				projectile.Kill();
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Player player = Main.player[projectile.owner];
			projectile.friendly = false;
            target.immune[projectile.owner] = 0;
			projectile.tileCollide = false;
			latch = true;
			projectile.damage--;
			projectile.damage = (int)(projectile.damage * 0.7f);
			for(int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if(npc == target)
				{
					if(diffPosX == 0)
					diffPosX = npc.Center.X - projectile.Center.X;
				
					if(diffPosY == 0)
					diffPosY = npc.Center.Y - projectile.Center.Y;
				
					enemyIndex = i;
					break;
				}
			}
			if(target.life <= 0)
			{
				enemyIndex = -1;
				projectile.aiStyle = 1;
				latch = false;
				projectile.tileCollide = true;
				projectile.friendly = true;
			}
        }
		public override void Kill(int timeLeft)
        {
			for(int i = 0; i < 15; i++)
			{
				int num1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 42, 24, 251);
				Main.dust[num1].noGravity = true;
			}
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
			width = 12;
			height = 12;
			return base.TileCollideStyle(ref width, ref height, ref fallThrough);
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if(projectile.penetrate < 1)
			{
				projectile.Kill();
			}
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X * 0.45f; 
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y * 0.4f;
			}
			return false;
		}
	}
}
		