using System;
using Terraria;
using Terraria.ModLoader;
 
namespace SOTS.Buffs
{
    public class Nightmare : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Nightmare");
			Description.SetDefault("Compress enemies together with critical strikes");   
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SOTSPlayer modPlayer = SOTSPlayer.ModPlayer(player);
            modPlayer.CritNightmare = true;
        }
    }
}