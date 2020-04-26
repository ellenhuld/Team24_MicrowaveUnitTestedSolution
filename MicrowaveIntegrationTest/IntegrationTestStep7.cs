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
    public class IntegrationTestStep7
    {
        private Door _door;
        private Button _timeButton;
        private Button _startCancelButton;
        private Button _powerButton;
        private ICookController _iCookController;
        private UserInterface _userInterface;
        private ILight _iLight;
        private Display _display;
        private IOutput _iOutput;

        public void SetUp()
        {
            _door = new Door();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _powerButton = new Button();
            _iCookController = Substitute.For<ICookController>();
            _iLight = Substitute.For<ILight>();
            _iOutput = Substitute.For<IOutput>();
            _display = new Display(_iOutput);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _iLight, _iCookController);
        }

        [TestCase]
        public void DisplayPower_PowerButtonPressed_PowerIsShown()
        {
            int power = 50;
            _powerButton.Press();
            string exspectedOutout = "" + power;
            _iOutput.Received(1).OutputLine(Arg.Is<string>(s => s.Contains(exspectedOutout)));
        }

        [TestCase(01, 00)]
        public void DisplayTime_PressTimeButton_TimeIsShown(int minute, int sec)
        {
            // Act
            _powerButton.Press();
            _timeButton.Press();

            string expectedOutput = minute + ":" + sec;

            //Assert
            _iOutput.Received(1).OutputLine(Arg.Is<string>(s => s.Contains(expectedOutput)));

        }

        [TestCase]
        public void CookingIsDone_WhenCookingIsDone_DisplayIsCleared()
        {
            _powerButton.Press();
            _startCancelButton.Press();
            _userInterface.CookingIsDone();

            _iOutput.Received(1).OutputLine("Display cleared");
        }
    }
}
