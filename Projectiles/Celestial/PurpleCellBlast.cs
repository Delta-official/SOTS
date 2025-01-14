using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SOTS.Projectiles.Celestial
{    
    public class PurpleCellBlast : ModProjectile 
    {	
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Confusionspire");
		}
        public override void SetDefaults()
        {
			projectile.width = 26;
			projectile.height = 8;
			projectile.friendly = false;
			projectile.timeLeft = 720;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hostile = true;
			projectile.alpha = 125;
		}
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
			int num1 = Dust.NewDust(new Vector2(projectile.Center.X - 2, projectile.Center.Y - 2), 2, 2, 21);
			Main.dust[num1].noGravity = true;
			Main.dust[num1].velocity *= 0.1f;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit) 
		{
		//	target.AddBuff(BuffID.Confused, 90, false); //shadowflame
		}
	}
}
		