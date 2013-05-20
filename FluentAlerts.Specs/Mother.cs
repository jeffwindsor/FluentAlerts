using System;
using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Transformers.Formatters;

namespace FluentAlerts.Specs
{
    public static class Mother
    {
        public static string GetFormat(object o)
        {
            return string.Format("Extra {0} Extra", o);
        }

        public static NestedTestClass GetNestedTestClass(int nestingDepth)
        {
            return new NestedTestClass()
                {
                    TestDate = DateTime.Now,
                    Number = (NumberEnum) nestingDepth,
                    Child = (nestingDepth < 1)
                                ? null
                                : GetNestedTestClass(nestingDepth - 1),
                    Children = (nestingDepth < 1)
                                   ? Enumerable.Empty<NestedTestClass>()
                                   : from i in Enumerable.Range(0, 5) select GetNestedTestClass(nestingDepth - 1)
                };
        }

        public static NestedTestStruct GetNestedTestStruct(int nestingDepth)
        {
            return new NestedTestStruct()
                {
                    TestDate = DateTime.Now,
                    Number = (NumberEnum) nestingDepth,
                    Children = (nestingDepth < 1)
                                   ? Enumerable.Empty<NestedTestStruct>()
                                   : from i in Enumerable.Range(0, 5) select GetNestedTestStruct(nestingDepth - 1)
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
        Five,
        Six,
        Seven
    }

    public class NestedTestClass
    {
        public DateTime TestDate { get; set; }
        public NumberEnum Number { get; set; }
        public NestedTestClass Child { get; set; }
        public IEnumerable<NestedTestClass> Children { get; set; } 
    }

    public struct NestedTestStruct
    {
        public DateTime TestDate { get; set; }
        public NumberEnum Number { get; set; }
        public IEnumerable<NestedTestStruct> Children { get; set; }
    }

}
