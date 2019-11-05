using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SOTS.Projectiles.Pyramid
{    
    public class PhantomFist : ModProjectile 
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantom Fist");
			
		}
		
        public override void SetDefaults()
        {
			projectile.aiStyle = 0;
			projectile.thrown = true;
			projectile.friendly = true;
			projectile.width = 56;
			projectile.height = 30;
			projectile.timeLeft = 70;
			projectile.penetrate = 6;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.alpha = 40;
            Main.projFrames[projectile.type] = 5;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 15;
		}
		public override void AI()
		{ 
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
			projectile.velocity *= 0.99f;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 5;
            }
			projectile.alpha += 3;		
			float minDist = 560;
			int target2 = -1;
			float dX = 0f;
			float dY = 0f;
			float distance = 0;
			float speed = 0.15f;
			if(projectile.friendly == true && projectile.hostile == false)
			{
				for(int i = 0; i < Main.npc.Length - 1; i++)
				{
					NPC target = Main.npc[i];
					if(!target.friendly && target.dontTakeDamage == false && target.lifeMax > 5 && target.active)
					{
						dX = target.Center.X - projectile.Center.X;
						dY = target.Center.Y - projectile.Center.Y;
						distance = (float) Math.Sqrt((double)(dX * dX + dY * dY));
						if(distance < minDist)
						{
							minDist = distance;
							target2 = i;
						}
					}
				}
				
				if(target2 != -1)
				{
					NPC toHit = Main.npc[target2];
					if(toHit.active == true)
					{
						dX = toHit.Center.X - projectile.Center.X;
						dY = toHit.Center.Y - projectile.Center.Y;
						distance = (float)Math.Sqrt((double)(dX * dX + dY * dY));
						speed /= distance;
					   
						projectile.velocity += new Vector2(dX * speed, dY * speed);
					}
				}
			}
		}
	}
}
		