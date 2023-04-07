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
            set
            {
                bool shouldCallback = value != occupied;
                occupied = value;
                if (shouldCallback)
                    OnStatusChanged?.Invoke();
            }
        }
        private event VoidEvent OnStatusChanged;

        public GridCell(int x, int y, bool occupied)
        {
            Column = x;
            Row = y;
            this.occupied = occupied;
        }

        public void AddStatusChangeListener(VoidEvent callback)
        {
            OnStatusChanged += callback;
        }
    }
}