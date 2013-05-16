using System;
using System.Collections.Generic;

namespace FluentAlerts.Specs
{
    public static class StepsMother
    {

        public static Exception GetNestedException(int nestingDepth)
        {
            try
            {
                ThrowNestedException(nestingDepth);
            }
            catch (Exception ex)
            {
                return ex;
            }

            //Should not happen, but compiler required it for all paths
            return null;
        }

        private static void ThrowNestedException(int nestingDepth)
        {
            if (nestingDepth == 0)
            {
                throw new ApplicationException(string.Format("Depth {0}", nestingDepth));
            }
            try
            {
                ThrowNestedException(nestingDepth - 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Depth {0}", nestingDepth), ex);
            }
        }       
    }
    //Simulates an external exception derived from the alert exception
    public class SpecsAlertException : AlertException
    {
        public SpecsAlertException(IAlert alert) : base(alert) { }
        public SpecsAlertException(IAlert alert, Exception inner) : base(alert, inner) { }
    }

    public class TestObject
    {
        public static TestObject Create(int toDepth = 0)
        {
            return toDepth < 0
                ? null //End Recursion
                : new TestObject() { One = toDepth.ToString(), Two = toDepth, Inner = Create(--toDepth) };
        }
        public static IEnumerable<TestObject> CreateList(int count)
        {
            var result = new List<TestObject>();
            for (var i = 0; i < count;++i )
            {
                result.Add(Create());
            }
            return result;
        }

        public string One { get; set; }
        public decimal Two { get; set; }
        public TestObject Inner { get; set; }
    }
}
