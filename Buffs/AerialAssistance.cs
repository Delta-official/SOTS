using Terraria;
using Terraria.ModLoader;
 
namespace SOTS.Buffs
{
    public class AerialAssistance : ModBuff
    {
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Aerial Assistance");
			Description.SetDefault("Penguins assist you in combat");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex) 
		{
			if (player.ownedProjectileCounts[mod.ProjectileType("PenguinCopter")] > 0) 
			{
				player.buffTime[buffIndex] = 18000;
			}
			else 
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
    }
}