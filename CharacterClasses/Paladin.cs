namespace AutoBattle
{
    public class Paladin : Character
    {
        public Paladin(GridCell startingPosition, CharacterTeam team, int index)
        : base(startingPosition, team, index) { }

        protected override void SetBaseStats()
        {
            Health = 100;
            BaseDamage = 20;
        }
    }
}