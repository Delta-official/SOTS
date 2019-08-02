using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SOTS.NPCs.Spectre
{
	public class SpectrePrimeVice: ModNPC
	{	int restrictor = 0;
	int despawn = 0;
		public override void SetStaticDefaults()
		{
			
			DisplayName.SetDefault("Detached Vice");
		}
		public override void SetDefaults()
		{
			
            npc.aiStyle = 56;  //5 is the flying AI
            npc.lifeMax = 64000;   //boss life
            npc.damage = 240;  //boss damage
            npc.defense = 999;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 92;
            npc.height = 46;
            animationType = NPCID.FlyingFish;   //this boss will behavior like the DemonEye
            Main.npcFrameCount[npc.type] = 4;    //boss frame/animation
            npc.value = 10000;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.buffImmune[24] = true;
            music = MusicID.PumpkinMoon;
            npc.netAlways = true;
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.5f);  //boss damage increase in expermode
        }
		
		
		public override void AI()
		{	
		 Player player  = Main.player[npc.target];
			if(!NPC.AnyNPCs(mod.NPCType("SpectrePrime")))
			{
				
				npc.defense = 0;
				
				if(restrictor == 0)
				{
					restrictor += 1;
				}
			}
			
			
			Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), 1, 1, 160);
		
			if(Main.player[npc.target].dead)
			{
			 despawn++;
			}
			if(despawn >= 720)
			{
			npc.active = false;
			}
		}
	
	}
}





















