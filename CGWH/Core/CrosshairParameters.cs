namespace CGWH.Core.Other
{
    internal class CrosshairParameters
    {
        internal int TriggerEnemy { get; }

        internal int TriggerEnemyTeam { get; }

        internal int TriggerValue { get; }



        internal CrosshairParameters(int triggerEnemy, int enemyTeam, int triggerValue)
        {
            TriggerEnemy = triggerEnemy;
            TriggerEnemyTeam = enemyTeam;
            TriggerValue = triggerValue;
        }



        internal bool TriggerIsTeammate() => Player.TeamNum == TriggerEnemyTeam;
    }
}
