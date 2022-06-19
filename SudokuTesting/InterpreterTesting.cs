using System;
using Construction.Builder;
using Construction.Interpreters;
using Import.Import;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SudokuTesting;

[TestClass]
public class InterpreterTesting
{
    [TestMethod]
    public void TestInterpreterNoEmptyDataFile()
    {
        // Arrange
        var interpreter = new Interpreter(new Mock<BoardBuildDirector>().Object,
            new Mock<BoardBuilder>().Object);
        var file = new BoardFile(new string[1], ".4x4");
        var file2 = new BoardFile(Array.Empty<string>(), ".4x4");
        var file3 = new BoardFile(new string[1], ".9x9");

        // Act & Assert
        Assert.ThrowsException<ArgumentException>
            (delegate { interpreter.Interpret(file); });
        
        Assert.ThrowsException<ArgumentException>
            (delegate { interpreter.Interpret(file2); });
        
        Assert.ThrowsException<ArgumentException>
            (delegate { interpreter.Interpret(file3); });
    }

    [TestMethod]
    public void TestInterpreterBuildBoard()
    {
        // Arrange
        var nine = "700509001000000000150070063003904100000050000002106400390040076000000000600201004";
        var four = "0340400210030210";
        var six = "003010560320054203206450012045040100";

        var interpreter = new Interpreter(new Mock<BoardBuildDirector>().Object,
            new Mock<BoardBuilder>().Object);
        var file = new BoardFile(new [] {four}, ".4x4");
        var file2 = new BoardFile(new [] {six}, ".6x6");
        var file3 = new BoardFile(new [] {nine}, ".9x9");

        // Act
        var fourBoard = interpreter.Interpret(file);
        var sixBoard = interpreter.Interpret(file2);
        var nineBoard = interpreter.Interpret(file3);
        
        // Assert
        Assert.IsNotNull(fourBoard);
        Assert.IsNotNull(sixBoard);
        Assert.IsNotNull(nineBoard);
        
    }
}