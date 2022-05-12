namespace GenerateLib.Factory.Config.JSONModel;

public class InterpreterJSONModel
{
    public Boardinterpreter[] boardinterpreter { get; set; }
}

public class Boardinterpreter
{
    public string match { get; set; }
    public string library { get; set; }
    public string _namespace { get; set; }
}
