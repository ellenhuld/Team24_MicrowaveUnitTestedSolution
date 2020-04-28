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
    public class IntegrationTestStep6
    {
        private UserInterface _userInterface;
        private ICookController _iCookController;
        private IOutput _iOutput;
        private Door _door;
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private Light _light;
        private Display _display;


        [SetUp]
        public void SetUp()
        {
            _iCookController = Substitute.For<ICookController>();
            _iOutput = Substitute.For<IOutput>();
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _light = new Light(_iOutput);
            _display = new Display(_iOutput);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _iCookController);
        }

        [Test]
        public void DoorOpen_LightIsOn() // Her åbnes døren til mikroovnen, lyset tænder
        {
            _door.Open();

            _iOutput.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void DoorClosed_LightIsOff() // Døren åbnes, lyset tænder. Døren lukkes, lyset slukker igen
        {
            _door.Open();
            _iOutput.Received(1).OutputLine("Light is turned on");
           
            _door.Close();
            _iOutput.Received(1).OutputLine("Light is turned off");
        }

        [Test]
        public void DoorIsClosed_StartCooking_LightIsOn() //Ovnen indstilles og sættes til at køre, lyset er tændt mens ovnen kører
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _iOutput.Received(1).OutputLine("Light is turned on");
        }


        [Test]
        public void DoorIsClosed_CookingDone_LightIsOff() // Ovnen indstilles og sættes igang. Ovnen kører programmet færdigt, hvorefter lyset slukker.
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _iOutput.Received(1).OutputLine("Light is turned on");

            _userInterface.CookingIsDone();

            _iOutput.Received(1).OutputLine("Light is turned off");
        }

        [Test]
        public void DoorIsClosed_CookingStarted_CancelCooking_LightIsOff() // Ovnen indstilles og sættes igang. Der trykkes på cancel inden programmet er kørt færdigt, lyset slukker.
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _iOutput.Received(1).OutputLine("Light is turned on");
            _startCancelButton.Press();

            _iOutput.Received(1).OutputLine("Light is turned off");
        }
    }
}
