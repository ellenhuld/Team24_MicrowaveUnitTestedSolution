using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;

namespace MicrowaveIntegrationTest
{
    [TestFixture]
    class Step2_CookController_PowerTube
    {

        private CookController _cookController;
        private PowerTube _powerTube;

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

        //Power value must be between 1 and 100
        [TestCase(50, 10)]
        [TestCase(60, 20)]
        [TestCase(100, 60)]
        public void StartCookController_PowerTubeIsValid_TurnOnCalledIsCalledWithPowerOutput(int power, int timer)
        {
            //Arrange
            _cookController.StartCooking(power, timer);

            //Assert
            _fakeOutput.Received(1).OutputLine($"PowerTube works with {power} W");

        }


        //Power is above limit (100) and therefore throws an exception
        [TestCase(2000, 10)]
        [TestCase(-1, 10)]
        [TestCase(0, 10)]
        public void CookController_PowerTubeAboveLimit_ThrowsExceptionIsCalled(int power, int timer)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _powerTube.TurnOn(power));
        }

        //The cooking is stopped and therefor no output
        [Test]
        public void StopCookController_TurnOffIsCalled_NoOutput()
        {
            _cookController.StartCooking(50, 60);
            _cookController.Stop();

            _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        //The event has expired and the cooking is done
        [Test]
        public void ExpiredCookController_TimerEventIsExpired_CookControllerStopped()
        {
            _cookController.StartCooking(50, 60);
            _fakeTimer.Expired += Raise.EventWith(this, EventArgs.Empty);

            _fakeOutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }
    }
}