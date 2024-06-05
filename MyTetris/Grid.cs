
namespace MyTetris
{
    public class Grid
    {
        public int Rows { get;}

        public int Columns { get;}

        public readonly int[,] grid;

        //indexer
        public int this[int row, int column]
        {
            get { return grid[row, column]; }
            set { grid[row, column] = value; }
        }

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }





        public bool IsInside(int row, int column)
        {
            return row >= 0 && row < Rows && column >= 0 && column < Columns;
        }

        public bool IsEmpty(int row, int column)
        {
            return IsInside(row, column) && grid[row, column] == 0;
        }

        public bool IsRowEmpty(int row)
        {
            for (int column = 0; column < Columns; column++)
            {
                if (grid[row, column] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsRowFull(int row)
        {
            for (int column = 0; column < Columns; column++)
            {
                if (grid[row, column] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void ClearRow(int row)
        {
            for (int column = 0; column < Columns; column++)
            {
                grid[row, column] = 0;
            }
        }

        private void MoveRowDown(int row, int numRows)
        {
            for (int column = 0; column < Columns; column++)
            {
                grid[row + numRows, column] = grid[row, column];
                grid[row, column] = 0;
            }
        }

        public int ClearFullRows()
        {
            int clearedRows = 0;
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (IsRowFull(row))
                {
                    ClearRow(row);
                    clearedRows++;
                }
                else if (clearedRows > 0)
                {
                    MoveRowDown(row, clearedRows);
                }
            }
            return clearedRows;
        }
    }
}
