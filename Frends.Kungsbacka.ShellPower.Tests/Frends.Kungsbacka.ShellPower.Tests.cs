using NUnit.Framework;
using System;

namespace Frends.Kungsbacka.ShellPower.Tests
{
	[TestFixture]
	class TestClass
	{
		[Test]
		public void CreateSession_ReturnsCreateSessionResultWithSession()
		{
			// Arrange

			// Act
			var result = PS.CreateSession();

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Session);
		}

		[Test]
		public void RemoveSession_DisposesSessionAndReturnsRemoveSessionResult()
		{
			// Arrange
			var session = new Session();
			var input = new RemoveSessionParameters { Session = session };

			// Act
			var result = PS.RemoveSession(input);

			// Assert
			Assert.IsNull(input.Session); // Ensure the session has been disposed and set to null
			Assert.IsNotNull(result);
		}

		[Test]
		public void InvokeCommand_ReturnsInvokeCommandResultWithResult()
		{
			// Arrange
			var input = new InvokeCommandParameters
			{
				Command = "Get-Date",
				Parameters = new[]
				{
					new CommandParameter { Name = "Format", Value = "yyyy-MM-dd" },
				}
			};
			var options = new InvokeCommandOptions();

			// Act
			var result = PS.InvokeCommand(input, options);

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Result);
		}

		[Test]
		public void InvokeCommand_WithSessionOption_ReturnsInvokeCommandResultWithResult()
		{
			// Arrange
			var input = new InvokeCommandParameters
			{
				Command = "Get-Date",
				Parameters = new[]
				{
					new CommandParameter { Name = "Format", Value = "yyyy-MM-dd" }
				}
			};
			var options = new InvokeCommandOptions { Session = new Session() };

			// Act
			var result = PS.InvokeCommand(input, options);

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Result);
		}
		[Test]
		public void InvokeCommand_ReturnsCorrectResultWhenCommandIsSpecified()
		{
			// Arrange
			var input = new InvokeCommandParameters
			{
				Command = "Get-Date",
				Parameters = new[]
				{
					new CommandParameter { Name = "Format", Value = "yyyy-MM-dd" },
				}
			};
			var options = new InvokeCommandOptions();
			var expectedResult = DateTime.Now.ToString("yyyy-MM-dd");

			// Act
			var result = PS.InvokeCommand(input, options);


			// Assert
			Assert.AreEqual(result.Result.ToString(), expectedResult);
		}
	}
}