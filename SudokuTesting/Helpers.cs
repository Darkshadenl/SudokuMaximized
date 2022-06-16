using System;
using System.IO;

namespace SudokuTesting;

public class Helpers
{
    public string[] CreateBoardData(FileInfo fileInfo)
    {
        string[] data = File.ReadAllText(fileInfo.FullName)
            .Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        return data;
    }
}