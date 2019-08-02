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

namespace SOTS.Projectiles.Chess
{    
    public class PumpkinCrusher : ModProjectile 
    {	int wait = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crushing Pumpkin");
			
		}
		
        public override void SetDefaults()
        {
			projectile.CloneDefaults(1);
            aiType = 1; //18 is the demon scythe style
			projectile.timeLeft = 18000;
			projectile.hostile = true;
			projectile.friendly = true;
			projectile.width = 26;
			projectile.height = 26;
			projectile.penetrate = -1;
		}
		public override void AI()
		{
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{	
		projectile.velocity.X = 0;
		projectile.position.X = (int)projectile.position.X;
		projectile.position.Y = (int)projectile.position.Y;
		if(projectile.penetrate == -1)
		{
		projectile.penetrate = 5;
		}
		projectile.penetrate--;
		
			if (projectile.penetrate == 1)
			{
				projectile.Kill();
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 2;     //this is the explosion radius, the highter is the value the bigger is the explosion
 
                    int xPosition = (int)(position.X / 16.0f);
                    int yPosition = (int)(position.Y / 16.0f);
				WorldGen.PlaceTile(xPosition, yPosition, TileID.PumpkinBlock);
				 
			}
			return false;
			
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
				if(target.lifeMax <= 10000)
				{
					target.life = 1;
					
				}
			
		}	
	}
}
		
			