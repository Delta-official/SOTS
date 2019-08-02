using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Projectiles 
{    
    public class HeartProj : ModProjectile 
    {	int wait = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Swapped");
			
		}
		
        public override void SetDefaults()
        {
		
		

			projectile.netImportant = true;
            projectile.width = 36;
            projectile.height = 40; 
            projectile.timeLeft = 2;
            projectile.penetrate = -1; 
            projectile.friendly = false; 
            projectile.hostile = false; 
            projectile.tileCollide = false;
            projectile.ignoreWater = true; 
            projectile.magic = true; 
			projectile.aiStyle = 0;
			projectile.alpha = 0;


		}
	
			
      

			
			  
		}
		}
	
	

