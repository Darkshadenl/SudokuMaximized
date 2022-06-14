﻿using Helpers.Helpers;
using Helpers.Viewable;

namespace Helpers.Visitors;

public interface IPrintBoardVisitor
{
    public void Draw(IViewData viewData, BoardTypes type);
}