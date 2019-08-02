using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Projectiles.Minions
{    
    public class FrostProbe : ModProjectile 
    {	int wait = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frost Probe");
			
		}
		
        public override void SetDefaults()
        {
		
		
			projectile.CloneDefaults(199);
			aiType = 199;
			projectile.netImportant = true;
            projectile.width = 48;
            projectile.height = 28; 
            projectile.timeLeft = 30;
            projectile.penetrate = -1; 
            projectile.friendly = false; 
            projectile.hostile = false; 
            projectile.tileCollide = false;
            projectile.ignoreWater = true; 
            projectile.magic = true; 
			projectile.alpha = 0;


		}
		public override void AI() 
		{
			Player player = Main.player[projectile.owner];
			
				projectile.ai[1]++;
				float minDist = 1200;
				int target2 = -1;
				float dX = 0f;
				float dY = 0f;
				float distanceEnemy = 0;
				Vector2 towardsEnemy = new Vector2(0, 0);
				float speed = 6.75f;
				
					for(int j = 0; j < Main.npc.Length - 1; j++)
					{
						NPC target = Main.npc[j];
						if(!target.friendly && target.dontTakeDamage == false && target.active)
							{
								dX = target.Center.X - projectile.Center.X;
								dY = target.Center.Y - projectile.Center.Y;
								distanceEnemy = (float) Math.Sqrt((double)(dX * dX + dY * dY));
								if(distanceEnemy < minDist)
									{
										minDist = distanceEnemy;
										target2 = j;
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
							distanceEnemy = (float)Math.Sqrt((double)(dX * dX + dY * dY));
							speed /= distanceEnemy;
											   
							towardsEnemy = new Vector2(dX * speed, dY * speed);
							projectile.tileCollide = false;
						}
					}
					
					if(towardsEnemy.X == 0 || towardsEnemy.Y == 0)
					{
						projectile.rotation += MathHelper.ToRadians(1);
					}
					else
					{
						double startingDirection = Math.Atan2((double)-towardsEnemy.Y, (double)-towardsEnemy.X);
						startingDirection *= 180/Math.PI;
						
						projectile.rotation = MathHelper.ToRadians((float)startingDirection);
						
						if(projectile.ai[1] >= 70)
						{
							if(projectile.owner == Main.myPlayer)
							{
							Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, towardsEnemy.X * 3.25f, towardsEnemy.Y * 3.25f, mod.ProjectileType("MargritBoltFriendly"), projectile.damage, 2, Main.myPlayer, 0f, 0f);
							Main.PlaySound(SoundID.Item11, (int)(projectile.Center.X), (int)(projectile.Center.Y));
							}
							projectile.ai[1] = 0;			   
						}
					}
		}
	}
}
