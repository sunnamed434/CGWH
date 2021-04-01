using CGWH.Core.Other;
using System;

namespace CGWH.Core
{
	internal sealed class Player
	{
		internal static int Local => Cheat.Memory.Read<int>(Cheat.ModuleAddress + Offsets.dwLocalPlayer);

		internal static int TeamNum => Cheat.Memory.Read<int>(Local + Offsets.m_iTeamNum);

		internal static int Crosshair => Cheat.Memory.Read<int>(Local + Offsets.m_iCrosshairId);

		internal static float FlashAlpha => Cheat.Memory.Read<float>(Local + Offsets.m_flFlashMaxAlpha);

		internal static int Flags => Cheat.Memory.Read<int>(Local + Offsets.m_fFlags);

		internal static bool IsGround => Flags == 257 || Flags == 263;

		internal static bool IsScoping => Cheat.Memory.Read<int>(Local + Offsets.m_bIsScoped) == 1;



		#region - Weapon -

		internal static int HandsItemId
		{
			get
			{
				int activeWeapon = Cheat.Memory.Read<int>(Player.Local + Offsets.m_hActiveWeapon);

				int weapon = Cheat.Memory.Read<int>(Cheat.ModuleAddress + Offsets.dwEntityList + ((activeWeapon & 0xFFF) - 1) * 0x10);

				int index = Cheat.Memory.Read<int>(weapon + Offsets.m_iItemDefinitionIndex);

				return index;
			}
		}

		internal static Item HandsWeapon => (Item)HandsItemId;

		internal static bool HasHandsWeapon => !HasHandsPistol && !HasHandsGrenade && !HasHandsC4;

		internal static bool HasHandsPistol =>
			HandsItemId == (int)Pistol.WEAPON_CZ75A || HandsItemId == (int)Pistol.WEAPON_DEAGLE ||
			HandsItemId == (int)Pistol.WEAPON_FIVESEVEN || HandsItemId == (int)Pistol.WEAPON_GLOCK ||
			HandsItemId == (int)Pistol.WEAPON_HKP2000 || HandsItemId == (int)Pistol.WEAPON_P250 ||
			HandsItemId == (int)Pistol.WEAPON_REVOLVER || HandsItemId == (int)Pistol.WEAPON_TEC9 ||
			HandsItemId == (int)Pistol.WEAPON_USP_SILENCER || HandsItemId == (int)Pistol.WEAPON_DUAL_BERRETS;

		internal static bool HasHandsGrenade =>
			HandsItemId == (int)Grenade.WEAPON_DECOY || HandsItemId == (int)Grenade.WEAPON_FLASHBANG ||
			HandsItemId == (int)Grenade.WEAPON_HEGRENADE || HandsItemId == (int)Grenade.WEAPON_INCGRENADE ||
			HandsItemId == (int)Grenade.WEAPON_MOLOTOV || HandsItemId == (int)Grenade.WEAPON_SMOKEGRENADE;

		internal static bool HasHandsC4 => HandsItemId == (int)Other.WEAPON_C4;

		internal static bool HasHandsKnife =>
			HandsItemId == (int)Knife.WEAPON_KNIFE || HandsItemId == (int)Knife.WEAPON_KNIFE_BAYONET ||
			HandsItemId == (int)Knife.WEAPON_KNIFE_BUTTERFLY || HandsItemId == (int)Knife.WEAPON_KNIFE_FALCHION ||
			HandsItemId == (int)Knife.WEAPON_KNIFE_FLIP || HandsItemId == (int)Knife.WEAPON_KNIFE_GUT ||
			HandsItemId == (int)Knife.WEAPON_KNIFE_KARAMBIT || HandsItemId == (int)Knife.WEAPON_KNIFE_M9_BAYONET ||
			HandsItemId == (int)Knife.WEAPON_KNIFE_PUSH || HandsItemId == (int)Knife.WEAPON_KNIFE_T ||
			HandsItemId == (int)Knife.WEAPON_KNIFE_TACTICAL;

		#endregion



		internal static void SetFlashAlpha(float value) => Cheat.Memory.Write<float>(Local + Offsets.m_flFlashMaxAlpha, value);

		internal static void SetFlashAlphaByDefault() => Cheat.Memory.Write<float>(Local + Offsets.m_flFlashMaxAlpha, 255f);

		internal static void SetThirdPersonView() => Cheat.Memory.Write<int>(Offsets.m_iObserverMode + Local, 1);

		internal static void SetFirstPersonView() => Cheat.Memory.Write<int>(Offsets.m_iObserverMode + Local, 0);

		internal static void SetFov(int value) => Cheat.Memory.Write<int>(Local + Offsets.m_iDefaultFOV, value);

		internal static void SetFovByDefault() => Cheat.Memory.Write<int>(Local + Offsets.m_iDefaultFOV, 90);

		internal static bool TryGetCrosshairEnemyTrigger(out CrosshairParameters parameters)
		{
			bool result = false;

			parameters = null;

			int triggerValue = 0;
			if ((triggerValue = Cheat.Memory.Read<int>(Local + Offsets.m_iCrosshairId)) > 0)
			{
				if (triggerValue >= 0 && triggerValue <= 64)
                {
					int enemy = Cheat.Memory.Read<int>(Cheat.ModuleAddress + Offsets.dwEntityList + (Crosshair - 1) * 0x10);

					int enemyTeam = Cheat.Memory.Read<int>(enemy + Offsets.m_iTeamNum);

					parameters = new CrosshairParameters(enemy, enemyTeam, triggerValue);
					result = true;
				}
			}

			return result;
		}

