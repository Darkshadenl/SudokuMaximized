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
    public int[][] ConvertData(int columns, int rows)
    {
        var dataArray = new int[rows][];
        int rowNumber = -1;

        var data = Data; // variable for different modes

        if (Extension == ".samurai")
        {
            // samurai has 5 x Data
            // splits every line of data (every 'board') in a seperate index in arr
            var samuraiData = Data.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            // for import purposes you only need to display the 'first' board :)
            // so we use arr[0]
            // to know which board is prev/next use indexes as references
            // from the imported list
            data = samuraiData[0];
        }

        for (int i = 0; i < data.Length; i++)
        {
            if (i % rows == 0)
            {
                rowNumber++;
                dataArray[rowNumber] = new int[columns];
            }
            char c = data[i];
            dataArray[rowNumber][i % columns] = int.Parse(c.ToString());
        }

        return dataArray;
    }
}