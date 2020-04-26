using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace MicrowaveIntegrationTest
{
    [TestFixture]
    class Step5_Button_UserInterface
    {
        private IButton _timeButton;
        private IButton _powerButton;
        private IButton _startCancelButton;
        private IUserInterface _userInterface;
        private IDoor _fakeDoor;
        private IDisplay _fakeDisplay;
        private ILight _fakeLight;

        private ICookController _fakeCookController;


        [SetUp]
        public void SetUp()
        {

            _timeButton = new Button();
            _powerButton = new Button();
            _startCancelButton = new Button();
            _fakeDoor = Substitute.For<IDoor>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeLight = Substitute.For<ILight>();

            _fakeCookController = Substitute.For<ICookController>();
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _fakeDoor, _fakeDisplay, _fakeLight, _fakeCookController);
        }

        [TestCase(1, 50)]
        [TestCase(2, 100)]
        [TestCase(14, 700)]
        public void OnPowerEvent_MethodCalled_ShowPowerIsCalled(int pressed, int power)
        {
            for (int i = 1; i <= pressed; i++)
            {
                _powerButton.Press();

            }

            _fakeDisplay.Received().ShowPower(power);
        }
        
        [TestCase(1, 01, 00)]
        [TestCase(2, 02, 00)]
        [TestCase(3, 03, 00)]
        [TestCase(10, 10, 00)]
        public void OnTimeEvent_MethodCalled_ShowTimeIsCalled(int pressed, int min, int sec)
        {
            //act
            _powerButton.Press();

            for (int i = 0; i < pressed; i++)
            {

                _timeButton.Press();
            }


            // assert
            _fakeDisplay.Received().ShowTime(min, sec);

        }

        [TestCase]
        public void StartCancelEvent_MethodCalled_TurnOnIsCalled()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _fakeLight.Received(1).TurnOn();
        }

        [TestCase]
        public void StartCancelEvent_MethodCalledTwice_TurnOffIsCalled()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();

            _fakeLight.Received(1).TurnOff();
        }


    }


}
