using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Abstraction;
using Construction.Boards;
using Construction.Builder;
using Construction.Components;
using Import.Import;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SudokuTesting;

[TestClass]
public class ComponentTesting
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
    public void TestGetAllCellsGetsAllCells()
    {
        // Arrange
        var expectedNine = 9 * 9;
        var expectedSix = 6 * 6;
        var expectedFour = 4 * 4;
        
        // Act
        var cellsNine = _abstractNine.SudokuBoards[0].GetAllCells();
        var cellsSix = _abstractSix.SudokuBoards[0].GetAllCells();
        var cellsFour = _abstractFour.SudokuBoards[0].GetAllCells();
        
        // Assert
        Assert.AreEqual(expectedNine, cellsNine.Count);
        Assert.AreEqual(expectedSix, cellsSix.Count);
        Assert.AreEqual(expectedFour, cellsFour.Count);
    }
    
    [TestMethod]
    public void TestGetAllViewablesGetsAllViewables()
    {
        // Arrange
        var expectedNine = 9 * 9;
        var expectedSix = 6 * 6;
        var expectedFour = 4 * 4;
        
        // Act
        var cellsNine = _abstractNine.SudokuBoards[0].GetAllViewables();
        var cellsSix = _abstractSix.SudokuBoards[0].GetAllViewables();
        var cellsFour = _abstractFour.SudokuBoards[0].GetAllViewables();
        
        // Assert
        Assert.AreEqual(expectedNine, cellsNine.Count);
        Assert.AreEqual(expectedSix, cellsSix.Count);
        Assert.AreEqual(expectedFour, cellsFour.Count);
    }
    
    [TestMethod]
    public void TestHasDuplicateCellValueCorrectlyReturnTrue()
    {
        // Arrange
        var component = _abstractNine.SudokuBoards[0].Components[0];
        var doubleVal = component.Components.First(c => true).Value;
        component.Add(new Cell(doubleVal, 10, 10, false));

        // Act
        var hasDoubleVal = component.HasDuplicateCellValue(doubleVal);

        // Assert
        Assert.IsTrue(hasDoubleVal);
    }
    
    [TestMethod]
    public void TestHasDuplicateCellValueCorrectlyReturnFalse()
    {
        // Arrange
        var component = _abstractNine.SudokuBoards[0].Components[0];
        
        var allValues = component.Components.Select(c => c.Value).ToList();
        var found = false;
        var nr = 0;

        while (!found)
        {
            nr++;
            if (!allValues.Contains(nr))
                found = true;
        }

        // Act
        var notHasDoubleVal = component.HasDuplicateCellValue(nr);

        // Assert
        Assert.IsFalse(notHasDoubleVal);
    }

}