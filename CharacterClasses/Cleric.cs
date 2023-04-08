namespace AutoBattle
{
    public class Cleric : Character
    {
        public Cleric(GridCell startingPosition, CharacterTeam team, int index)
        : base(startingPosition, team, index) { }

        protected override CharacterClass MyClass => CharacterClass.Cleric;

        protected override void SetBaseStats()
        {
            Health = 150;
            BaseDamage = 20;
        }
    }
}