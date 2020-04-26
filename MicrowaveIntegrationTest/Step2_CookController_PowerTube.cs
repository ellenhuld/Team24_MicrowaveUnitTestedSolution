using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicrowaveIntegrationTest
{
    [TestFixture]
    class Step2_CookController_PowerTube
    {

        private ICookController _cookController;
        private IPowerTube _powerTube;

        private IUserInterface _fakeUserInterface;
        private ITimer _fakeTimer;
        private IDisplay _fakeDisplay;
        private IOutput _fakeOutput;

        [SetUp]
        public void SetUp()
        {
            _fakeUserInterface = Substitute.For<IUserInterface>();
            _fakeTimer = Substitute.For<ITimer>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeOutput = Substitute.For<IOutput>();

            _powerTube = new PowerTube(_fakeOutput);

            _cookController = new CookController(_fakeTimer, _fakeDisplay, _powerTube, _fakeUserInterface);
        }

        [TestCase(50, 10)]
        [TestCase(60, 20)]
        [TestCase(70, 30)]
        public void StartCookController_PowerTubeIsValid_TurnOnCalledWithPowerOutput(int power, int timer)
        {
            //Arrange
            _cookController.StartCooking(power, timer);

            //Assert
            _fakeOutput.Received(1).OutputLine($"PowerTube works with {power} W");

        }

        [TestCase(2000, 10)]
        [TestCase(-1, 10)]
        [TestCase(0, 10)]
        public void CookControllerPowerTube_PowerTubeAboveLimit_ThrowsException(int power, int timer)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => _powerTube.TurnOn(power));
        }

        [TestCase]
        public void StopCookController_TurnOffCalled_NoOutput()
        {
            _cookController.StartCooking(50, 50);
            _cookController.Stop();

            _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));

        }

        [TestCase]
        public void CookControllerPowerTube_TimerEventExspired_CookControllerStopped()
        {
            _cookController.StartCooking(50, 50);
            _fakeTimer.Expired += Raise.EventWith(this, EventArgs.Empty);

            _fakeOutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }
    }
}
