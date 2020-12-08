using System;
using System.Collections.Generic;
using System.Text;
using Vrnz2.Infra.CrossCutting.Extensions;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Extensions
{
    public class DateTimeExtensionsTest
    {
        [Theory]
        [InlineData("2020-12-18 00:00:00")]
        public void FirstDayOfMonth_StringValue(string value)
        {
            var bla = value.FirstDayOfMonth();

            //Assert.True(cpf.IsValid());
        }
    }
}
