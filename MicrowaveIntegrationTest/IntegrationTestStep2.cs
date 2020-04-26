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
    public class IntegrationTestStep2
    {
        private CookController _cookController;
        private IPowerTube _powerTube;
        private IUserInterface _userInterface;
        private ITimer _timer;
        private IDisplay _display;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _cookController = new CookController();
            _powerTube = new PowerTube();
            _userInterface = Substitute.For<IUserInterface>();
            _timer = Substitute.For<ITimer>();
            _display = Substitute.For<IDisplay>();
            _output = Substitute.For<IOutput>();
        }

    }
}
