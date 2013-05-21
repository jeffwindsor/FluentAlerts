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
        private readonly AlertContext _context;
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
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.PropertyInfos = info.PropertyInfos.Where(pi => pi.PropertyType == typeof (NestedTestClass)));
                    break;
                case "NestedTestStruct":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.PropertyInfos =
                        info.PropertyInfos.Where(pi => pi.PropertyType == typeof (NestedTestStruct)));
                    break;
                case "String":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.PropertyInfos = info.PropertyInfos.Where(pi => pi.PropertyType == typeof (string)));
                    break;
                case "DateTime":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.PropertyInfos = info.PropertyInfos.Where(pi => pi.PropertyType == typeof (DateTime)));
                    break;
                case "Integer":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.PropertyInfos = info.PropertyInfos.Where(pi => pi.PropertyType == typeof (int)));
                    break;
                case "Long":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.PropertyInfos = info.PropertyInfos.Where(pi => pi.PropertyType == typeof (long)));
                    break;
                case "Float":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.PropertyInfos = info.PropertyInfos.Where(pi => pi.PropertyType == typeof (float)));
                    break;
                case "Double":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.PropertyInfos = info.PropertyInfos.Where(pi => pi.PropertyType == typeof (double)));
                    break;
                case "NumberEnum":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.PropertyInfos = info.PropertyInfos.Where(pi => pi.PropertyType == typeof (NumberEnum)));
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
            _selector.Rules.Add((info, obj, path) => 
                info.PropertyInfos = info.PropertyInfos.Where(pi =>
                    path.Concat(new[] { pi.Name }).SequenceEqual(_memberPath)));
        }

        [Given(@"I limit the informer to (.*) fields")]
        public void GivenILimitTheInformerToFields(string typeString)
        {
            switch (typeString)
            {
                case "Null":
                    //no rule required
                    break;
                case "NestedTestClass":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.FieldInfos = info.FieldInfos.Where(pi => pi.FieldType == typeof(NestedTestClass)));
                    break;
                case "NestedTestStruct":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.FieldInfos =
                        info.FieldInfos.Where(pi => pi.FieldType == typeof(NestedTestStruct)));
                    break;
                case "String":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.FieldInfos = info.FieldInfos.Where(pi => pi.FieldType == typeof(string)));
                    break;
                case "DateTime":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.FieldInfos = info.FieldInfos.Where(pi => pi.FieldType == typeof(DateTime)));
                    break;
                case "Integer":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.FieldInfos = info.FieldInfos.Where(pi => pi.FieldType == typeof(int)));
                    break;
                case "Long":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.FieldInfos = info.FieldInfos.Where(pi => pi.FieldType == typeof(long)));
                    break;
                case "Float":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.FieldInfos = info.FieldInfos.Where(pi => pi.FieldType == typeof(float)));
                    break;
                case "Double":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.FieldInfos = info.FieldInfos.Where(pi => pi.FieldType == typeof(double)));
                    break;
                case "NumberEnum":
                    _selector.Rules.Add(
                        (info, obj, path) =>
                        info.FieldInfos = info.FieldInfos.Where(pi => pi.FieldType == typeof(NumberEnum)));
                    break;
                default:
                    throw new ArgumentException("Type Not Known", "type");
            }
        }

        [Given(@"I limit the informer to (.*) fields at (.*)")]
        public void GivenILimitTheInformerToFieldssAt(string typeString, string targetPathString)
        {
            _memberPath = new MemberPath(targetPathString);
            //current path plus property name = target path
            _selector.Rules.Add((info, obj, path) =>
                info.FieldInfos = info.FieldInfos.Where(pi =>
                    path.Concat(new[] { pi.Name }).SequenceEqual(_memberPath)));
        }


        [When(@"I get the object's type info")]
        public void WhenIGetTheObjectSTypeInfo()
        {
            _typeInfo = _selector.Find(_context.TestValue, _memberPath);
        }

        [When(@"I transform the alert")]
        public void WhenITransformTheAlert()
        {
            _context.Alert = _context.Alert.Transform();
        }

        [When(@"I transform the alert using a custom transformer")]
        public void WhenITransformTheAlertUsingACustomTransformer()
        {
            _context.Alert = _context.Alert.Transform(Alerts.Transformers.CreateDefault());
        }


        [Then(@"there should not be any element that is not a grpah class or result type")]
        public void ThenThereShouldNotBeAnyElementThatIsNotAGrpahClassOrResultType()
        {
            var nonTransformedValues = from value in _context.Alert.AllValues()
                          where !(value is string)
                          select value;
            nonTransformedValues.Should().HaveCount(0);
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
            _typeInfo.PropertyInfos.Should().BeEquivalentTo(expected);
        }

        [Then(@"only the objects (.*) properties at (.*) are listed in the type info")]
        public void ThenOnlyTheObjectsPropertiesAtAreListedInTheTypeInfo(string expectedTypeName, string expectedPath)
        {
            var expected = from pi in _context.TestValue.GetType().GetProperties()
                           where pi.PropertyType.Name == expectedTypeName
                           select pi;
            _typeInfo.PropertyInfos.Should().BeEquivalentTo(expected);
        }

        [Then(@"only the objects (.*) fields are listed in the type info")]
        public void ThenOnlyTheObjectsFieldsAreListedInTheTypeInfo(string expectedTypeName)
        {
            var expected = from fi in _context.TestValue.GetType().GetFields()
                           where fi.FieldType.Name == expectedTypeName
                           select fi;
            _typeInfo.FieldInfos.Should().BeEquivalentTo(expected);
        }

        [Then(@"only the objects (.*) fields at (.*) are listed in the type info")]
        public void ThenOnlyTheObjectsFieldsAtAreListedInTheTypeInfo(string expectedTypeName, string expectedPath)
        {
            var expected = from fi in _context.TestValue.GetType().GetFields()
                           where fi.FieldType.Name == expectedTypeName
                           select fi;
            _typeInfo.FieldInfos.Should().BeEquivalentTo(expected);
        }


        private static IEnumerable<PropertyInfo> FilterOnType<T>(IEnumerable<PropertyInfo> source)
        {
            return  source.Where(info => info.PropertyType == typeof(T));
        }

    }
}
