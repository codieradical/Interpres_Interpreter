using Interpreter.Syntax;
using Interpreter.Syntax.Operations.Numeracy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Interpres_UnitTests
{
    [TestClass]
    public class AddOperationTest
    {
        [TestMethod]
        public void ConcatenateStringFloat()
        {
            // Arrange
            ValueSyntax stringValue = new ValueSyntax("height ");
            ValueSyntax floatValue = new ValueSyntax(5.5f);

            // Act
            object result = new AddOperation().Operate(stringValue, floatValue);

            // Assert
            Assert.AreEqual(result, "height 5.5");
        }
    }
}
