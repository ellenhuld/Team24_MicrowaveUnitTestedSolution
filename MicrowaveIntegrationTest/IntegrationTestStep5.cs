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
    public class IntegrationTestStep5
    {
        private Button _button;
        private IUserInterface _userInterface;
        private IDoor _door;
        private ICookController _cookController;

    }
}
