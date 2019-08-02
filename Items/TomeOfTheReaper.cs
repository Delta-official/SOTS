using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace SOTS.Items
{
	public class TomeOfTheReaper : ModItem
	{	int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tome of the Reaper");
			Tooltip.SetDefault("Casts many demon scythes");
		}
		public override void SetDefaults()
		{
            item.damage = 48; 
            item.magic = true; 
            item.width = 28;   
            item.height = 30;   
            item.useTime = 7;   
            item.useAnimation = 21;
            item.useStyle = 5;    
            item.noMelee = true;  
            item.knockBack = 5.5f;
            item.value = 250000;
            item.rare = 6;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = ProjectileID.DemonScythe; 
            item.shootSpeed = 9.5f;
			item.mana = 16;
			item.reuseDelay = 14;

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemonScythe, 1);
			recipe.AddIngredient(ItemID.SoulofFright, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
         {
				counter++;
				
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
				if(counter >= 3)
				{
					counter = 0;
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(10)); 
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);

					perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(-10));
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
				return false; 
		}
	}
}
