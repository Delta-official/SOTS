using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SOTS.Projectiles.Nature
{    
    public class AcornOfJustice : ModProjectile 
    {	
        public override void SetDefaults()
        {
			projectile.aiStyle = 1;
			projectile.width = 20;
			projectile.height = 20;
            projectile.magic = true;
			projectile.penetrate = 1;
			projectile.ranged = false;
			projectile.alpha = 0; 
			projectile.friendly = true;
		}
		public override void AI()
        {
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) - MathHelper.ToRadians(125);
			projectile.spriteDirection = 1;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Player player = Main.player[projectile.owner];
			if(projectile.owner == Main.myPlayer)
			{
				int Probe = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ModContent.ProjectileType<GrowTree>(), projectile.damage, projectile.knockBack, player.whoAmI, 1);
				Main.projectile[Probe].rotation = (float)Math.Atan2((double)oldVelocity.Y, (double)oldVelocity.X) + MathHelper.ToRadians(90);
				Main.projectile[Probe].spriteDirection = 1;
				Main.projectile[Probe].frame = 3;
			}
			return true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Player player = Main.player[projectile.owner];
			if(projectile.owner == Main.myPlayer)
			{
				int Probe = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ModContent.ProjectileType<GrowTree>(), projectile.damage, projectile.knockBack, player.whoAmI, 1);
				Main.projectile[Probe].rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + MathHelper.ToRadians(90);
				Main.projectile[Probe].spriteDirection = 1;
				Main.projectile[Probe].frame = 3;
            }
		}
		public override void Kill(int timeLeft)
        {
			for(int i = 0; i < 15; i++)
			{
				int num1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 2);
				Main.dust[num1].noGravity = true;
			}
		}
	}
}