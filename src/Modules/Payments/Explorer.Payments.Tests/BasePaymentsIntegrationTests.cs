using Explorer.BuildingBlocks.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests;

public class BasePaymentsIntegrationTests : BaseWebIntegrationTest<PaymentsTestFactory>
{
    public BasePaymentsIntegrationTests(PaymentsTestFactory factory) : base(factory) { }
}
