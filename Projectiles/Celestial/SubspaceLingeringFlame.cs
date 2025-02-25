using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOTS.Buffs;
using SOTS.Dusts;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Projectiles.Celestial
{
	public class SubspaceLingeringFlame : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire Bolt");
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.timeLeft = 120;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
		}
		List<FireParticle> particleList = new List<FireParticle>();
		public override bool PreAI()
		{
			cataloguePos();
			Vector2 rotational = new Vector2(0, -1.8f).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-30f, 30f)));
			rotational.X *= 0.25f;
			rotational.Y *= 0.75f;
			rotational += projectile.velocity;
			rotational = rotational.SafeNormalize(Vector2.Zero) * 3f;
			particleList.Add(new FireParticle(projectile.Center - rotational * 2, rotational, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(0.9f, 1.1f)));
			return base.PreAI();
		}
		public void cataloguePos()
		{
			for (int i = 0; i < particleList.Count; i++)
			{
				FireParticle particle = particleList[i];
				particle.Update();
				if(!particle.active)
                {
					particleList.RemoveAt(i);
					i--;
                }
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
			for (int i = 0; i < particleList.Count; i++)
			{
				Color color = new Color(255, 69, 0, 0);
				Vector2 drawPos = particleList[i].position - Main.screenPosition ;
				color = projectile.GetAlpha(color) * (0.35f + 0.65f * particleList[i].scale);
				for (int j = 0; j < 2; j++)
				{
					float x = Main.rand.NextFloat(-2f, 2f);
					float y = Main.rand.NextFloat(-2f, 2f);
					Main.spriteBatch.Draw(texture, drawPos + new Vector2(x, y), null, color, particleList[i].rotation, drawOrigin, particleList[i].scale, SpriteEffects.None, 0f);
				}
			}
			return false;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 3; i++)
			{
				int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<CopyDust4>());
				Dust dust = Main.dust[dust2];
				dust.color = new Color(255, 75, 0, 0);
				dust.noGravity = true;
				dust.fadeIn = 0.1f;
				dust.scale *= 2.25f;
				dust.velocity *= 0.9f;
				dust.alpha = 125;
			}
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 360, false);
		}
		public override bool ShouldUpdatePosition()
		{
			return true;
		}
		public override void AI()
		{
			projectile.velocity *= 0.98f;
			projectile.velocity.Y += 0.14f;
			if (projectile.timeLeft <= 51)
				projectile.alpha += 5;
			if (projectile.timeLeft <= 15)
				projectile.hostile = false;
			Lighting.AddLight(projectile.Center, 0.75f, 0.25f, 0.0f);
			projectile.ai[0]++;
		}
	}
	public class DimensionalFlame : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dimensional Flame");
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.hostile = false;
			projectile.friendly = false;
			projectile.timeLeft = 180;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
		}
		List<FireParticle> particleList = new List<FireParticle>();
		public override bool PreAI()
		{
			cataloguePos();
			Vector2 rotational = new Vector2(0, -1.8f).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-30f, 30f)));
			rotational.X *= 0.25f;
			rotational.Y *= 0.75f;
			rotational += projectile.velocity;
			rotational = rotational.SafeNormalize(Vector2.Zero) * 3f;
			particleList.Add(new FireParticle(projectile.Center - rotational * 2, rotational, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(0.9f, 1.1f)));
			return base.PreAI();
		}
		public void cataloguePos()
		{
			for (int i = 0; i < particleList.Count; i++)
			{
				FireParticle particle = particleList[i];
				particle.Update();
				if (!particle.active)
				{
					particleList.RemoveAt(i);
					i--;
				}
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
			for (int i = 0; i < particleList.Count; i++)
			{
				Color color = new Color(75, 255, 33, 0);
				Vector2 drawPos = particleList[i].position - Main.screenPosition;
				color = projectile.GetAlpha(color) * (0.35f + 0.65f * particleList[i].scale);
				for (int j = 0; j < 2; j++)
				{
					float x = Main.rand.NextFloat(-2f, 2f);
					float y = Main.rand.NextFloat(-2f, 2f);
					Main.spriteBatch.Draw(texture, drawPos + new Vector2(x, y), null, color, particleList[i].rotation, drawOrigin, particleList[i].scale * 1.3f, SpriteEffects.None, 0f);
				}
			}
			return false;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 4; i++)
			{
				int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<CopyDust4>());
				Dust dust = Main.dust[dust2];
				dust.color = new Color(75, 255, 33, 0);
				dust.noGravity = true;
				dust.fadeIn = 0.1f;
				dust.scale *= 2.25f;
				dust.velocity *= 0.9f;
				dust.alpha = 125;
			}
		}
		public override bool ShouldUpdatePosition()
		{
			return true;
		}
		public override void AI()
		{
			projectile.velocity *= 0.965f;
			if (projectile.timeLeft <= 51)
				projectile.alpha += 5;
			if (projectile.timeLeft <= 15)
				projectile.hostile = false;
			Lighting.AddLight(projectile.Center, 0.25f, 0.75f, 0.0f);
			projectile.ai[0]++;
		}
	}
	public class FireParticle
	{
		public Vector2 position;
		public Vector2 velocity;
		public float rotation;
		public float nextRotation;
		public float mult;
		public FireParticle()
		{
			position = Vector2.Zero;
			velocity = Vector2.Zero;
			rotation = 0;
			nextRotation = 0;
			scale = 1;
			mult = Main.rand.NextFloat(0.9f, 1.1f);
		}
		public FireParticle(Vector2 position, Vector2 velocity, float rotation, float nextRotation, float scale)
		{
			this.position = position;
			this.velocity = velocity;
			this.rotation = rotation;
			this.nextRotation = nextRotation;
			this.scale = scale;
			mult = Main.rand.NextFloat(0.9f, 1.1f);
		}
		public float counter = 0;
		public float scale;
		public bool active = true;
		public void Update()
		{
			counter++;
			float veloMult = 0.6f + 0.4f * counter / 15f;
			if (veloMult > 1)
				veloMult = 1f;
			position += velocity * veloMult;
			for(int i = 0; i < 1 + (int)(Main.rand.NextFloat(1f) * mult); i++)
			{
				velocity.Y *= 0.95f;
				velocity.X *= 0.98f;
				scale *= 0.95f;
			}
			if (counter < 31f)
				rotation += nextRotation / 30f;
			if (scale <= 0.05f)
				active = false;	
		}
	}
}