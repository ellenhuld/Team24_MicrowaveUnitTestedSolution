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


    }
}
