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
using SOTS.Void;

namespace SOTS.Projectiles.Base
{    
    public class HealProj : ModProjectile 
    {	
		private float amount {
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		private float type {
			get => projectile.ai[1];
			set => projectile.ai[1] = value;
		}

		private float extraNum1 {
			get => projectile.knockBack;
			set => projectile.knockBack = value;
		}
		
		private int healType {
			get => projectile.damage;
			set => projectile.damage = value;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heal Proj");
		}
        public override void SetDefaults()
        {
			projectile.height = 8;
			projectile.width = 8;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 720;
			projectile.friendly = false;
			projectile.hostile = false;
			projectile.alpha = 255;
		}
		private void genDust()
		{
			if((int)type == 0) //platinum staff
			{
				int num1 = Dust.NewDust(new Vector2(projectile.position.X , projectile.position.Y), projectile.width, projectile.height, 16);
				Main.dust[num1].noGravity = true;
				Main.dust[num1].velocity *= 0.1f;
			}
			if((int)type == 1) //crimson heal
			{
				int num1 = Dust.NewDust(new Vector2(projectile.position.X , projectile.position.Y), projectile.width, projectile.height, 60);
				Main.dust[num1].noGravity = true;
				Main.dust[num1].velocity *= 0.1f;
				Main.dust[num1].scale = 1.5f;
			}
			if((int)type == 2) //corruption void heal
			{
				int num1 = Dust.NewDust(new Vector2(projectile.position.X , projectile.position.Y), projectile.width, projectile.height, 62);
				Main.dust[num1].noGravity = true;
				Main.dust[num1].velocity *= 0.1f;
				Main.dust[num1].scale = 1.5f;
			}
			if((int)type == 3) //corruption mana heal
			{
				int num1 = Dust.NewDust(new Vector2(projectile.position.X , projectile.position.Y), projectile.width, projectile.height, 15);
				Main.dust[num1].noGravity = true;
				Main.dust[num1].velocity *= 0.1f;
				Main.dust[num1].scale = 1.5f;
			}
			if((int)type == 4) //ice
			{
				int num1 = Dust.NewDust(new Vector2(projectile.position.X , projectile.position.Y), projectile.width, projectile.height, 67);
				Main.dust[num1].noGravity = true;
				Main.dust[num1].velocity *= 0.1f;
				if(projectile.timeLeft % 10 == 0)
				{
					additionalEffects();
				}
			}
			if((int)type == 5) //Hungry Hunter / default
			{
				int num1 = Dust.NewDust(new Vector2(projectile.position.X , projectile.position.Y), projectile.width, projectile.height, 37);
				Main.dust[num1].noGravity = true;
				Main.dust[num1].velocity *= 0.1f;
			}
			if((int)type == 6) //Clover Charm
			{
				int num1 = Dust.NewDust(new Vector2(projectile.position.X , projectile.position.Y), projectile.width, projectile.height, 2);
				Main.dust[num1].noGravity = true;
				Main.dust[num1].velocity *= 0.1f;
			}
		}
		private float getSpeed()
		{
			if((int)type == 4)
			{
				return 10f;
			}
			if((int)type == 5) 
			{
				return 16.25f;
			}
			if((int)type == 6) //Clover Charm
			{
				return 11f;
			}
			return 14.5f;
		}
		private float getMinDist()
		{
			return 20f;
		}
		private void additionalEffects()
		{
			if((int)type == 4)
			{
				for(int i = 0; i < 360; i += 30)
				{
					Vector2 circularLocation = new Vector2(4, 0).RotatedBy(MathHelper.ToRadians(i));
					int num1 = Dust.NewDust(new Vector2(projectile.Center.X + circularLocation.X - 4, projectile.Center.Y + circularLocation.Y - 4), 4, 4, 67);
					Main.dust[num1].noGravity = true;
					Main.dust[num1].velocity = -projectile.velocity;
				}
			}
		}
		public override void Kill(int timeLeft)
		{
			
		}
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if(projectile.timeLeft < 720)
			{
				genDust();
				projectile.velocity = new Vector2(-getSpeed(), 0).RotatedBy(Math.Atan2(projectile.Center.Y - player.Center.Y, projectile.Center.X - player.Center.X));
				
				Vector2 toPlayer = player.Center - projectile.Center;
				float distance = toPlayer.Length();
				if(distance < getMinDist())
				{
					if(healType == 0)
					{
						player.statLife += (int)amount;
						if(player.whoAmI == Main.myPlayer)
						player.HealEffect((int)amount);
					}
					if(healType == 1)
					{
						player.statMana += (int)amount;
						if(player.whoAmI == Main.myPlayer)
						player.ManaEffect((int)amount);
					}
					if(healType == 2)
					{
						VoidPlayer voidPlayer = VoidPlayer.ModPlayer(player);
						voidPlayer.voidMeter += amount;
						VoidPlayer.VoidEffect(player, (int)(amount + 0.5f));
					}
					projectile.Kill();
				}
			}
		}
	}
}
		