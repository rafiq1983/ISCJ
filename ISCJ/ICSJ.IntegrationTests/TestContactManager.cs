using System;
using System.Configuration;
using BusinessLogic;
using MA.Common;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Microsoft.Extensions.Configuration;

namespace ICSJ.IntegrationTests
{
    public class TestContactManager
    {
        Mock myInterfaceMock;

        public TestContactManager()
        {
            
        }

        [SetUp]
        public void Setup()
        {
            myInterfaceMock = new Mock<ILogger<SignupManager>>();

            SignupManager mgr = new SignupManager(null, null);
        }

        [Test]
        public void Test1()
        {
           
        }
    }
    
}