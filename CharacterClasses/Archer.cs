namespace AutoBattle
{
    public class Archer : Character
    {
        public Archer(GridCell startingPosition, CharacterTeam team, int index)
        : base(startingPosition, team, index) { }

        protected override void SetBaseStats()
        {
            Health = 100;
            BaseDamage = 20;
        }
    }
}