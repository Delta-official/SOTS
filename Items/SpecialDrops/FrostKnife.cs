using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Items.SpecialDrops
{
	public class FrostKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frost Knife");
			Tooltip.SetDefault("Right click to heal\nFound in puddles of icy water");
		}
		public override void SetDefaults()
		{
            item.damage = 32;  //gun damage
            item.melee = true;   //its a gun so set this to true
            item.width = 34;     //gun image width
            item.height = 38;   //gun image  height
            item.useTime = 60;  //how fast 
            item.useAnimation = 9;
            item.useStyle = 1;    
            item.knockBack = 2.4f;
            item.value = 75000;
            item.rare = 5;
            item.UseSound = SoundID.Item18;
            item.autoReuse = true;
			item.useTurn = true;
			item.shootSpeed = 22;
			item.shoot = mod.ProjectileType("PikeProj");
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int typse, ref int damage, ref float knockBack)
        {
				if(player.altFunctionUse == 2)
				{
			
                  Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2)); // This defines the projectiles random spread . 30 degree spread.
				  
				  player.statLife += 10;
				  player.HealEffect(10);
				  
					for(int i = 1; i < 325; i++)
					Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 58);
				return false;
				}
				else
				{
					return true;
				}
				
			
			
		}
		 public override bool AltFunctionUse(Player player)
        {
            return true;
        }
 
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)     //2 is right click
            {
			item.noUseGraphic = true;
            item.useStyle = 5; 
            }
            else
            {
			item.noUseGraphic = false;
            item.useStyle = 1;    
            }
            return base.CanUseItem(player);
        }
	}
}
