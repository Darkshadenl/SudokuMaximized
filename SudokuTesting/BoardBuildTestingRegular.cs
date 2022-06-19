using System.Collections.Generic;
using System.IO;
using System.Linq;
using Abstraction;
using Construction.Boards;
using Construction.Builder;
using Construction.Components;
using Helpers.Helpers;
using Import.Import;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Solvers;
using Sudoku.Command;
using Sudoku.Controller;

namespace SudokuTesting;

[TestClass]
public class BoardBuildTestingRegular
{
    private static Helpers _helper;

    private static AbstractBoard _abstractNine;

    private static AbstractBoard _abstractSix;

    private static AbstractBoard _abstractFour;

    
    [ClassInitialize]
    public static void TestFixtureSetup(TestContext context)
    {
        _helper = new Helpers();
        
        // Arrange
        var fileInfoNine = new FileInfo("TestResources\\puzzle.9x9");
        var fileInfoSix = new FileInfo("TestResources\\puzzle.6x6");
        var fileInfoFour = new FileInfo("TestResources\\puzzle.4x4");

        var nine = _helper.CreateBoardData(fileInfoNine);
        var six = _helper.CreateBoardData(fileInfoSix);
        var four = _helper.CreateBoardData(fileInfoFour);
        
        BoardFile bNine = new BoardFile(nine, fileInfoNine.Extension);
        BoardFile bSix = new BoardFile(six, fileInfoNine.Extension);
        BoardFile bFour = new BoardFile(four, fileInfoNine.Extension);
        
        BoardBuildDirector director = new BoardBuildDirector();
        BoardBuilder builder = new BoardBuilder();
        
        director.BoardBuilder = builder;
        
        director.ConstructRegularBoard(bNine);
        _abstractNine = builder.Build();
        director.Construct6X6Board(bSix);
        _abstractSix = builder.Build();
        director.Construct4X4Board(bFour);
        _abstractFour = builder.Build();
        
    }
    
    [TestMethod]
    public void TestCreateCorrectAmountSudokuboardsRegular()
    {
        // Assert
        Assert.AreEqual(1, _abstractNine.SudokuBoards.Count);
        Assert.AreEqual(1, _abstractSix.SudokuBoards.Count);
        Assert.AreEqual(1, _abstractFour.SudokuBoards.Count);
    }

    [TestMethod]
    public void TestFourRightAmountCols()
    {
        // Arrange
        var rightAmount = 4;

        // Act
        var data = _abstractFour.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Column).ToList()
            .Count;
        
