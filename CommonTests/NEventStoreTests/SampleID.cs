using GuDash.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTests.NEventStoreTests
{
    public class SampleID : Identity
    {
        public SampleID() : base() { }

        public SampleID(Guid id) : base(id.ToString()) {}
    }
}
