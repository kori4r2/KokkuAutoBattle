namespace AutoBattle
{
    public struct GridCell
    {
        public int column;
        public int row;
        public bool occupied;
        public int Index;

        public GridCell(int x, int y, bool occupied, int index)
        {
            column = x;
            row = y;
            this.occupied = occupied;
            Index = index;
        }
    }
}