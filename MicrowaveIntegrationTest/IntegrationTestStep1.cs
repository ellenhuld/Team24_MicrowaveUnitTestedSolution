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

        // sleeptime higher because it didnt work everytime with exact time
        [TestCase(120, 1100, 1, 59)]
        [TestCase(6,1100,0,5)] 
        [TestCase(120, 2500, 1, 58)]
        public void TimerTick_On_Display_ShowTime(int time, int sleepTime, int showMin, int showSec )
        {
            int power = 700;
            _cookController.StartCooking(power,time);
            
            Thread.Sleep(sleepTime);
            _display.Received(1).ShowTime(showMin, showSec);
        }


        // sleeptime higher because it didnt work everytime with exact time
        [TestCase(2,2500)]
        [TestCase(4,4500)]
        public void Timer_Expired_CookingIsDone(int time, int sleepTime)
        {
            int power = 700;
            _cookController.StartCooking(power,time);
            
            Thread.Sleep(sleepTime);
            _userInterface.Received(1).CookingIsDone();
        }
        [TestCase(2, 1000)]
        [TestCase(4, 3000)]
        public void Timer_Not_Expired_No_CookingIsDone(int time, int sleepTime)
        {
            int power = 700;
            _cookController.StartCooking(power, time);

            Thread.Sleep(sleepTime);
            _userInterface.DidNotReceive().CookingIsDone();
        }

    }
}
