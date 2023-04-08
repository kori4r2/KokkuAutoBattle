namespace AutoBattle
{
    public class Paladin : Character
    {
        public Paladin(GridCell startingPosition, CharacterTeam team, int index)
        : base(startingPosition, team, index) { }

        protected override CharacterClass MyClass => CharacterClass.Paladin;

        protected override void SetBaseStats()
        {
            Health = 125;
            BaseDamage = 25;
        }
    }
}