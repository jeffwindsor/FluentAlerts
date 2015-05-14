using System;
using System.Collections.Generic;

namespace FluentAlerts.Examples
{
    internal class Mother
    {
        private static readonly Guid TestGuid = new Guid("9149cae6-b300-44a7-8130-034f742477c4");
        private static readonly DateTime TestDate = new DateTime(2000, 01, 01);

        public static IEnumerable<object> CreateMixedList()
        {
            return new Object[]
            {
                "Test String",
                12.2324,
                TestGuid,
                TestDate,
                new Tuple<string,string>("a","b"),
                new []{"one","two","three"},
                new string[]{}
            };
        }
        public static TestClass CreateTestObject(int nestingDepth = 0)
        {
            return new TestClass()
            {
                DateProperty = TestDate,
                NumberProperty = (NumberEnum)nestingDepth,
                DateField = TestDate,
                NumberField = (NumberEnum)nestingDepth,
                InnerObject = (nestingDepth < 1) ? null : CreateTestObject(nestingDepth - 1),
                InnerObjectList = CreateMixedList()
            };
        }

        public static TestStruct CreateTestStruct(int nestingDepth)
        {
            return new TestStruct()
            {
                DateProperty = TestDate,
                NumberProperty = (NumberEnum)nestingDepth,
                DateField = TestDate,
                NumberField = (NumberEnum)nestingDepth,
                InnerObjectList =  CreateMixedList()
            };
        }

        public static Exception CreateNestedException(int nestingDepth)
        {
            try
            {
                ThrowNestedException(0,nestingDepth);
            }
            catch (Exception ex)
            {
                return ex;
            }

            //Should not happen, but compiler required it for all paths
            return null;
        }

        private static void ThrowNestedException(int nestingDepth, int maxDepth)
        {
            //At top, throw without a catch
            if (nestingDepth == maxDepth)
            {
                throw new ApplicationException(string.Format("Exception Message at Depth {0}", nestingDepth));
            }

            //Inside so re-curse
            try
            {
                ThrowNestedException(nestingDepth + 1, maxDepth);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Exception Message at Depth {0}", nestingDepth), ex);
            }
        }       

        #region ClassesAndStructs
        internal enum NumberEnum
        {
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven
        }

        internal class TestClass
        {
            public DateTime DateProperty { get; set; }
            public NumberEnum NumberProperty  { get; set; }
            public DateTime DateField;
            public NumberEnum NumberField;
            public TestClass InnerObject { get; set; }
            public IEnumerable<object> InnerObjectList { get; set; }
        }

        internal struct TestStruct
        {
            public DateTime DateProperty { get; set; }
            public NumberEnum NumberProperty { get; set; }
            public DateTime DateField;
            public NumberEnum NumberField;
            public IEnumerable<object> InnerObjectList { get; set; }
        }
        #endregion
    }
}
