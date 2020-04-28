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

        // Fail compares to the order in testcases...why? alone test they succeeded
        [TestCase(120, 1000, 1, 59)]
        [TestCase(6,1000,0,5)] // fail sometimes
        [TestCase(120, 2000, 1, 58)]// Shouldn't fail (doesn't fail everytime)
        public void TimerTick_On_Display_ShowTime(int time, int sleepTime, int showMin, int showSec )
        {
            int power = 700;
            _cookController.StartCooking(power,time);
            
            Thread.Sleep(sleepTime);
            _display.Received(1).ShowTime(showMin, showSec);
        }

        [TestCase(120, 1000, 1, 59)]
        public void TimerTick_On_Display_ShowTime1(int time, int sleepTime, int showMin, int showSec)
        {
            int power = 700;
            _cookController.StartCooking(power, time);

            Thread.Sleep(sleepTime);
            _display.Received(1).ShowTime(showMin, showSec);
        }

        [TestCase(6, 1000, 0, 5)]
        public void TimerTick_On_Display_ShowTime2(int time, int sleepTime, int showMin, int showSec)
        {
            int power = 700;
            _cookController.StartCooking(power, time);

            Thread.Sleep(sleepTime);
            _display.Received(1).ShowTime(showMin, showSec);
        }
        [TestCase(120, 2000, 1, 58)]
        public void TimerTick_On_Display_ShowTime3(int time, int sleepTime, int showMin, int showSec)
        {
            int power = 700;
            _cookController.StartCooking(power, time);

            Thread.Sleep(sleepTime);
            _display.Received(1).ShowTime(showMin, showSec);
        }
        

        // Again problems when it goes in the testcases
        [TestCase(2,2000)]
        [TestCase(4,4000)]
        public void Timer_Expired_CookingIsDone(int time, int sleepTime)
        {
            int power = 700;
            _cookController.StartCooking(power,time);
            
            Thread.Sleep(sleepTime);
            _userInterface.Received(1).CookingIsDone();
        }
        [TestCase(2, 1000)]
        [TestCase(4, 4000)]
        public void Timer_Not_Expired_No_CookingIsDone(int time, int sleepTime)
        {
            int power = 700;
            _cookController.StartCooking(power, time);

            Thread.Sleep(sleepTime);
            _userInterface.DidNotReceive().CookingIsDone();
        }
        [TestCase(4, 4000)]
        public void Timer_Not_Expired_No_CookingIsDone1(int time, int sleepTime)
        {
            int power = 700;
            _cookController.StartCooking(power, time);

            Thread.Sleep(sleepTime);
            _userInterface.DidNotReceive().CookingIsDone();
        }

    }
}