		internal static void Attack() => Cheat.Memory.Write<int>(Cheat.ModuleAddress + Offsets.dwForceAttack, 6);

		internal static void Jump(int force = 6) => Cheat.Memory.Write<int>(Cheat.ModuleAddress + Offsets.dwForceJump, force);



		internal enum Item : int
		{
			WEAPON_DEAGLE = 1,
			WEAPON_ELITE = 2,
			WEAPON_FIVESEVEN = 3,
			WEAPON_GLOCK = 4,
			WEAPON_AK47 = 7,
			WEAPON_AUG = 8,
			WEAPON_AWP = 9,
			WEAPON_FAMAS = 10,
			WEAPON_G3SG1 = 11,
			WEAPON_GALILAR = 13,
			WEAPON_M249 = 14,
			WEAPON_M4A1 = 16,
			WEAPON_MAC10 = 17,
			WEAPON_P90 = 19,
			WEAPON_UMP45 = 24,
			WEAPON_XM1014 = 25,
			WEAPON_BIZON = 26,
			WEAPON_MAG7 = 27,
			WEAPON_NEGEV = 28,
			WEAPON_SAWEDOFF = 29,
			WEAPON_TEC9 = 30,
			WEAPON_TASER = 31,
			WEAPON_HKP2000 = 32,
			WEAPON_MP7 = 33,
			WEAPON_MP9 = 34,
			WEAPON_NOVA = 35,
			WEAPON_P250 = 36,
			WEAPON_SCAR20 = 38,
			WEAPON_SG556 = 39,
			WEAPON_SSG08 = 40,
			WEAPON_KNIFE = 42,
			WEAPON_FLASHBANG = 43,
			WEAPON_HEGRENADE = 44,
			WEAPON_SMOKEGRENADE = 45,
			WEAPON_MOLOTOV = 46,
			WEAPON_DECOY = 47,
			WEAPON_INCGRENADE = 48,
			WEAPON_C4 = 49,
			WEAPON_KNIFE_T = 59,
			WEAPON_M4A1_SILENCER = 60,
			WEAPON_CZ75A = 63,
			WEAPON_REVOLVER = 64,
			WEAPON_KNIFE_BAYONET = 500,
			WEAPON_KNIFE_FLIP = 505,
			WEAPON_KNIFE_GUT = 506,
			WEAPON_KNIFE_KARAMBIT = 507,
			WEAPON_KNIFE_M9_BAYONET = 508,
			WEAPON_KNIFE_TACTICAL = 509,
			WEAPON_KNIFE_FALCHION = 512,
			WEAPON_KNIFE_BUTTERFLY = 515,
			WEAPON_KNIFE_PUSH = 516,
			WEAPON_USP_SILENCER = 262205
		}

		internal enum Weapon : int
		{
			WEAPON_AK47 = 7,
			WEAPON_AUG = 8,
			WEAPON_AWP = 9,
			WEAPON_FAMAS = 10,
			WEAPON_G3SG1 = 11,
			WEAPON_GALILAR = 13,
			WEAPON_M249 = 14,
			WEAPON_M4A1 = 16,
			WEAPON_MAC10 = 17,
			WEAPON_P90 = 19,
			WEAPON_UMP45 = 24,
			WEAPON_XM1014 = 25,
			WEAPON_BIZON = 26,
			WEAPON_MAG7 = 27,
			WEAPON_NEGEV = 28,
			WEAPON_SAWEDOFF = 29,
			WEAPON_MP7 = 33,
			WEAPON_MP9 = 34,
			WEAPON_NOVA = 35,
			WEAPON_SCAR20 = 38,
			WEAPON_SG556 = 39,
			WEAPON_SSG08 = 40,
			WEAPON_M4A1_SILENCER = 60
		}

		internal enum Pistol : int
		{
			WEAPON_DEAGLE = 1,
			WEAPON_DUAL_BERRETS = 2,
			WEAPON_FIVESEVEN = 3,
			WEAPON_GLOCK = 4,
			WEAPON_TEC9 = 30,
			WEAPON_HKP2000 = 32,
			WEAPON_P250 = 36,
			WEAPON_CZ75A = 63,
			WEAPON_REVOLVER = 64,
			WEAPON_USP_SILENCER = 262205
		}

		internal enum Grenade : int
		{
			WEAPON_FLASHBANG = 43,
			WEAPON_HEGRENADE = 44,
			WEAPON_SMOKEGRENADE = 45,
			WEAPON_MOLOTOV = 46,
			WEAPON_DECOY = 47,
			WEAPON_INCGRENADE = 48
		}

		internal enum Knife : int
		{
			WEAPON_KNIFE = 42,
			WEAPON_KNIFE_T = 59,
			WEAPON_KNIFE_BAYONET = 500,
			WEAPON_KNIFE_FLIP = 505,
			WEAPON_KNIFE_GUT = 506,
			WEAPON_KNIFE_KARAMBIT = 507,
			WEAPON_KNIFE_M9_BAYONET = 508,
			WEAPON_KNIFE_TACTICAL = 509,
			WEAPON_KNIFE_FALCHION = 512,
			WEAPON_KNIFE_BUTTERFLY = 515,
			WEAPON_KNIFE_PUSH = 516
		}

		internal enum Other : int
		{
			WEAPON_C4 = 49,
			WEAPON_TASER = 31
		}
	}
}
