using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAlerts.Transformers;
using FluentAlerts.Transformers.Formatters;
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
        private object _transformedObject;
        private IAlert _transformedAlert;

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
                {
                    //only for objects of type
                    if (obj.GetType().Name == typeString)
                    {
                        //Current path + property name = the target path
                        info.PropertyInfos = from pi in info.PropertyInfos
                                             where path.Concat(new[] {pi.Name})
                                                       .SequenceEqual(_memberPath)
                                             select pi;

                    }
                });
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
        
        [Given(@"I have a default transformer")]
        public void GivenIHaveADefaultTransformer()
        {
            _context.Transformer = Factory.Transformers.Create();
        }
        
        [Given(@"I have a name type value pair transformer")]
        public void GivenIHaveANameTypeValuePairTransformer()
        {
            _context.Transformer = Factory.Transformers.CreateNameTypeValue();
        }


        [When(@"I get the object's type info")]
        public void WhenIGetTheObjectSTypeInfo()
        {
            _typeInfo = _selector.Find(_context.TestValue, _memberPath);
        }

        [When(@"I transform the alert")]
        public void WhenITransformTheAlert()
        {
            _transformedAlert = _context.Alert.Transform();
        }

        [When(@"I transform the alert using a custom transformer")]
        public void WhenITransformTheAlertUsingACustomTransformer()
        {
            _context.Alert = _context.Alert.Transform(Factory.Transformers.Create());
        }
        
        [When(@"I transform the object")]
        public void WhenITransformTheObject()
        {
            _transformedObject = _context.Transformer.Transform(_context.TestValue);
        }



        [Then(@"the result should be an IAlert")]
        public void ThenTheResultShouldBeAnIAlert()
        {
            _transformedObject.Should().BeAssignableTo<IAlert>();
            _transformedAlert=_transformedObject  as IAlert;
        }

        [Then(@"the alert title equals the object's type name")]
        public void ThenTheAlertTitleEqualsTheObjectSTypeName()
        {
            _transformedAlert.Title.Should().Be(_context.TestValue.GetType().Name);
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

        //[Then(@"only the objects (.*) properties at (.*) are listed in the type info")]
        //public void ThenOnlyTheObjectsPropertiesAtAreListedInTheTypeInfo(string expectedTypeName, string expectedPath)
        //{
        //    var expected = from pi in _context.TestValue.GetType().GetProperties()
        //                   where pi.PropertyType.Name == expectedTypeName
        //                   select pi;
        //    _typeInfo.PropertyInfos.Should().BeEquivalentTo(expected);
        //}

        [Then(@"only the objects (.*) fields are listed in the type info")]
        public void ThenOnlyTheObjectsFieldsAreListedInTheTypeInfo(string expectedTypeName)
        {
            var expected = from fi in _context.TestValue.GetType().GetFields()
                           where fi.FieldType.Name == expectedTypeName
                           select fi;
            _typeInfo.FieldInfos.Should().BeEquivalentTo(expected);
        }

        //[Then(@"only the objects (.*) fields at (.*) are listed in the type info")]
        //public void ThenOnlyTheObjectsFieldsAtAreListedInTheTypeInfo(string expectedTypeName, string expectedPath)
        //{
        //    var expected = from fi in _context.TestValue.GetType().GetFields()
        //                   where fi.FieldType.Name == expectedTypeName
        //                   select fi;
        //    _typeInfo.FieldInfos.Should().BeEquivalentTo(expected);
        //}

        [Then(@"the alert has a group for each of objects properties with name value pairs")]
        public void ThenTheAlertHasAGroupForEachOfObjectsPropertiesWithNameValuePairs()
        {
            var expected = from pi in _context.TestValue.GetType().GetProperties()
                           select new {Name = pi.Name, Value = pi.GetValue(_context.TestValue, null)};

            var actual = from a in _transformedAlert
                         where a is AlertGroup
                         select a as AlertGroup;

            var merge = from e in expected
                        join a in actual on e.Name equals a.Values[0].ToString()
                        select new { e.Name, Expected = e.Value, Actual = a.Values[1] };


            actual.Count(a => a.Values.Length == 2).Should().Be(actual.Count(), "each property should have a name value pair group");
            expected.Count().Should().Be(merge.Count(), "properties groups");
            merge.Count(r => r.Expected.ToString() == r.Actual.ToString()).Should().Be(merge.Count(), "each property transformed value should be its to string");
        }

        [Then(@"the alert has a group for each of objects fields with name value pairs")]
        public void ThenTheAlertHasAGroupForEachOfObjectsFieldsWithNameValuePairs()
        {
            var expected = from pi in _context.TestValue.GetType().GetFields()
                           select new { Name = pi.Name, Value = pi.GetValue(_context.TestValue) };

            var actual = from a in _transformedAlert
                         where a is AlertGroup
                         select a as AlertGroup;

            var merge = from e in expected
                        join a in actual on e.Name equals a.Values[0].ToString()
                        select new { e.Name, Expected = e.Value, Actual = a.Values[1] };

            actual.Count(a => a.Values.Length == 2).Should().Be(actual.Count(), "each field should have a name value pair group");
            expected.Count().Should().Be(merge.Count(), "field groups");
            merge.Count(r => r.Expected.ToString() == r.Actual.ToString()).Should().Be(merge.Count(), "each field transformed value should be its to string");
    
        }

        [Then(@"all the (.*) are the same")]
        public void ThenAllTheAreTheSame(string type)
        {
            switch (type)
            {
                case "IAlert":
                    _context.Alert.Should().BeEquivalentTo(_transformedAlert);
                    break;
                case "IAlertItem":
                    _context.Alert.AllItems().ShouldBeEquivalentTo(_transformedAlert.AllItems());
                    break;
                case "Result":
                    var expecteds = from i in _context.Alert.AllValues()
                                   where i is string
                                   select i as string;
                    var actuals = from i in _transformedAlert.AllValues()
                                 where i is string
                                 select i as string;
                    var merge = from e in expecteds
                                join a in actuals on e equals a
                                select a;
                    merge.Count().Should().Be(expecteds.Count());
                    break;
                default :
                    throw new ArgumentException("Type unknown","type");
            }
        }
        
        [Then(@"the alert has a group for each of objects properties with name type value pairs")]
        public void ThenTheAlertHasAGroupForEachOfObjectsPropertiesWithNameTypeValuePairs()
        {
            var expected = from pi in _context.TestValue.GetType().GetProperties()
                           select new {pi, Name = pi.Name, Value = pi.GetValue(_context.TestValue, null) };

            var actual = from a in _transformedAlert
                         where a is AlertGroup
                         select a as AlertGroup;

            var merge = from e in expected
                        join a in actual on e.Name equals a.Values[0].ToString()
                        select new
                            {
                                e.Name,
                                Expected = e.Value,
                                Actual = a.Values[2],
                                ExpectedType = DefaultToStringFormatter.PrettyTypeName(e.pi.PropertyType),
                                ActualType = a.Values[1].ToString()
                            };


            actual.Count(a => a.Values.Length == 3).Should().Be(actual.Count(), "each property should have a name value pair group");
            expected.Count().Should().Be(merge.Count(), "properties groups");
            merge.Count(r => r.Expected.ToString() == r.Actual.ToString()).Should().Be(merge.Count(), "each property transformed value should be its to string");
            merge.Count(r => r.ExpectedType == r.ActualType).Should().Be(merge.Count(), "each property transformed type should be its to string");
        }

        [Then(@"the alert has a group for each of objects fields with name type value pairs")]
        public void ThenTheAlertHasAGroupForEachOfObjectsFieldsWithNameTypeValuePairs()
        {
            var expected = from pi in _context.TestValue.GetType().GetFields()
                           select new {pi, Name = pi.Name, Value = pi.GetValue(_context.TestValue) };

            var actual = from a in _transformedAlert
                         where a is AlertGroup
                         select a as AlertGroup;

            var merge = from e in expected
                        join a in actual on e.Name equals a.Values[0].ToString()
                        select new
                            {
                                e.Name,
                                Expected = e.Value,
                                Actual = a.Values[2],
                                ExpectedType = DefaultToStringFormatter.PrettyTypeName(e.pi.FieldType),
                                ActualType = a.Values[1].ToString()
                            };

            actual.Count(a => a.Values.Length == 3).Should().Be(actual.Count(), "each field should have a name value pair group");
            expected.Count().Should().Be(merge.Count(), "field groups");
            merge.Count(r => r.Expected.ToString() == r.Actual.ToString()).Should().Be(merge.Count(), "each field transformed value should be its to string");
            merge.Count(r => r.ExpectedType == r.ActualType).Should().Be(merge.Count(), "each field transformed type should be its to string");
        }


        private static IEnumerable<PropertyInfo> FilterOnType<T>(IEnumerable<PropertyInfo> source)
        {
            return  source.Where(info => info.PropertyType == typeof(T));
        }

    }
}
