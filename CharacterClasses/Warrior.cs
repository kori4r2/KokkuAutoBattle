namespace AutoBattle
{
    public class Warrior : Character
    {
        public Warrior(GridCell startingPosition, CharacterTeam team, int index)
        : base(startingPosition, team, index) { }

        protected override CharacterClass MyClass => CharacterClass.Warrior;

        protected override void SetBaseStats()
        {
            Health = 100;
            BaseDamage = 20;
        }
    }
}