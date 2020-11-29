using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestExample.Test
{
    class PasswordControllerTestFixture
    {
        [Test,
            TestCase("abcd", true),
            TestCase("ABCD1234", true),
            TestCase("abcd1234", true),
            TestCase(len)
    }
}
