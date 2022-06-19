using System;
using Construction.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SudokuTesting;

[TestClass]
public class CellTesting
{
    [TestMethod]
    public void TestFindDuplicateInRowTrue()
    {
        // Arrange
        var number = 5;
        var cell = new Cell(number, 0, 0, false);
        var row = new Row();
        row.Add(cell);
        cell.Rows.Add(row);
        
        // Act
        var isDuplicate = cell.IsValueDuplicateInRows(number);

        // Assert
        Assert.IsTrue(isDuplicate);
    }
    
    [TestMethod]
    public void TestFindDuplicateInRowFalse()
    {
        // Arrange
        var number = 5;
        var cell = new Cell(6, 0, 0, false);
        var row = new Row();
        row.Add(cell);
        cell.Rows.Add(row);
        
        // Act
        var isDuplicate = cell.IsValueDuplicateInRows(number);

        // Assert
        Assert.IsFalse(isDuplicate);
    }
    
    [TestMethod]
    public void TestFindDuplicateInColumnTrue()
    {
        // Arrange
        var number = 5;
        var cell = new Cell(number, 0, 0, false);
        var col = new Column();
        col.Add(cell);
        cell.Columns.Add(col);
        
        // Act
        var isDuplicate = cell.IsValueDuplicateInColumns(number);

        // Assert
        Assert.IsTrue(isDuplicate);
    }
    
    [TestMethod]
    public void TestFindDuplicateInColumnFalse()
    {
        // Arrange
        var number = 5;
        var cell = new Cell(6, 0, 0, false);
        var col = new Column();
        col.Add(cell);
        cell.Columns.Add(col);
        
        // Act
        var isDuplicate = cell.IsValueDuplicateInColumns(number);

        // Assert
        Assert.IsFalse(isDuplicate);
    }
    
    [TestMethod]
    public void TestFindDuplicateInSquareTrue()
    {
        // Arrange
        var number = 5;
        var cell = new Cell(number, 0, 0, false);
        var square = new Square(1);
        square.Add(cell);
        cell.Squares.Add(square);
        
        // Act
        var isDuplicate = cell.IsValueDuplicateInSquares(number);

        // Assert
        Assert.IsTrue(isDuplicate);
    }
    
    [TestMethod]
    public void TestFindDuplicateInSquareFalse()
    {
        // Arrange
        var number = 6;
        var cell = new Cell(number, 0, 0, false);
        var square = new Square(1);
        square.Add(cell);
        cell.Squares.Add(square);
        
        // Act
        var isDuplicate = cell.IsValueDuplicateInColumns(number);

        // Assert
        Assert.IsFalse(isDuplicate);
    }
    
    [TestMethod]
    public void TestAddComponentThrows()
    {
        // Arrange
        var cell = new Cell(1, 0, 0, false);

        // Act
        
        // Assert
        Assert.ThrowsException<NotImplementedException>
            (delegate { cell.Add(new Cell(0, 0, 0, false)); });
        
    }
    
    [TestMethod]
    public void TestIsCompositeFalse()
    {
        // Arrange
        var cell = new Cell(1, 0, 0, false);

        // Act
        var isComposite = cell.IsComposite();
        
        // Assert
        Assert.IsFalse(isComposite);
        
    }
    
    [TestMethod]
    public void TestIsEmptyCell()
    {
        // Arrange
        var expected = true;
        var cell = new Cell(0, 0, 0, false);

        // Act
        var isEmpty = cell.HasEmptyCell();
        
        // Assert
        Assert.AreEqual(expected, isEmpty);
        
    }
    
    [TestMethod]
    public void TestIsNotEmptyCell()
    {
        // Arrange
        var expected = false;
        var cell1 = new Cell(1, 0, 0, false);
        var cell2 = new Cell(5, 0, 0, false);
        var cell3 = new Cell(9, 0, 0, false);

        // Act
        var isEmptyOne = cell1.HasEmptyCell();
        var isEmptyTwo = cell2.HasEmptyCell();
        var isEmptyThree = cell3.HasEmptyCell();
        
        // Assert
        Assert.AreEqual(expected, isEmptyOne);
        Assert.AreEqual(expected, isEmptyTwo);
        Assert.AreEqual(expected, isEmptyThree);
        
    }
}