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
    public class MightEssence2 : ModProjectile 
    {	int bounce = 24;
		int wait = 1;              
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Might Essence");
			
		}
		
        public override void SetDefaults()
        {
			projectile.CloneDefaults(263);
            aiType = 263; 
			projectile.height = 16;
			projectile.width = 16;
			projectile.ranged = true;
			projectile.friendly = false;
			projectile.timeLeft = 120;
			projectile.hostile = false;
		}
		
		public override void Kill(int timeLeft)
		{
			
				 NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType("MightPawn"));	
		}
	}
}
		