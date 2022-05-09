using Sudoku.Model.Components;

namespace Sudoku.Model;

public class RegularBoard : AbstractBoard
{
    private Solver _solver;

    public RegularBoard(Solver solver)
    {
        _solver = solver;
        _sudokuBoard = new SudokuBoard();
        _solver.SudokuBoard = _sudokuBoard;  
    }
    
    public AbstractBoard CreateBoard(int[][] mdArray)
    {
        var columns = new Column[mdArray[0].Length];
        var rows = new Row[mdArray.Length];
        var squares = new Square[mdArray.Length / 3][];
        
        squares[0] = new Square[mdArray.Length / 3];
        squares[1] = new Square[mdArray.Length / 3];
        squares[2] = new Square[mdArray.Length / 3];
        
        // create rows, cols and squares
        for (int i = 0; i < mdArray.Length; i++)
        {
            if (rows[i] == null)
            {
                rows[i] = new Row(i);
            }
            
            for (int j = 0; j < mdArray[i].Length; j++)
            {
                var c = j;
                var r = i;
                Square activeSquare;
                if (columns[j] == null)
                {
                    columns[j] = new Column(j);
                }
                
                int value = mdArray[i][j];
                var cell = new Cell(value, j, i);
                
                rows[i].Add(cell);
                columns[j].Add(cell);
                cell.Row = rows[i];
                cell.Column = columns[j];

                activeSquare = squares[i % 3][j % 3];
                if (activeSquare == null)
                {
                    activeSquare = new Square(i % 3, j % 3);
                    squares[i % 3][j % 3] = activeSquare;
                }
                activeSquare.Add(cell);
                cell.Square = activeSquare;
            }
        }
        foreach (var column in columns)
        {
            _sudokuBoard.Add(column);
        }

        foreach (var row in rows)
        {
            _sudokuBoard.Add(row);
        }

        foreach (var square in squares)
        {
            foreach (var square1 in square)
            {
                _sudokuBoard.Add(square1);
            }
        }

        return this;
    }
    
}