using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace MicrowaveIntegrationTest
{
    [TestFixture]
    public class IntegrationTestStep4
    {
        private Door _door;
        private UserInterface _userInterface;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private ICookController _cookController;
        private ILight _light;
        private IDisplay _display;

        [SetUp]
        public void SetUp()
        {

            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _cookController = Substitute.For<ICookController>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            
            _door = new Door();

            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void DoorOpen_LightTurnOn_DoorClose_LightTurnOff()
        {
            // One test because you have to open before you can close
            _door.Open();
            _light.Received().TurnOn();
            _door.Close();
            _light.Received().TurnOff();
        }

    }
}
