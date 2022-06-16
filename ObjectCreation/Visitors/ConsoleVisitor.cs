using Helpers.DrawingAlgsConsole;
using Helpers.Helpers;
using Helpers.Viewable;

namespace ObjectCreation.Visitors;

public class ConsoleVisitor : IPrintBoardVisitor
{
    private IDraw _draw;
    private readonly IDraw _definitive = new DefinitiveDraw();
    private readonly IDraw _help = new HelpDraw();

    public void Draw(IViewData viewData, BoardTypes type)
    {
        DrawStatic(viewData.State); 
        PreBoardDraw(viewData.PreBoardMessages);

        switch (viewData.State)
        {
            case States.Definitive:
                DrawDefinitive(type, viewData.Viewables);
                break;
            case States.Help:
                DrawHelp(type, viewData.Viewables);
                break;
            case States.Cheat:
                throw new NotImplementedException(); // TODO add cheat mode
            default:
                throw new ArgumentException("Invalid state");
        }

        PostBoardDraw(viewData.PostBoardMessages);
    }

    private void DrawDefinitive(BoardTypes type, List<IViewable> board)
    {
        if (_draw is not DefinitiveDraw)
            _draw = _definitive;

        DrawBoardOnType(type, board);
    }

    private void DrawHelp(BoardTypes type, List<IViewable> board)
    {
        if (_draw is not HelpDraw)
            _draw = _help;

        DrawBoardOnType(type, board);
    }

    private void DrawBoardOnType(BoardTypes type, List<IViewable> board)
    {
        switch (type)
        {
            case BoardTypes.nine:
                _draw.DrawRegularBoard(9, board);
                break;
            case BoardTypes.six:
                _draw.DrawRegularBoard(6, board);
                break;
            case BoardTypes.four:
                _draw.DrawRegularBoard(4, board);
                break;
            case BoardTypes.jigsaw:
                _draw.DrawJigSawBoard(9, board);
                break;
            case BoardTypes.samurai:
                _draw.DrawSamuraiBoard(9, board);
                break;
            default:
                throw new ArgumentException("Invalid board type");
        }
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
    
    public void DrawStatic(States state)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(state switch
        {
            States.Definitive => "Definitive",
            States.Help => "Help",
            _ => throw new ArgumentOutOfRangeException()
        });
        Console.ResetColor();
    }
    public void PreBoardDraw(List<ISimpleViewMessage>? messages)
    {
        DrawMessages(messages);
    }

    public void PostBoardDraw(List<ISimpleViewMessage>? messages)
    {
        DrawMessages(messages);
    }
    
}