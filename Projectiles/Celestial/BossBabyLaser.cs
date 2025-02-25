using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOTS.Buffs;
using SOTS.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Projectiles.Celestial
{
	public class BossBabyLaser : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Red Laser");
		}
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.alpha = 80;
			projectile.penetrate = -1;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 360, false);
		}
		public override bool ShouldUpdatePosition()
        {
			return false;
        }
		int counter = 0;
        public override void AI()
		{
			counter++;
			//projectile.Center = npc.Center;
			if (projectile.alpha <= 100)
            {
				for (int i = 0; i < 20; i++)
				{
					int dust3 = Dust.NewDust(projectile.Center - new Vector2(20, 20) - new Vector2(5), 40, 40, ModContent.DustType<CopyDust4>());
					Dust dust4 = Main.dust[dust3];
					dust4.velocity *= 0.55f;
					dust4.velocity += projectile.velocity.SafeNormalize(Vector2.Zero) * 2f;
					dust4.color = new Color(255, 69, 0, 0);
					dust4.noGravity = true;
					dust4.fadeIn = 0.1f;
					dust4.scale *= 2.75f;
				}
			}
			projectile.alpha += 7;
			if (projectile.alpha > 120)
			{
				projectile.hostile = false;
			}
			if (projectile.alpha >= 255)
			{
				projectile.Kill();
			}
		}
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (!projectile.hostile)
				return false;
			float laserDist = 200;
			Vector2 center = projectile.Center;
			for (int i = 0; i < laserDist; i++)
			{
				Rectangle rect = new Rectangle((int)center.X - 18, (int)center.Y - 18, 36, 36);
				center += projectile.velocity.SafeNormalize(new Vector2(1, 0)) * projectile.width * 2.5f;
				if (rect.Intersects(targetHitbox))
					return true;
			}
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];
			Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
			float laserDist = 200;
			Vector2 center = projectile.Center;
			for (int i = 0; i < laserDist; i++)
            {
				center += projectile.velocity.SafeNormalize(new Vector2(1, 0)) * projectile.width * 3f;
				spriteBatch.Draw(texture, center - Main.screenPosition, null, new Color(255, 69, 0, 0) * ((255f - projectile.alpha) / 255f), projectile.velocity.ToRotation(), origin, 3f, SpriteEffects.None, 0f);
				for (int j = 0; j < 2; j++)
				{
					float bonusAlphaMult = 1 - 1 * (counter / 28f);
					float dir = j * 2 - 1;
					Vector2 offset = new Vector2(counter * 0.75f * dir, 0).RotatedBy(projectile.velocity.ToRotation() + MathHelper.ToRadians(90));
					Main.spriteBatch.Draw(texture, center - Main.screenPosition + offset, null, new Color(255, 69, 0, 0) * bonusAlphaMult * ((255f - projectile.alpha) / 255f), projectile.velocity.ToRotation(), origin, 3f, SpriteEffects.None, 0.0f);
				}
			}
			return false;
		}
	}
}