        // Assert
        Assert.AreEqual(rightAmount, data);
    }
    
    [TestMethod]
    public void TestFourRightAmountRows()
    {
        // Arrange
        var rightAmount = 4;

        // Act
        var data = _abstractFour.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Row).ToList()
            .Count;
        
        // Assert
        Assert.AreEqual(rightAmount, data);
    }
    
    [TestMethod]
    public void TestFourRightAmountSquares()
    {
        // Arrange
        var rightAmount = 4;

        // Act
        var data = _abstractFour.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Square).ToList()
            .Count;
        
        // Assert
        Assert.AreEqual(rightAmount, data);
    }
    
    [TestMethod]
    public void TestFourRightAmountCellsPerCol()
    {
        // Arrange
        var rightAmount = 4;

        // Act
        var cellCountPerCol = new List<int>();
        
        _abstractFour.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Column).ToList()
            .ForEach(c => cellCountPerCol.Add(c.Components.Count));
        
        // Assert
        foreach (var i in cellCountPerCol)
        {
            Assert.AreEqual(rightAmount, i);
        }
    }
    
    [TestMethod]
    public void TestFourRightAmountCellsPerRow()
    {
        // Arrange
        var rightAmount = 4;

        // Act
        var cellCountPerRow = new List<int>();
        
        _abstractFour.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Row).ToList()
            .ForEach(c => cellCountPerRow.Add(c.Components.Count));
        
        // Assert
        foreach (var i in cellCountPerRow)
        {
            Assert.AreEqual(rightAmount, i);
        }
    }
    
    [TestMethod]
    public void TestFourRightAmountCellsPerSquare()
    {
        // Arrange
        var rightAmount = 4;

        // Act
        var cellCountPerSquare = new List<int>();
        
        _abstractFour.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Square).ToList()
            .ForEach(c => cellCountPerSquare.Add(c.Components.Count));
        
        // Assert
        foreach (var i in cellCountPerSquare)
        {
            Assert.AreEqual(rightAmount, i);
        }
    }
    
    [TestMethod]
    public void TestSixRightAmountCols()
    {
        // Arrange
        var rightAmount = 6;

        // Act
        var data = _abstractSix.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Column).ToList()
            .Count;
        
        // Assert
        Assert.AreEqual(rightAmount, data);
    }
    
    [TestMethod]
    public void TestSixRightAmountRows()
    {
        // Arrange
        var rightAmount = 6;

        // Act
        var data = _abstractSix.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Row).ToList()
            .Count;
        
        // Assert
        Assert.AreEqual(rightAmount, data);
    }
    
    [TestMethod]
    public void TestSixRightAmountSquares()
    {
        // Arrange
        var rightAmount = 6;

        // Act
        var data = _abstractSix.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Square).ToList()
            .Count;
        
        // Assert
        Assert.AreEqual(rightAmount, data);
    }
    
    [TestMethod]
    public void TestSixRightAmountCellsPerCol()
    {
        // Arrange
        var rightAmount = 6;

        // Act
        var cellCountPerCol = new List<int>();
        
        _abstractSix.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Column).ToList()
            .ForEach(c => cellCountPerCol.Add(c.Components.Count));
        
        // Assert
        foreach (var i in cellCountPerCol)
        {
            Assert.AreEqual(rightAmount, i);
        }
    }
    
    [TestMethod]
    public void TestSixRightAmountCellsPerRow()
    {
        // Arrange
        var rightAmount = 6;

        // Act
        var cellCountPerRow = new List<int>();
        
        _abstractSix.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Row).ToList()
            .ForEach(c => cellCountPerRow.Add(c.Components.Count));
        
        // Assert
        foreach (var i in cellCountPerRow)
        {
            Assert.AreEqual(rightAmount, i);
        }
    }
    
    [TestMethod]
    public void TestSixRightAmountCellsPerSquare()
    {
        // Arrange
        var rightAmount = 6;

        // Act
        var cellCountPerSquare = new List<int>();
        
        _abstractSix.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Square).ToList()
            .ForEach(c => cellCountPerSquare.Add(c.Components.Count));
        
        // Assert
        foreach (var i in cellCountPerSquare)
        {
            Assert.AreEqual(rightAmount, i);
        }
    }

    [TestMethod]
    public void TestNineRightAmountCols()
    {
        // Arrange
        var rightAmount = 9;

        // Act
        var data = _abstractNine.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Column).ToList()
            .Count;
        
        // Assert
        Assert.AreEqual(rightAmount, data);
    }

    [TestMethod]
    public void TestNineRightAmountRows()
    {
        // Arrange
        var rightAmount = 9;

        // Act
        var data = _abstractNine.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Row).ToList()
            .Count;
        
        // Assert
        Assert.AreEqual(rightAmount, data);
    }
    
    [TestMethod]
    public void TestNineRightAmountSquares()
    {
        // Arrange
        var rightAmount = 9;

        // Act
        var data = _abstractNine.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Square).ToList()
            .Count;
        
        // Assert
        Assert.AreEqual(rightAmount, data);
    }
    
    [TestMethod]
    public void TestNineRightAmountCellsPerCol()
    {
        // Arrange
        var rightAmount = 9;

        // Act
        var cellCountPerCol = new List<int>();
        
        _abstractNine.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Column).ToList()
            .ForEach(c => cellCountPerCol.Add(c.Components.Count));
        
        // Assert
        foreach (var i in cellCountPerCol)
        {
            Assert.AreEqual(rightAmount, i);
        }
    }
    
    [TestMethod]
    public void TestNineRightAmountCellsPerRow()
    {
        // Arrange
        var rightAmount = 9;

        // Act
        var cellCountPerRow = new List<int>();
        
        _abstractNine.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Row).ToList()
            .ForEach(c => cellCountPerRow.Add(c.Components.Count));
        
        // Assert
        foreach (var i in cellCountPerRow)
        {
            Assert.AreEqual(rightAmount, i);
        }
    }
    
    [TestMethod]
    public void TestNineRightAmountCellsPerSquare()
    {
        // Arrange
        var rightAmount = 9;

        // Act
        var cellCountPerSquare = new List<int>();
        
        _abstractNine.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Square).ToList()
            .ForEach(c => cellCountPerSquare.Add(c.Components.Count));
        
        // Assert
        foreach (var i in cellCountPerSquare)
        {
            Assert.AreEqual(rightAmount, i);
        }
    }
    
    [TestMethod]
    [Timeout(10000)]
    public void TestSolverNine()
    {
        // Arrange
        var mock = new Mock<GameController>();
        var components = _abstractNine.SudokuBoards.Cast<IComponent>().ToList();
        var boardSolved = components[0];
        BoardTypes boardType = BoardTypes.nine;
        var solveCommand = new SolveCommand(boardType, components, mock.Object);

        // Act
        solveCommand.Execute();

        // Assert
        var viewables = boardSolved.GetAllViewables();
        foreach (var viewable in viewables)
        {
            Assert.AreNotEqual(0, viewable.Value);
        }
    }
    
    [TestMethod]
    [Timeout(10000)]
    public void TestSolverSix()
    {
        // Arrange
        var mock = new Mock<GameController>();
        var components = _abstractSix.SudokuBoards.Cast<IComponent>().ToList();
        var boardSolved = components[0];
        BoardTypes boardType = BoardTypes.six;
        var solveCommand = new SolveCommand(boardType, components, mock.Object);

        // Act
        solveCommand.Execute();

        // Assert
        var viewables = boardSolved.GetAllViewables();
        foreach (var viewable in viewables)
        {
            Assert.AreNotEqual(0, viewable.Value);
        }
    }
    
    [TestMethod]
    [Timeout(10000)]
    public void TestSolverFour()
    {
        // Arrange
        var mock = new Mock<GameController>();
        var components = _abstractFour.SudokuBoards.Cast<IComponent>().ToList();
        var boardSolved = components[0];
        BoardTypes boardType = BoardTypes.four;
        var solveCommand = new SolveCommand(boardType, components, mock.Object);

        // Act
        solveCommand.Execute();

        // Assert
        var viewables = boardSolved.GetAllViewables();
        foreach (var viewable in viewables)
        {
            Assert.AreNotEqual(0, viewable.Value);
        }
    }
    
   

}