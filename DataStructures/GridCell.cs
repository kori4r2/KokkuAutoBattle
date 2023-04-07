namespace AutoBattle
{
    public class GridCell
    {
        public int column;
        public int row;
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
        public int Index;
        private event VoidEvent OnStatusChanged;

        public GridCell(int x, int y, bool occupied, int index)
        {
            column = x;
            row = y;
            this.occupied = occupied;
            Index = index;
        }

        public void AddStatusChangeListener(VoidEvent callback)
        {
            OnStatusChanged += callback;
        }
    }
}