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
    [TestFixture]
    public class Step4_Door_UserInterface
    {
        private IUserInterface _userInterface;
        private IButton _fakePowerButton;
        private IButton _fakeTimeButton;
        private IButton _fakeStartCancelButton;
        private IDisplay _fakeDisplay;
        private ILight _fakeLight;
        private ICookController _fakeCookController;

        private IDoor _door;

        [SetUp]
        public void SetUp()
        {
            _fakePowerButton = Substitute.For<IButton>();
            _fakeTimeButton = Substitute.For<IButton>();
            _fakeStartCancelButton = Substitute.For<IButton>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeLight = Substitute.For<ILight>();
            _fakeCookController = Substitute.For<ICookController>();

            _door = new Door();

            _userInterface = new UserInterface(_fakePowerButton, _fakeTimeButton, _fakeStartCancelButton, _door, _fakeDisplay, _fakeLight, _fakeCookController);

        }

        [Test]
        public void Open_WhenDoorOpens_OutputLightTurnsOn()
        {
            _door.Open();
            _fakeLight.Received().TurnOn();

        }

        [Test]
        public void Close_WhenDoorCloses_OutputLightTurnedOff()
        {

            // act -> døren skal først åbnes før den kan lukkes
            _door.Open();

            //Act and assert
            _door.Close();
            _fakeLight.Received().TurnOff();

        }
    }


}


