using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SOTS.Void
{
	
	public class VoidPlayer : ModPlayer
	{
		public int voidMeterMax = 100;
		private const int voidCrystalMax = 10;
		public override TagCompound Save() {
				
			return new TagCompound {
				
				{"voidMeterMax", voidMeterMax},
				};
		}

		public override void Load(TagCompound tag) 
		{
			voidMeterMax = tag.GetInt("voidMeterMax");
		
		}
		
		public float voidMeter = 0; 
		public float voidRegen = 0.0035f; 
		public float voidCost = 1f; 
		public float voidSpeed = 1f; 
		public int voidMeterMax2 = 0;
		
		public static VoidPlayer ModPlayer(Player player) {
			return player.GetModPlayer<VoidPlayer>();
		}

		public float voidDamage = 1f;
		public float voidKnockback;
		public int voidCrit;

		public override void ResetEffects() {
			ResetVariables();
		}

		public override void UpdateDead() {
			voidMeter = 0;
			ResetVariables();
		}

		private void ResetVariables() {
			//make sure damage is 100% before making modifications
			voidDamage = 1f;
			
			//percent damage grows as health lowers
			voidDamage += 1f - (float)((float)player.statLife / (float)player.statLifeMax2);
			
			voidSpeed = 1f; 
			voidCost = 1f; 
			voidMeter += voidRegen;
			
			if(voidMeter > voidMeterMax2)
			{
				//make sure meter doesn't go above max
				voidMeter = voidMeterMax2;
			}
			
			voidMeterMax2 = voidMeterMax;
			
			voidKnockback = 0f;
			voidCrit = 0;
			voidRegen = 0.0035f; 
			
			if(voidMeter != 0)
			{
				VoidUI.visible = true;
			}
		}
		public override void PostUpdateBuffs()
		{
			if(voidMeter < 0)
			{
				
			player.lifeRegen += (int)(voidMeter * 1f);
			voidMeter += -player.lifeRegen * 0.001f;
			
			}
		}
	}
}