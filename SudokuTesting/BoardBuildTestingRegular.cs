using System.Collections.Generic;
using System.IO;
using System.Linq;
using BoardConstruction.Boards;
using BoardConstruction.Builder;
using BoardConstruction.Components;
using Import.Import;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SudokuTesting;

[TestClass]
public class BoardBuildTestingRegular
{
    private static Helpers _helper;
    private static AbstractBoard abstractNine;
    private static AbstractBoard abstractSix;
    private static AbstractBoard abstractFour;
    
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
        abstractNine = builder.Build();
        director.Construct6X6Board(bSix);
        abstractSix = builder.Build();
        director.Construct4X4Board(bFour);
        abstractFour = builder.Build();
        
    }
    
    [TestMethod]
    public void TestCreateCorrectAmountSudokuboardsRegular()
    {
        // Assert
        Assert.AreEqual(1, abstractNine.SudokuBoards.Count);
        Assert.AreEqual(1, abstractSix.SudokuBoards.Count);
        Assert.AreEqual(1, abstractFour.SudokuBoards.Count);
    }

    [TestMethod]
    public void TestFourRightAmountCols()
    {
        // Arrange
        var rightAmount = 4;

        // Act
        var data = abstractFour.SudokuBoards.FirstOrDefault()!
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
        var data = abstractFour.SudokuBoards.FirstOrDefault()!
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
        var data = abstractFour.SudokuBoards.FirstOrDefault()!
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
        
        abstractFour.SudokuBoards.FirstOrDefault()!
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
        
        abstractFour.SudokuBoards.FirstOrDefault()!
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
        
        abstractFour.SudokuBoards.FirstOrDefault()!
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
        var data = abstractSix.SudokuBoards.FirstOrDefault()!
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
        var data = abstractSix.SudokuBoards.FirstOrDefault()!
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
        var data = abstractSix.SudokuBoards.FirstOrDefault()!
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
        
        abstractSix.SudokuBoards.FirstOrDefault()!
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
        
        abstractSix.SudokuBoards.FirstOrDefault()!
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
        
        abstractSix.SudokuBoards.FirstOrDefault()!
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
        var data = abstractNine.SudokuBoards.FirstOrDefault()!
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
        var data = abstractNine.SudokuBoards.FirstOrDefault()!
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
        var data = abstractNine.SudokuBoards.FirstOrDefault()!
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
        
        abstractNine.SudokuBoards.FirstOrDefault()!
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
        
        abstractNine.SudokuBoards.FirstOrDefault()!
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
        
        abstractNine.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Square).ToList()
            .ForEach(c => cellCountPerSquare.Add(c.Components.Count));
        
        // Assert
        foreach (var i in cellCountPerSquare)
        {
            Assert.AreEqual(rightAmount, i);
        }
    }

}