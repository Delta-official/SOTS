using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace SOTS.Items
{
	public class PolarisStarIIIIII : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Spur");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
            item.damage = 32;  //gun damage
            item.magic = true;   //its a gun so set this to true
            item.width = 40;     //gun image width
            item.height = 20;   //gun image  height
            item.useTime = 4;  //how fast 
            item.useAnimation = 4;
            item.useStyle = 5;    
            item.noMelee = false; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = 575000;
            item.rare = 9;
            item.UseSound = SoundID.Item12;
            item.autoReuse = true;
            item.shoot = 440; 
            item.shootSpeed = 35;
			item.expert = true;
			item.mana = 5;
			item.channel = false;   
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TheHardCore", 1);
			recipe.AddIngredient(null, "PolarisStarIIIII", 1);
			recipe.AddIngredient(null, "TomeOfTheReaper", 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
          {
              int numberProjectiles = 4;
			  for (int i = 0; i < numberProjectiles; i++)
              {
                  Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(1)); // This defines the projectiles random spread . 30 degree spread.
                  Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				  
              }
              return false; 
		  }
	}
}
