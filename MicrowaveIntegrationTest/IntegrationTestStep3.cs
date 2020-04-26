using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;


namespace MicrowaveIntegrationTest
{
    [TestFixture]
    public class IntegrationTestStep3
    {
        private CookController _cookController;
        private IUserInterface _iUserInterface;
        private ITimer _iTimer;
        private IPowerTube _iPowerTube;
        private IOutput _iOutput;
        private Display _display;

        [SetUp]
        public void SetUp()
        {
            _iUserInterface = Substitute.For<IUserInterface>();
            _iTimer = Substitute.For<ITimer>();
            _iPowerTube = Substitute.For<IPowerTube>();
            _iOutput = Substitute.For<IOutput>();
            _display = new Display(_iOutput);
            _cookController = new CookController(_iTimer, _display, _iPowerTube, _iUserInterface);
        }

        [Test]
        public void OnTimerTick_ShowTime() // Tester at display modtager en string der indeholder "Display shows"
        {
            _iTimer.TimerTick += Raise.Event();
            _iOutput.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display shows")));
        }

    }
}
