using Microsoft.AspNetCore.Mvc;
using S7PROSIMLib;

namespace PlcLampController.WEB.Controllers;

[ApiController]
[Route("api/sim")]
public class SimController : ControllerBase
{
    private S7ProSim sim;

    public SimController()
    {
        sim = new S7ProSim();

        sim.Connect();
        sim.BeginScanNotify();
        sim.SetScanMode(ScanModeConstants.ContinuousScan);
    }

    [HttpGet("status")]
    public IActionResult Status()
    {
        return Ok(sim.GetState());
    }

    [HttpGet("read")]
    public IActionResult Read(int byteIndex, int bitIndex)
    {
        object data = null;
        sim.ReadOutputPoint(byteIndex, bitIndex, PointDataTypeConstants.S7_Bit, ref data);

        return Ok(data);
    }

    [HttpGet("write")]
    public IActionResult Write(int byteIndex, int bitIndex, int data)
    {
        object toWrite = Convert.ToBoolean(data);

        sim.WriteInputPoint(byteIndex, bitIndex, ref toWrite);

        return Ok("Written");
    }
}