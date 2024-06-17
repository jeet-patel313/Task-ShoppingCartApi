using System;
using Xunit;

namespace ShoppingCartApi.Tests
{
  public class UnitTest1
  {
    public UnitTest1()
    {
      // Set the environment variable in the constructor
      Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
    }

    [Fact]
    public void BasicTest()
    {
      // Arrange
      int expected = 1;
      int actual = 1;

      // Act

      // Assert
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void CheckEnvironmentSetup()
    {
      // Arrange
      string expectedEnvironment = "Development";

      // Act
      string actualEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

      // Assert
      Assert.Equal(expectedEnvironment, actualEnvironment);
    }
  }
}
