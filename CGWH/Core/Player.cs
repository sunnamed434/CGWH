using CGWH.Core.Other;

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



        internal static void SetFlashAlpha(float value) => Cheat.Memory.Write<float>(Local + Offsets.m_flFlashMaxAlpha, value);

        internal static void SetFlashAlphaByDefault() => Cheat.Memory.Write<float>(Local + Offsets.m_flFlashMaxAlpha, 255f);

        internal static void SetThirdPersonView() => Cheat.Memory.Write<int>(Offsets.m_iObserverMode + Local, 1);

        internal static void SetFirstPersonView() => Cheat.Memory.Write<int>(Offsets.m_iObserverMode + Local, 0);

        internal static void SetFov(int value) => Cheat.Memory.Write<int>(Local + Offsets.m_iDefaultFOV, value);

        internal static void SetFovByDefault() => Cheat.Memory.Write<int>(Local + Offsets.m_iDefaultFOV, 90);

        internal static bool TryGetCrosshairTrigger(out CrosshairParameters parameters)
        {
            bool result = false;

            parameters = null;

            int triggerValue = 0;
            if ((triggerValue = Cheat.Memory.Read<int>(Local + Offsets.m_iCrosshairId)) > 0)
            {
                int enemy = Cheat.Memory.Read<int>(Cheat.ModuleAddress + Offsets.dwEntityList + (Crosshair - 1) * 0x10);

                int enemyTeam = Cheat.Memory.Read<int>(enemy + Offsets.m_iTeamNum);

                parameters = new CrosshairParameters(enemy, enemyTeam, triggerValue);
                result = true;
            }

            return result;
        }

        internal static void Attack() => Cheat.Memory.Write<int>(Cheat.ModuleAddress + Offsets.dwForceAttack, 6);

        internal static void Jump(int force = 6) => Cheat.Memory.Write<int>(Cheat.ModuleAddress + Offsets.dwForceJump, force);
    }
}
