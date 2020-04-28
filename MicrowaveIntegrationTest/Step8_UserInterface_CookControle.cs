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
    class Step8_UserInterface_CookControle
    {
        private Door _door;
        private Button _timeButton;
        private Button _startCancelButton;
        private Button _powerButton;

        private Timer _fakeTimer;
        private Display _display;
        private PowerTube _powerTube;
        private IOutput _fakeOutput;
        private ILight _light;

        private ICookController _cookController;
        private UserInterface _userInterface;


        [SetUp]
        public void SetUp()
        {
            _door = new Door();
            _timeButton = new Button();
            _powerButton = new Button();
            _startCancelButton = new Button();

            _fakeTimer = Substitute.For<Timer>();
            _fakeOutput = Substitute.For<IOutput>();

            _display = new Display(_fakeOutput);
            _powerTube = new PowerTube(_fakeOutput);
            _light = new Light(_fakeOutput);

            _cookController = new CookController(_fakeTimer, _display, _powerTube, _userInterface);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);


        }

        //The user closes the door and start cooking and the timer starts
        //Display shows: 50 W" - Display shows: 01:00" - Light is turned on - PowerTube works with 50
        [Test]
        public void OnStartCancelPressed_StartCooking_StartCookingIsCalledWithOutput()
        {
            //act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            

            //assert
            _fakeOutput.Received().OutputLine(Arg.Is<string>(s => s.Contains("50")));
        }

        //The user clicks on StartCancel button, opens the door and removes the food - the timer stops
        [Test]
        public void OnDoorOpened_DoorOpens_StopCookingIsCalledWithStopTimer()
        {
            //act
            _startCancelButton.Press();

            _door.Open();


            // assert
            _fakeTimer.Received(1).Stop();

        }

    }
}

