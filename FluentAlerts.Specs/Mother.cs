using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.Specs
{
    public static class Mother
    {
        public static NestedTestClass GetNestedTestClass(int nestingDepth)
        {
            if (nestingDepth < 0) return null;

            return new NestedTestClass()
                {
                    TestDate = DateTime.Now,
                    Number = (NumberEnum) nestingDepth,
                    Child = GetNestedTestClass(nestingDepth - 1),
                    Children = from i in Enumerable.Range(0, 5) select GetNestedTestClass(nestingDepth)
                };
        }

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

    public enum NumberEnum
    {
        One,
        Two,
        Three,
        Four,
        Five
    }

    public class NestedTestClass
    {
        public DateTime TestDate { get; set; }
        public NumberEnum Number { get; set; }
        public NestedTestClass Child { get; set; }
        public IEnumerable<NestedTestClass> Children { get; set; } 
    }
}
