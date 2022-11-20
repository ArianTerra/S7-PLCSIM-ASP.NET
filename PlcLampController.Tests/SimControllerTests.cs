using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcLampController.WEB.Controllers;

namespace PlcLampController.Tests;

[TestClass]
public class SimControllerTests
{
    /// <summary>
    /// Delay in ms before reading value.
    /// Sometimes output values is not updated immediately after inputting a value,
    /// so I created a small delay before reading value.
    /// </summary>
    private const int DELAY = 1000;

    [TestInitialize]
    public void Initialize()
    {
        var controller = new SimController();

        var response = controller.Status() as OkObjectResult;
        var responseString = response.Value.ToString();

        if (responseString != "RUN_P" && responseString != "RUN")
        {
            throw new Exception($"The PLCSIM simulator is not working or stopped. Status: {responseString}");
        }
    }

    [TestCleanup]
    public void Cleanup()
    {
        var controller = new SimController();

        controller.Write(0, 0, 0);
        controller.Write(0, 1, 0);
        controller.Write(0, 2, 0);
    }

    [TestMethod]
    public void Read_0111_1()
    {
        var controller = new SimController();

        controller.Write(0, 0, 1);
        controller.Write(0, 1, 1);
        controller.Write(0, 2, 1);

        Thread.Sleep(DELAY);

        var result = controller.Read(0, 0) as OkObjectResult;
        var resultString = result.Value.ToString();

        Assert.AreEqual("True", resultString);
    }

    [TestMethod]
    public void Read_0011_1()
    {
        var controller = new SimController();

        controller.Write(0, 0, 1);
        controller.Write(0, 1, 1);
        controller.Write(0, 2, 0);

        Thread.Sleep(DELAY);

        var result = controller.Read(0, 0) as OkObjectResult;
        var resultString = result.Value.ToString();

        Assert.AreEqual("True", resultString);
    }

    [TestMethod]
    public void Read_0101_1()
    {
        var controller = new SimController();

        controller.Write(0, 0, 1);
        controller.Write(0, 1, 0);
        controller.Write(0, 2, 1);

        Thread.Sleep(DELAY);

        var result = controller.Read(0, 0) as OkObjectResult;
        var resultString = result.Value.ToString();

        Assert.AreEqual("True", resultString);
    }

    [TestMethod]
    public void Read_0001_0()
    {
        var controller = new SimController();

        controller.Write(0, 0, 1);
        controller.Write(0, 1, 0);
        controller.Write(0, 2, 0);

        Thread.Sleep(DELAY);

        var result = controller.Read(0, 0) as OkObjectResult;
        var resultString = result.Value.ToString();

        Assert.AreEqual("False", resultString);
    }

    [TestMethod]
    public void Read_0110_0()
    {
        var controller = new SimController();

        controller.Write(0, 0, 0);
        controller.Write(0, 1, 1);
        controller.Write(0, 2, 1);

        Thread.Sleep(DELAY);

        var result = controller.Read(0, 0) as OkObjectResult;
        var resultString = result.Value.ToString();

        Assert.AreEqual("False", resultString);
    }
}