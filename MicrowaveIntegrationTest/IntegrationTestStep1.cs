using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MicrowaveIntegrationTest
{
    [TestFixture]
    public class IntegrationTestStep1
    {
        private CookController _cookController;
        private Timer _timer;
        private IUserInterface _userInterface;
        private IPowerTube _powerTube;
        private IDisplay _display;

        [SetUp]
        public void SetUp()
        {
            _userInterface = Substitute.For<IUserInterface>();
            _powerTube = Substitute.For<IPowerTube>();
            _display = Substitute.For<IDisplay>();
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [TestCase(6,1000,0,5)]
        [TestCase(120, 1000, 1,59)]
        [TestCase()]
        //[TestCase()]
        public void TimerTick(int time, int sleeptime, int showMin, int showSec )
        {
            int power = 700;
            _cookController.StartCooking(power,time);
            Thread.Sleep(sleeptime);
            _display.Received().ShowTime(showMin, showSec);
        }

        [TestCase()]
        public void Expired()
        {

        }
    }
}
