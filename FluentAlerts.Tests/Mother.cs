using System;

namespace Tests.FluentAlerts
{
    public static class Mother
    {
        public static Exception GetNestedException(int nestingDepth = 1)
        {
            try { ThrowNestedException(nestingDepth);}
            catch (Exception ex) { return ex; }

            //Should not happen, but compiler required it for all paths
            return null;
        }
        public static void ThrowNestedException(int nestingDepth = 1)
        {
            try
            {
                //Re-curse to Bottom
                if (nestingDepth > 0) ThrowNestedException(nestingDepth - 1);
                
                //Throw exception at bottom
                throw new ApplicationException(string.Format("Depth {0}", nestingDepth));
            }
            catch (Exception ex)
            {
                //Wrap Nested Exception in new Exception and throw
                throw new ApplicationException(string.Format("Depth {0}", nestingDepth), ex);
            }
        }


        public class TestNode
        {
            public static TestNode Create(int toDepth = 0)
            {
                return toDepth < 0
                    ? null //End Recursion
                    : new TestNode() { One = toDepth.ToString(), Two = toDepth, Inner = Create(--toDepth) };
            }

            public string One { get; set; }
            public decimal Two { get; set; }
            public TestNode Inner { get; set; }
        }
    }
}