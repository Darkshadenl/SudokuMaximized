﻿using System.Diagnostics;
using System.Text;
using GenerateLib.Config.ImportHandler.JSONModel;
using GenerateLib.Import;
using Newtonsoft.Json;

namespace Sudoku.Model.Import;

public class ImportHandler
{
    private string[] _validExtensions;

    private void FillValidExtensionsList()
    {
        try
        {
            var json = File.ReadAllText(Environment.GetEnvironmentVariable("IMPORTVALIDEXTENSIONCONFIG") ??
            "./Config/ImportHandler/ImportValidFileExtensionConfiguration.json");

            var res = JsonConvert.DeserializeObject<ImportJSONModel>(json);

            _validExtensions = new string[res.validExtensions.Count()];

            for(int i = 0; i < res.validExtensions.Count(); i++)
            {
                _validExtensions[i] = res.validExtensions[i].extension;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Valid file extension list could not be loaded");
            Console.WriteLine(e);
            return;
        }
    }


    public BoardFile ImportFromPath(FileInfo fileInfo)
    {
        // fills list of valid extensions on initialization
        FillValidExtensionsList();

        Debug.Assert(fileInfo.DirectoryName != null, "fileInfo.DirectoryName != null");
        string data = File.ReadAllText(fileInfo.FullName);
        string extension = fileInfo.Extension;

        if (ValidExtension(extension))
        {
            return new BoardFile(data, extension);
        }

        var sb = new StringBuilder("Wrong file. Accepted extensions are");

        for (int i = 0; i < _validExtensions.Length; i++)
        {
            if (i == _validExtensions.Length - 1)
            {
                sb.Append($" {_validExtensions[i]}.");
                continue;
            }
            sb.Append($" {_validExtensions[i]},");
        }

        throw new InvalidDataException(sb.ToString());
    }

    private bool ValidExtension(string extension)
    {
        return _validExtensions.Contains(extension);
    }
}