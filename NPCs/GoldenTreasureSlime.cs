using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace SOTS.NPCs
{
	public class GoldenTreasureSlime : ModNPC
	{	int initiateSize = 1;
		public override void SetStaticDefaults()
		{
			
			DisplayName.SetDefault("Golden Treasure Slime");
		}
		public override void SetDefaults()
		{
			//npc.CloneDefaults(NPCID.BlackSlime);
			npc.aiStyle = 1;
            npc.lifeMax = 150;  
            npc.damage = 45; 
            npc.defense = 7;  
            npc.knockBackResist = 0.5f;
            npc.width = 32;
            npc.height = 28;
            animationType = NPCID.BlueSlime;
			Main.npcFrameCount[npc.type] = 2;  
            npc.value = 10000;
            npc.npcSlots = .5f;
            npc.boss = false;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.netAlways = false;
			npc.alpha = 90;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
		}
		public override bool PreAI()
		{
			if(initiateSize == 1)
			{
				initiateSize = -1;
				npc.scale = 1.5f;
				npc.width = (int)(npc.width * npc.scale);
				npc.height = (int)(npc.height * npc.scale);
			}
			return true;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(NPC.downedBoss1){
				if(spawnInfo.spawnTileY <= Main.rockLayer){
					return SpawnCondition.Underground.Chance * 0.03f;
				}
				else if(spawnInfo.spawnTileY <= Main.maxTilesY - 200){
					return SpawnCondition.Cavern.Chance * 0.05f;
				}
			}
			return 0f;
		}
		public override void NPCLoot()
		{
		
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, (ItemID.Gel), Main.rand.Next(4) + 3);
			
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  mod.ItemType("MinersPickaxe"), Main.rand.Next(2) + 1);	
			
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  mod.ItemType("Peanut"), Main.rand.Next(12) + 1);	
			
			if(Main.rand.Next(9) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  mod.ItemType("MinersPickaxe"), Main.rand.Next(3) + 1);	
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  mod.ItemType("Peanut"), Main.rand.Next(4) + 1);	
			}
			
			if(Main.rand.Next(9) == 0 && Main.expertMode)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  mod.ItemType("MinersPickaxe"), Main.rand.Next(2) + 1);	
			}
			
			if(Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  mod.ItemType("MinersSword"), 1);		
			}
			if(Main.rand.Next(200) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  mod.ItemType("ShieldOfDesecar"), 1);		
			}
			if(Main.rand.Next(200) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  mod.ItemType("ShieldOfStekpla"), 1);		
			}
			if(Main.rand.Next(2) == 0 || Main.expertMode)
			{
				int rand = Main.rand.Next(30);
				if(rand == 0){
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.SpelunkerPotion, 1);
				}
				if(rand == 1)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.FeatherfallPotion, 1);
				}
				if(rand == 2)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.NightOwlPotion, 1);
				}
				if(rand == 3)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.WaterWalkingPotion, 1);
				}
				if(rand == 4)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.ArcheryPotion, 1);
				}
				if(rand == 5)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.GravitationPotion, 1);
				}
				if(rand == 6)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.ThornsPotion, 1);
				}
				if(rand == 7)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.InvisibilityPotion, 1);
				}
				if(rand == 8)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.HunterPotion, 1);
				}
				if(rand == 9)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  2329, 1); //dangersense
				}
				if(rand == 10)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.TeleportationPotion, 1);
				}
				if(rand == 11)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.RegenerationPotion, 1);
				}
				if(rand == 12)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.SwiftnessPotion, 1);
				}
				if(rand == 13)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.ShinePotion, 1);
				}
				if(rand == 14)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.IronskinPotion, 1);
				}
				if(rand == 15)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.MiningPotion, 1);
				}
				if(rand == 16)	{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.BuilderPotion, 1);
				}
				if(rand > 16){
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.HealingPotion, Main.rand.Next(2) + 2);
				}
			}

			if(Main.rand.Next(40) == 0 || (Main.expertMode && Main.rand.Next(30) == 0))
			{
				int rand = Main.rand.Next(13);
				if(rand == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.MiningShirt, 1);
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.MiningPants, 1);
				}
				if(rand == 1)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.BonePickaxe, 1);
				}
				if(rand == 2)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.WhoopieCushion, 1);
				}
				if(rand == 3)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.MetalDetector, 1);
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.DepthMeter, 1);
				}
				if(rand == 4)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.BandofRegeneration, 1);
				}
				if(rand == 5)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.MagicMirror, 1);
				}
				if(rand == 6)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.CloudinaBottle, 1);
				}
				if(rand == 7)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.HermesBoots, 1);
				}
				if(rand == 8)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.EnchantedBoomerang, 1);
				}
				if(rand == 9)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.ShoeSpikes, 1);
				}
				if(rand == 10)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.FlareGun, 1);
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.Flare, Main.rand.Next(26) + 25);
				}
				if(rand == 11)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.Extractinator, 1);
				}
				if(rand == 12)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,  ItemID.LavaCharm, 1);
				}
			}
		}	
	
	}
}