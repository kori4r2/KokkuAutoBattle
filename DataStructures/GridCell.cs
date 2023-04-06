namespace AutoBattle
{
    public struct GridCell
    {
        public int xIndex;
        public int yIndex;
        public bool occupied;
        public int Index;

        public GridCell(int x, int y, bool occupied, int index)
        {
            xIndex = x;
            yIndex = y;
            this.occupied = occupied;
            Index = index;
        }
    }
}