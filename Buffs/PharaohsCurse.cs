using Terraria;
using Terraria.ModLoader;
 
namespace SOTS.Buffs
{
    public class PharaohsCurse : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Pharaoh's Curse");
			Description.SetDefault("Something is watching you, spawn rates increased");   
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
		{
			bool update = true;
			SOTSPlayer modPlayer = (SOTSPlayer)player.GetModPlayer(mod, "SOTSPlayer");
			if (SOTSWorld.downedBoss2 || modPlayer.weakerCurse)
			{
				update = false;	
			}
			if(update)
			{
				player.lifeRegen -= 100;
			}
			modPlayer.weakerCurse = false;
		}

    }
}