﻿using BoardConstruction.Config.JSONModel;
using BoardConstruction.Visitors;
using Newtonsoft.Json;

namespace BoardConstruction.Factory;

public class VisitorFactory : IVisitorFactory
{
    private readonly Dictionary<string, Func<IPrintBoardVisitor>>? _visitors = new();
 
    public VisitorFactory()
    {
        var json = File.ReadAllText(Environment.GetEnvironmentVariable("VISITORCONFIG") ?? 
                                    "./Config/VisitorFactoryConfiguration.json");
        
        var deserializeObject = JsonConvert.DeserializeObject<VisitorJSONModel>(json);
        
        foreach (var visitor in deserializeObject.visitor)
        {
            var type = Type.GetType($"{visitor._namespace}, {visitor.library}");
            
            _visitors!.Add(visitor.match, () =>
            {
                return Activator.CreateInstance(type) as IPrintBoardVisitor;
            });
        }
    }

    public IPrintBoardVisitor Create(string uiType)
    {
        string lookupValue = uiType.ToLowerInvariant();

        if (_visitors.TryGetValue(lookupValue, out var visitorCreator))
        {
            return visitorCreator.Invoke();
        }

        throw new ArgumentException($"Visitor {uiType} not found");
    }
    
}