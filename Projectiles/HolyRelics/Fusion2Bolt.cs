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

namespace SOTS.Projectiles.HolyRelics
{    
    public class Fusion2Bolt : ModProjectile 
    {	int wait = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fusion Bolt");
			
		}
		
        public override void SetDefaults()
        {
			projectile.CloneDefaults(616);
            aiType = 616; 
			projectile.alpha = 255;
			projectile.timeLeft = 900;
			projectile.width = 1;
			projectile.height = 1;
			projectile.penetrate = 1;
			projectile.tileCollide = false;

		}
		public override void AI()
		{
			projectile.alpha = 255;
			
			int num1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 68);

			
			Main.dust[num1].noGravity = true;
			Main.dust[num1].velocity *= 0.1f;
			

		}
	}
}
		
			