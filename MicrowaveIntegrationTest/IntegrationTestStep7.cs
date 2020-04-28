using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace MicrowaveIntegrationTest
{
    [TestFixture]
    public class IntegrationTestStep7
    {
        private Door _door;
        private Button _timeButton;
        private Button _startCancelButton;
        private Button _powerButton;
        private UserInterface _userInterface;
        private ICookController _cookController;
        private ILight _light;
        private Display _display;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _cookController = Substitute.For<ICookController>();
            _light = Substitute.For<ILight>();
            _output = Substitute.For<IOutput>();

            _door = new Door();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _powerButton = new Button();
            _display =new Display(_output);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }
        [Test]
        public void PowerButtonIsPressed_DisplayShowsPower()
        {
            _powerButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(s => s.Contains("50")));
        }

        [Test]
        public void TimeButtonIsPressed_DisplayShowsTime()
        {
            _powerButton.Press();
            _timeButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void StartCancelButtonIsPressed_Display()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void StartCancelButton_PowerStateCancel()
        {
            _powerButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display cleared")));
        }

        [Test]
        public void StartCancelButton_While_Cooking()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display cleared")));
        }

        [Test]
        public void DoorOpens_While_In_PowerState()
        {
            _powerButton.Press();
            _door.Open();
            _output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display cleared")));
        }


        [Test]
        public void DoorOpens_While_In_SetTime()
        {
            _powerButton.Press();
            _timeButton.Press();
            _door.Open();
            _output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display cleared")));
        }

        [Test]
        public void DoorOpens_While_Cooking()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _door.Open();
            _output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display cleared")));
        }
    }
}
