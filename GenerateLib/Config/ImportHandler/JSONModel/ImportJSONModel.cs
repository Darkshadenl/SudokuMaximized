namespace GenerateLib.Config.ImportHandler.JSONModel;

public class ImportJSONModel
{
    public ImportValidExtensions[] validExtensions { get; set; }

    public class ImportValidExtensions
    {
        public string extension { get; set; }
    }
}