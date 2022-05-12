using GenerateLib.Factory.Config.JSONModel;
using GenerateLib.Interpreters;
using Newtonsoft.Json;

namespace GenerateLib.Factory;

public class BoardInterpreterFactory : IBoardInterpreterFactory
{
    private Dictionary<string, Func<IBoardInterpreter>>? _interpreters = new();
 
    public BoardInterpreterFactory()
    {
        var json = File.ReadAllText(Environment.GetEnvironmentVariable("INTERPRETERCONFIG") ?? 
                                    "./Factory/Config/InterpreterFactoryConfiguration.json");
        
        var deserializeObject = JsonConvert.DeserializeObject<InterpreterJSONModel>(json);
        foreach (var boardinterpreter in deserializeObject.boardinterpreter)
        {
            var type = Type.GetType($"{boardinterpreter._namespace}");
            
            _interpreters!.Add(boardinterpreter.match, () =>
            {
                return Activator.CreateInstance(type) as IBoardInterpreter;
            });
        }
    }

    public IBoardInterpreter Create(string interpreter)
    {
        string lookupValue = interpreter.ToLowerInvariant();

        if (_interpreters.TryGetValue(lookupValue, out var interpreterCreator))
        {
            return interpreterCreator.Invoke();
        }

        throw new ArgumentException($"Interpreter {interpreter} not found");
    }
    
}