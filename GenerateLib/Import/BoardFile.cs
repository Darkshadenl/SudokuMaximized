using Generating.Import;

namespace GenerateLib.Import;

public class BoardFile : IBoardFile
{
    public string[] Data { get; }
    public string Extension { get; }

    public BoardFile(string[] data, string extension)
    {
        Data = data;
        Extension = extension;
    }

    // <summary>
    // boardsize is either x or y size of board.
    // </summary>
    public int[][] ConvertData(int columns, int rows)
    {
        var dataArray = new int[rows][];
        int rowNumber = -1;

        for (int i = 0; i < Data[0].Length; i++)
        {
            if (i % rows == 0)
            {
                rowNumber++;
                dataArray[rowNumber] = new int[columns];
            }
            var c = Data[0][i].ToString();
            dataArray[rowNumber][i % columns] = int.Parse(c);
        }

        return dataArray;
    }

    public int GetAmountBoards()
    {
        return Data.Count();
    }

    public string[] GetData()
    {
        return Data;
    }
}