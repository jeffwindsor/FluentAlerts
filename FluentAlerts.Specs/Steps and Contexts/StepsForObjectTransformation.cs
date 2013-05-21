using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAlerts.Transformers;
using FluentAlerts.Transformers.TypeInformers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace FluentAlerts.Specs
{
    [Binding]
    public class StepsForObjectTransformation
    {
        private TypeInfo _typeInfo;
        private MemberPath _memberPath;
        private BaseTypeInfoSelector _selector;
        private AlertContext _context;
        public StepsForObjectTransformation(AlertContext context)
        {
            _context = context;
        }

        [Given(@"I have a default type informer")]
        public void GivenIHaveADefaultTypeInformer()
        {
            _selector = new DefaultTypeInfoSelector();
        }

        [Given(@"I limit the informer to (.*) properties")]
        public void GivenILimitTheInformerToProperties(string typeString)
        {
            switch (typeString)
            {
                case "Null":
                    //no rule required
                    break;
                case "NestedTestClass":
                    _selector.Rules.Add((info, obj, path) => info.PropertyInfos = FilterOnType<NestedTestClass>(info.PropertyInfos));
                    break;
                case "NestedTestStruct":
                    _selector.Rules.Add((info, obj, path) => info.PropertyInfos = FilterOnType<NestedTestStruct>(info.PropertyInfos));
                    break;
                case "String":
                    _selector.Rules.Add((info, obj, path)=>  info.PropertyInfos = FilterOnType<string>(info.PropertyInfos));
                    break;
                case "DateTime":
                    _selector.Rules.Add((info, obj, path) => info.PropertyInfos = FilterOnType<DateTime>(info.PropertyInfos));
                    break;
                case "Integer":
                    _selector.Rules.Add((info, obj, path) =>  info.PropertyInfos = FilterOnType<int>(info.PropertyInfos));
                    break;
                case "Long":
                    _selector.Rules.Add((info, obj, path) =>  info.PropertyInfos = FilterOnType<long>(info.PropertyInfos));
                    break;
                case "Float":
                    _selector.Rules.Add((info, obj, path) =>  info.PropertyInfos = FilterOnType<float>(info.PropertyInfos));
                    break;
                case "Double":
                    _selector.Rules.Add((info, obj, path) =>  info.PropertyInfos = FilterOnType<double>(info.PropertyInfos));
                    break;
                case "NumberEnum":
                    _selector.Rules.Add((info, obj, path) => info.PropertyInfos = FilterOnType<NumberEnum>(info.PropertyInfos));
                    break;
                default:
                    throw new ArgumentException("Type Not Known", "type");
            }
        }

        [Given(@"I limit the informer to (.*) properties at (.*)")]
        public void GivenILimitTheInformerToPropertiesAt(string typeString, string targetPathString)
        {
            _memberPath = new MemberPath(targetPathString);
            //current path plus property name = target path
            //TODO: what about recursing/partial paths cause we need to get down to path?
            _selector.Rules.Add((info, obj, path) => 
                info.PropertyInfos = info.PropertyInfos.Where(pi =>
                    path.Concat(new[] { pi.Name }).SequenceEqual(_memberPath)));
        }
        


        [When(@"I get the object's type info")]
        public void WhenIGetTheObjectSTypeInfo()
        {
            _typeInfo = _selector.Find(_context.TestValue, _memberPath);
        }

        

        [Then(@"all the objects properties are listed in the type info")]
        public void ThenAllTheObjectsPropertiesAreListedInTheTypeInfo()
        {
            var expected = _context.TestValue.GetType().GetProperties();
            _typeInfo.PropertyInfos.SequenceEqual(expected).Should().BeTrue();
        }

        [Then(@"all the objects fields are listed in the type info")]
        public void ThenAllTheObjectsFieldsAreListedInTheTypeInfo()
        {
            var expected = _context.TestValue.GetType().GetFields();
            _typeInfo.FieldInfos.SequenceEqual(expected).Should().BeTrue();
        }

        [Then(@"only the objects (.*) properties are listed in the type info")]
        public void ThenOnlyTheObjectsPropertiesAreListedInTheTypeInfo(string expectedTypeName)
        {
            var expected = from pi in _context.TestValue.GetType().GetProperties()
                           where pi.PropertyType.Name == expectedTypeName
                           select pi;
            _typeInfo.PropertyInfos.SequenceEqual(expected).Should().BeTrue();
        }

        [Then(@"only the objects (.*) properties at (.*) are listed in the type info")]
        public void ThenOnlyTheObjectsPropertiesAtAreListedInTheTypeInfo(string expectedTypeName, string expectedPath)
        {
            var expected = from pi in _context.TestValue.GetType().GetProperties()
                           where pi.PropertyType.Name == expectedTypeName
                           select pi;
            _typeInfo.PropertyInfos.SequenceEqual(expected).Should().BeTrue();
        }

        [Then(@"only the objects (.*) fields are listed in the type info")]
        public void ThenOnlyTheObjectsFieldsAreListedInTheTypeInfo(string expectedTypeName)
        {
            var expected = from fi in _context.TestValue.GetType().GetFields()
                           where fi.FieldType.Name == expectedTypeName
                           select fi;
            _typeInfo.FieldInfos.SequenceEqual(expected).Should().BeTrue();
        }

        [Then(@"only the objects (.*) fields at (.*) are listed in the type info")]
        public void ThenOnlyTheObjectsFieldsAtAreListedInTheTypeInfo(string expectedTypeName, string expectedPath)
        {
            var expected = from fi in _context.TestValue.GetType().GetFields()
                           where fi.FieldType.Name == expectedTypeName
                           select fi;
            _typeInfo.FieldInfos.SequenceEqual(expected).Should().BeTrue();
        }


        private static IEnumerable<PropertyInfo> FilterOnType<T>(IEnumerable<PropertyInfo> source)
        {
            return source.Where(info => info.PropertyType == typeof(T));
        }

    }
}
