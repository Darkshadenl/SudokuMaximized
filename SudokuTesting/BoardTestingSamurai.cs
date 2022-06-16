using System.Collections.Generic;
using System.IO;
using System.Linq;
using Abstraction;
using BoardConstruction.Boards;
using BoardConstruction.Builder;
using BoardConstruction.Components;
using Import.Import;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SudokuTesting;

[TestClass]
public class BoardTestingSamurai
{
    private static Helpers _helper;
    private static AbstractBoard _abstractOne;
    private static AbstractBoard _abstractTwo;
    private static AbstractBoard _abstractThree;


    [ClassInitialize]
    public static void TestFixtureSetup(TestContext context)
    {
        _helper = new Helpers();

        // Arrange
        var fileSamuraiOne = new FileInfo("TestResources\\puzzle.samurai");
        var fileSamuraiTwo = new FileInfo("TestResources\\puzzle2.samurai");
        var fileSamuraiThree = new FileInfo("TestResources\\puzzle3.samurai");

        var samuraiDataOne = _helper.CreateBoardData(fileSamuraiOne);
        var samuraiDataTwo = _helper.CreateBoardData(fileSamuraiTwo);
        var samuraiDataThree = _helper.CreateBoardData(fileSamuraiThree);

        BoardFile bSamuraiOne = new BoardFile(samuraiDataOne, fileSamuraiOne.Extension);
        BoardFile bSamuraiTwo = new BoardFile(samuraiDataTwo, fileSamuraiOne.Extension);
        BoardFile bSamuraiThree = new BoardFile(samuraiDataThree, fileSamuraiOne.Extension);

        BoardBuildDirector director = new BoardBuildDirector();
        BoardBuilder builder = new BoardBuilder();

        director.BoardBuilder = builder;

        director.ConstructSamuraiBoard(bSamuraiOne);
        _abstractOne = builder.Build();
        director.ConstructSamuraiBoard(bSamuraiTwo);
        _abstractTwo = builder.Build();
        director.ConstructSamuraiBoard(bSamuraiThree);
        _abstractThree = builder.Build();
    }

    [TestMethod]
    public void TestCreateBoardBuildGivesCorrectAmountSudokuboards()
    {
        // Arrange
        var rightAmount = 5;

        // Assert
        Assert.AreEqual(rightAmount, _abstractOne.SudokuBoards.Count);
        Assert.AreEqual(rightAmount, _abstractTwo.SudokuBoards.Count);
        Assert.AreEqual(rightAmount, _abstractThree.SudokuBoards.Count);
    }

    [TestMethod]
    public void TestRightAmountCols()
    {
        // Arrange
        var rightAmountOneBoard = 9;
        var rightAmountAllBoardsCombined = 45;

        // Act
        var dataOne = _abstractOne.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Column).ToList()
            .Count;

        List<IComponent> allCols = new List<IComponent>();
        _abstractOne.SudokuBoards.ForEach(c => { allCols.AddRange(c.Components.Where(c => c is Column).ToList()); });


        // Assert
        Assert.AreEqual(rightAmountOneBoard, dataOne);
        Assert.AreEqual(rightAmountAllBoardsCombined, allCols.Count);
    }

    [TestMethod]
    public void TestRightAmountRows()
    {
        // Arrange
        var rightAmountOneBoard = 9;
        var rightAmountAllBoardsCombined = 45;

        // Act
        var dataOne = _abstractOne.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Row).ToList()
            .Count;

        List<IComponent> all = new List<IComponent>();
        _abstractOne.SudokuBoards.ForEach(c => { all.AddRange(c.Components.Where(c => c is Row).ToList()); });


        // Assert
        Assert.AreEqual(rightAmountOneBoard, dataOne);
        Assert.AreEqual(rightAmountAllBoardsCombined, all.Count);
    }

    [TestMethod]
    public void TestRightAmountSquares()
    {
        // Arrange
        var rightAmountOneBoard = 9;
        var rightAmountAllBoardsCombined = 45;

        // Act
        var dataOne = _abstractOne.SudokuBoards.FirstOrDefault()!
            .Components
            .Where(c => c is Square).ToList()
            .Count;

        List<IComponent> all = new List<IComponent>();
        _abstractOne.SudokuBoards.ForEach(c => { all.AddRange(c.Components.Where(c => c is Square).ToList()); });


        // Assert
        Assert.AreEqual(rightAmountOneBoard, dataOne);
        Assert.AreEqual(rightAmountAllBoardsCombined, all.Count);
    }

    [TestMethod]
    public void TestRightAmountCellsPerCol()
    {
        // Arrange
        var rightAmount = 9;

        // Act
        var cellCountPerCol = new List<int>();

        _abstractOne.SudokuBoards.FirstOrDefault()!
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
    public void TestRightAmountCellsPerRow()
    {
        // Arrange
        var rightAmount = 9;

        // Act
        var cellCountPerRow = new List<int>();

        _abstractOne.SudokuBoards.FirstOrDefault()!
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
    public void TestRightAmountCellsPerSquare()
    {
        // Arrange
        var rightAmount = 9;

        // Act
        var cellCountPerSquare = new List<int>();

        _abstractOne.SudokuBoards.FirstOrDefault()!
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
    public void TestOnlyCloneInMiddleBoardCorners()
    {
        // Arrange
        var squares = _abstractOne.SudokuBoards[2]
            .Components.Where(c => c is Square).ToList();
        
        var leftUpper = squares[0];
        var rightUpper = squares[2];
        var leftLower = squares[6];
        var rightLower = squares[8];
        
        // Act

        var isLeftUpperCellsClone = leftUpper.Components.Where(c => c is Cell)
            .Cast<Cell>().All(c => c.IsClone);
        var isRightUpperCellsClone = rightUpper.Components.Where(c => c is Cell)
            .Cast<Cell>().All(c => c.IsClone);
        var isLeftLowerCellsClone = leftLower.Components.Where(c => c is Cell)
            .Cast<Cell>().All(c => c.IsClone);
        var isRightLowerCellsClone = rightLower.Components.Where(c => c is Cell)
            .Cast<Cell>().All(c => c.IsClone);
        
        // Assert
        Assert.IsTrue(isLeftUpperCellsClone);
        Assert.IsTrue(isRightUpperCellsClone);
        Assert.IsTrue(isLeftLowerCellsClone);
        Assert.IsTrue(isRightLowerCellsClone);

    }
}