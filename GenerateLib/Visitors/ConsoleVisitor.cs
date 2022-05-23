using GenerateLib.Helpers;
using GenerateLib.Viewable;

namespace GenerateLib.Visitors;

public class ConsoleVisitor : IPrintBoardVisitor
{
    private List<IViewable> _board = new();
    private List<ISimpleViewMessage>? _messages;
    private string _state = "";
    
    public void PreBoardDraw(List<ISimpleViewMessage>? messages)
    {
        DrawMessages(messages);
    }

    public void DrawStatic()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(_state);
        Console.ResetColor();
    }

    private void DrawMessages(List<ISimpleViewMessage>? messages)
    {
        if (messages == null) return;
        foreach (var message in messages)
        {
            Console.ForegroundColor = message.MessageColor;
            Console.WriteLine(message.Message);
            Console.ResetColor();
        }
    }

    public void PostBoardDraw(List<ISimpleViewMessage>? messages)
    {
        DrawMessages(messages);
    }

    public void Draw(IViewData viewData, BoardTypes type)
    {
        _board = viewData.Viewables;
        _state = viewData.State;
        DrawStatic();
        PreBoardDraw(viewData.PreBoardMessages);

        switch (type)
        {
            case BoardTypes.nine:
                DrawRegularBoard(9);
                break;
            case BoardTypes.six:
                DrawRegularBoard(6);
                break;
            case BoardTypes.four:
                DrawRegularBoard(4);
                break;
            case BoardTypes.jigsaw:
                DrawJigSawBoard(9);
                break;
            // case BoardTypes.samurai:
            //     return 0;
            default:
                throw new ArgumentException("Invalid board type");
        }
        
        PostBoardDraw(viewData.PostBoardMessages);
    }

    private void DrawRegularBoard(int size)
    {
        var verC = 0;
        var squareSize = (int) Math.Sqrt(size);

        var set = new Dictionary<int, string>();
        set.Add(6, "----------------------");
        set.Add(9, "-------------------------------");
        set.Add(4, "---------------");

        var horizontalLine = set.First(e => e.Key == size).Value;

        Console.WriteLine(horizontalLine);

        for (var index = 0; index < _board.Count; index++)
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

            var boardValue = _board[index].Value == 0 ? " " : _board[index].Value.ToString();
            if (_board[index].IsCursor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (_board[index].Value == 0) Console.Write(" _ ");
                else Console.Write($" {boardValue} ");
                ;
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

    public void DrawJigSawBoard(int size)
    {
        throw new NotImplementedException();
    }
}