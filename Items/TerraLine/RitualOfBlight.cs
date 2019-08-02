using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.Items.TerraLine
{
	public class RitualOfBlight  : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ritual Of Blight");
			Tooltip.SetDefault("Summons a ball of pure evil on your cursor\nA extra auro surrounds the ball of pure evil");
		}
		public override void SetDefaults()
		{
            item.damage = 50;  //gun damage
            item.magic = true;   //its a gun so set this to true
            item.width = 24;     //gun image width
            item.height = 30;   //gun image  height
            item.useTime = 1;  //how fast 
            item.useAnimation = 4;
            item.useStyle = 5;    
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 0;
            item.value = 30000;
            item.rare = 9;
            item.UseSound = SoundID.Item13;
            item.autoReuse = true;
            item.shoot =  mod.ProjectileType("TerraShadow"); 
            item.shootSpeed = 24;
			item.expert = true;

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TrueHallowStaff", 1);
			recipe.AddIngredient(null, "TrueHorcruxSpell", 1);
			recipe.AddIngredient(null, "TheHardCore", 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
          public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
          {
			  
			  Vector2 vector14;
					
						if (player.gravDir == 1f)
					{
					vector14.Y = (float)Main.mouseY + Main.screenPosition.Y;
					}
					else
					{
					vector14.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
					}
						vector14.X = (float)Main.mouseX + Main.screenPosition.X;
                Projectile.NewProjectile(vector14.X,  vector14.Y, 0, 0, type, damage, 1, Main.myPlayer, 0.0f, 1);

            
					return false;
					
          }  
	}
}
