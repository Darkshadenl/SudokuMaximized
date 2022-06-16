﻿using Abstraction;

namespace Solvers;

public abstract class AbstractSolver : ISolver
{
    public IGameController Controller { get; set; }
    public abstract List<IComponent> SolveBoards(List<IComponent> boards);
}