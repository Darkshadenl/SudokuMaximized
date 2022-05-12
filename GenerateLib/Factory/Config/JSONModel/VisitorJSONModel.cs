namespace GenerateLib.Factory.Config.JSONModel;


public class VisitorJSONModel
{
    public Visitor[] visitor { get; set; }
}

public class Visitor
{
    public string match { get; set; }
    public string library { get; set; }
    public string _namespace { get; set; }
}