using GenerateLib.Helpers;
using GenerateLib.Viewable;
using Sprache;

namespace GenerateLib.Visitors;

public class ConsoleVisitor : IPrintBoardVisitor
{
    
    public void Draw(List<IViewable> board, BoardTypes type)
    {

        switch (type)
        {
            case BoardTypes.nine:
                DrawRegular(board, 9);
                break;
            case BoardTypes.six:
                DrawRegular(board, 6);
                break;
            case BoardTypes.four:
                DrawRegular(board, 4);
                break;
            case BoardTypes.jigsaw:
                DrawJigSaw(board, 9);
                break;
            // case BoardTypes.samurai:
            //     return 0;
            default:
                DrawRegular(board, 9);
                break;
        }
    }

    private void DrawRegular(List<IViewable> board, int size)
    {
        var verC = 0;
        var squareSize =  (int) Math.Sqrt(size);
        var horizontalLine = "-------------------------------";

        Console.WriteLine(horizontalLine);

        for (var index = 0; index < board.Count; index++)
        {
            if (index != 0 && index % size == 0)
            {
                Console.Write("|");
                Console.WriteLine();
                if (verC == squareSize - 1)
                {
                    Console.WriteLine(horizontalLine);
                    verC = 0;
                }
                else
                {
                    verC++;
                }
            }

            if (index % squareSize == 0)
            {
                Console.Write("|");
            }

            var boardValue = board[index].Value == 0 ? " " : board[index].Value.ToString();
            if (board[index].IsCursor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" {boardValue} ");
                Console.ResetColor();
            }
            else
            {
                Console.Write($" {boardValue} ");
            }
        }

        Console.Write("|");
        Console.WriteLine();
        Console.WriteLine(horizontalLine);
    }

    public void DrawJigSaw(List<IViewable> board, int size)
    {
        throw new NotImplementedException();
    }
}