namespace AutoBattle
{
    public class GridCell
    {
        public int Column { get; private set; }
        public int Row { get; private set; }
        private bool occupied;
        public bool Occupied
        {
            get => occupied;
            private set
            {
                bool shouldCallback = value != occupied;
                occupied = value;
                if (shouldCallback)
                    OnStatusChanged?.Invoke(this);
            }
        }
        private char occupantChar = ' ';
        public char DisplayCharacter => Occupied ? occupantChar : ' ';
        private event GridCellEvent OnStatusChanged;

        public GridCell(int column, int row, bool occupied)
        {
            Column = column;
            Row = row;
            this.occupied = occupied;
        }

        public void Occupy(char displayChar)
        {
            occupantChar = displayChar;
            Occupied = true;
        }

        public void Vacate()
        {
            Occupied = false;
        }

        public void AddStatusChangeListener(GridCellEvent callback)
        {
            OnStatusChanged += callback;
        }
    }
}