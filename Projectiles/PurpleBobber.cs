using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
 
namespace SOTS.Projectiles
{
    public class PurpleBobber : ModProjectile
    {	
		int rodBobberType = -1;
        public override void SetStaticDefaults()
		{
			
		}
        public override void SetDefaults()
        {
			
			
			projectile.bobber = true;
			projectile.width = 4;
			projectile.height = 24;
			projectile.penetrate = -1;
			projectile.timeLeft = 2000;
        }
		public override bool PreAI()
		{
			
			if(rodBobberType == -1)
			{
			rodBobberType = (int)projectile.knockBack;
			string rodBobberString = rodBobberType.ToString();
			//projectile.CloneDefaults((int)projectile.knockBack);
			projectile.aiStyle = 61;
			aiType = (int)projectile.knockBack;
			//Main.NewText(rodBobberString, 125, 145, 125);
			projectile.timeLeft = 2000;
			return false;
			}
			return true;
		}
        public override bool PreDrawExtras(SpriteBatch spriteBatch)    
        {	
		
			
				//Lighting.AddLight(projectile.Center, 0.7f, 0.9f, 0.6f);
				Player owner = null;
				if (projectile.owner != -1)
				{
					owner = Main.player[projectile.owner];
				}
				else if (projectile.owner == 255)
				{
					owner = Main.LocalPlayer;
				}
				var player = owner;
                float pPosX = player.MountedCenter.X;
                float pPosY = player.MountedCenter.Y;
			for(int i = 0; i < 1000; i++)
			{
				Projectile balloon = Main.projectile[i];
				if(balloon.type == mod.ProjectileType("LuckyPurpleBalloon") && balloon.owner == projectile.owner)
				{
					pPosX = balloon.position.X;
					pPosY = balloon.Center.Y + 2;
				}
			}
			
            if (projectile.bobber && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].holdStyle > 0)
            {
                //pPosY += Main.player[projectile.owner].gfxOffY;
                int type = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].type;
               // float gravDir = Main.player[projectile.owner].gravDir;
 
                /*if (type == mod.ItemType("GooFishingPole"))
                {
                    pPosX += (float)(50 * Main.player[projectile.owner].direction);
                    if (Main.player[projectile.owner].direction < 0)
                    {
                        pPosX -= 13f;
                    }
                    pPosY -= 30f * gravDir;
                }
 
                if (gravDir == -1f)
                {
                    pPosY -= 12f;
                }
				*/
                Vector2 value = new Vector2(pPosX, pPosY);
                //value = Main.player[projectile.owner].RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
                float projPosX = projectile.Center.X;
                float projPosY = projectile.Center.Y;
                Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                float rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX);
                bool flag2 = true;
                if (projPosX == 0f && projPosY == 0f)
                {
                    flag2 = false;
                }
                else
                {
                    float projPosXY = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                    projPosXY = 12f / projPosXY;
                    projPosX *= projPosXY;
                    projPosY *= projPosXY;
                    //value.X -= projPosX;
                    //value.Y -= projPosY;
                    projPosX = projectile.Center.X;
                    projPosY = projectile.Center.Y;
                }
                while (flag2)
                {
                    float num = 12f;
                    float num2 = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                    float num3 = num2;
                    if (float.IsNaN(num2) || float.IsNaN(num3))
                    {
                        flag2 = false;
                    }
                    else
                    {
                        if (num2 < 20f)
                        {
                            num = num2 - 8f;
                            flag2 = false;
                        }
                        num2 = 12f / num2;
                        projPosX *= num2;
                        projPosY *= num2;
                        value.X += projPosX;
                        value.Y += projPosY;
                        projPosX = projectile.position.X + (float)projectile.width * 0.5f - value.X;
                        projPosY = projectile.position.Y + (float)projectile.height * 0.1f - value.Y;
                        if (num3 > 12f)
                        {
                            float num4 = 0.3f;
                            float num5 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
                            if (num5 > 16f)
                            {
                                num5 = 16f;
                            }
                            num5 = 1f - num5 / 16f;
                            num4 *= num5;
                            num5 = num3 / 80f;
                            if (num5 > 1f)
                            {
                                num5 = 1f;
                            }
                            num4 *= num5;
                            if (num4 < 0f)
                            {
                                num4 = 0f;
                            }
                            num5 = 1f - projectile.localAI[0] / 100f;
                            num4 *= num5;
                            if (projPosY > 0f)
                            {
                                projPosY *= 1f + num4;
                                projPosX *= 1f - num4;
                            }
                            else
                            {
                                num5 = Math.Abs(projectile.velocity.X) / 3f;
                                if (num5 > 1f)
                                {
                                    num5 = 1f;
                                }
                                num5 -= 0.5f;
                                num4 *= num5;
                                if (num4 > 0f)
                                {
                                    num4 *= 2f;
                                }
                                projPosY *= 1f + num4;
                                projPosX *= 1f - num4;
                            }
                        }
                        rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
                        Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Microsoft.Xna.Framework.Color(184, 147, 188));    //fishing line color
 
                        Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(value.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, value.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num)), color2, rotation2, new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                    }
                }
            }
					return false;
		}
    }
}