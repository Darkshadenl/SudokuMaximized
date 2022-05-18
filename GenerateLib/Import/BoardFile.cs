using Generating.Import;

namespace GenerateLib.Import;

public class BoardFile : IBoardFile
{
    public string Data { get; }
    public string Extension { get; }

    public BoardFile(string data, string extension)
    {
        Data = data;
        Extension = extension;
    }

    // <summary>
    // boardsize is either x or y size of board.
    // </summary>
    public int[][] ConvertData(int boardSize)
    {
        var dataArray = new int[boardSize][];
        int rowNumber = -1;
        
        for (int i = 0; i < Data.Length; i++)
        {
            if (i % boardSize == 0)
            {
                rowNumber++;
                dataArray[rowNumber] = new int[9];
            }
            char c = Data[i];
            dataArray[rowNumber][i%9] = int.Parse(c.ToString()); 
        }

        return dataArray;
    }
}