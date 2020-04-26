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
        private UserInterface _uut;
        private ICookController _iCookController;
        private IOutput _iOutput;
        private Door _door;
        private Button _button1;
        private Button _button2;
        private Button _button3;
        private Light _light;
        private Display _display;


        [SetUp]
        public void SetUp()
        {
            _iCookController = Substitute.For<ICookController>();
            _iOutput = Substitute.For<IOutput>();
            _door = new Door();
            _button1 = new Button();
            _button2 = new Button();
            _button3 = new Button();
            _light = new Light(_iOutput);
            _display = new Display(_iOutput);
            _uut = new UserInterface(_button1, _button2, _button3, _door, _display, _light, _iCookController);
        }
    }
}
