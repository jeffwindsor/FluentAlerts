using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Specs.FluentAlerts
{
    public static class Mother
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
            try
            {
                //Re-curse to Bottom
                if (nestingDepth > 1) ThrowNestedException(nestingDepth - 1);

                //Throw exception at bottom
                throw new ApplicationException(string.Format("Depth {0}", nestingDepth - 1));
            }
            catch (Exception ex)
            {
                //Wrap Nested Exception in new Exception and throw
                throw new ApplicationException(string.Format("Depth {0}", nestingDepth), ex);
            }
        }       
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
