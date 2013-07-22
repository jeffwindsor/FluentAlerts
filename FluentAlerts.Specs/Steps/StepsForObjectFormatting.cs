using System;
using System.Linq;
using FluentAlerts.Formatters;
using FluentAlerts.Transformers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace FluentAlerts.Specs
{
    [Binding]
    public class StepsForObjectFormatting
    {
        private string _formatResponse;
        private BaseValueFormatter<string> _formatter;
        private Func<object, string> SPEC_FORMAT = (o) => GetFormat(o);
        private Func<object, string> ALT_SPEC_FORMAT = (o) => GetAltFormat(o);

        private AlertContext _context;
        public StepsForObjectFormatting(AlertContext context)
        {
            _context = context;
        }
         
        [Given(@"I have a (.*) object")]
        public void GivenIHaveAObject(string type)
        {
            switch (type)
            {
                case "Null":
                    _context.TestValue = null;
                    break;
                case "NestedTestClass":
                    _context.TestValue = ObjectFactory.CreateNestedTestClass(2);
                    break;
                case "NestedTestStruct":
                    _context.TestValue = ObjectFactory.CreateNestedTestStruct(2);
                    break;
                case "String":
                    _context.TestValue = _context.TestText;
                    break;
                case "DateTime":
                    _context.TestValue = DateTime.Now;
                    break;
                case "Integer":
                    _context.TestValue = int.MaxValue;
                    break;
                case "Long":
                    _context.TestValue = long.MaxValue;
                    break;
                case "Float":
                    _context.TestValue = float.MinValue;
                    break;
                case "Double":
                    _context.TestValue = double.MaxValue;
                    break;
                case "NumberEnum":
                    _context.TestValue = NumberEnum.Seven;
                    break;
                default:
                    throw new ArgumentException("Type Not Known","type");
            }
        }

        [Given(@"I have a formatter")]
        [Given(@"I have the default formatter")]
        public void GivenIHaveTheDefaultFormatter()
        {
            _formatter = new DefaultValueToStringFormatter();
        }

        [Given(@"I have an empty formatter")]
        public void GivenIHaveAnEmptyformatter()
        {
            _formatter = new DefaultValueToStringFormatter();
            _formatter.FormatAsTitleRules.Clear();
            _formatter.FormatRules.Clear();
        }

        [Given(@"I insert a format for the (.*) at the beginning")]
        public void GivenIInsertAFormatForTheNullAtTheBeginning(string type)
        {
            switch (type)
            {
                case "Null":
                    //no rule required
                    break;
                case "NestedTestClass":
                    _formatter.FormatRules.InsertFirst((o, path) => o is NestedTestClass,
                                                       (o, path) => SPEC_FORMAT(o));
                    break;
                case "NestedTestStruct":
                    _formatter.FormatRules.InsertFirst((o, path) => o is NestedTestStruct,
                                                       (o, path) => SPEC_FORMAT(o));
                    break;
                case "String":
                    _formatter.FormatRules.InsertFirst((o, path) => o is String,
                                                       (o, path) => SPEC_FORMAT(o));
                    break;
                case "DateTime":
                    _formatter.FormatRules.InsertFirst((o, path) => o is DateTime,
                                                       (o, path) => SPEC_FORMAT(o));
                    break;
                case "Integer":
                    _formatter.FormatRules.InsertFirst((o, path) => o is int,
                                                       (o, path) => SPEC_FORMAT(o));
                    break;
                case "Long":
                    _formatter.FormatRules.InsertFirst((o, path) => o is long,
                                                       (o, path) => SPEC_FORMAT(o));
                    break;
                case "Float":
                    _formatter.FormatRules.InsertFirst((o, path) => o is float,
                                                       (o, path) => SPEC_FORMAT(o));
                    break;
                case "Double":
                    _formatter.FormatRules.InsertFirst((o, path) => o is double,
                                                       (o, path) => SPEC_FORMAT(o));
                    break;
                case "NumberEnum":
                    _formatter.FormatRules.InsertFirst((o, path) => o is NumberEnum,
                                                       (o, path) => SPEC_FORMAT(o));
                    break;
                default:
                    throw new ArgumentException("Type Not Known", "type");
            }
        }

        [Given(@"I add a format for the (.*)")]
        public void GivenIAddAFormatForTheType(string type)
        {
            //Specifing all rules to catch any cross rule issues
            _formatter.FormatRules.Add((o, path) => o is NestedTestClass,
                                       (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.Add((o, path) => o is NestedTestStruct,
                                       (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.Add((o, path) => o is String,
                                       (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.Add((o, path) => o is DateTime,
                                       (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.Add((o, path) => o is int,
                                       (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.Add((o, path) => o is long,
                                       (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.Add((o, path) => o is float,
                                       (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.Add((o, path) => o is double,
                                       (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.Add((o, path) => o is NumberEnum,
                                       (o, path) => SPEC_FORMAT(o));
        }

        [Given(@"I specify a format for the (.*) at (.*)")]
        public void GivenISpecifyAFormatForTheTypeAtPath(string type, string targetPathString)
        {
            //Specifing all rules to catch any cross rule issues 
            var targetPath = targetPathString.Split('.');
            _formatter.FormatRules.InsertFirst((o, path) => o is NestedTestClass && path.SequenceEqual(targetPath),
                                               (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.InsertFirst((o, path) => o is NestedTestStruct && path.SequenceEqual(targetPath),
                                               (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.InsertFirst((o, path) => o is String && path.SequenceEqual(targetPath),
                                               (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.InsertFirst((o, path) => o is DateTime && path.SequenceEqual(targetPath),
                                               (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.InsertFirst((o, path) => o is int && path.SequenceEqual(targetPath),
                                               (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.InsertFirst((o, path) => o is long && path.SequenceEqual(targetPath),
                                               (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.InsertFirst((o, path) => o is float && path.SequenceEqual(targetPath),
                                               (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.InsertFirst((o, path) => o is double && path.SequenceEqual(targetPath),
                                               (o, path) => SPEC_FORMAT(o));
            _formatter.FormatRules.InsertFirst((o, path) => o is NumberEnum && path.SequenceEqual(targetPath),
                                               (o, path) => SPEC_FORMAT(o));

        }
        
        [Given(@"I insert a specific format for the (.*) at the beginning")]
        public void GivenIInsertASpecificFormatForTheTypeAtTheBeginning(string type)
        {
            _formatter.FormatRules.InsertFirst((o, path) => true,
                                               (o, path) => SPEC_FORMAT(o));
        }

        [Given(@"I insert a different format for (.*) in between")]
        public void GivenIInsertADifferentFormatForTheTypeInBetween(string type)
        {
            _formatter.FormatRules.Insert(1,
                                          (o, path) => true,
                                          (o, path) => ALT_SPEC_FORMAT(o));
        }
        
        

        [When(@"I format the object")]
        public void WhenIFormatTheObject()
        {
            try
            {
                _formatResponse = ((IValueFormatter<string>)_formatter).Format(_context.TestValue, MemberPath.Empty);
            }
            catch (Exception ex)
            {
                _context.CaughtException = ex;
            }
            
        }

        [When(@"I format the object at (.*)")]
        public void WhenIFormatTheObjectAtPath(string pathString)
        {
            try
            {
                var path = new MemberPath(pathString);
                _formatResponse = _formatter.Format(_context.TestValue, path);
            }
            catch (Exception ex)
            {
                _context.CaughtException = ex;
            }
        }
        
        [When(@"I format the object as a title")]
        public void WhenIFormatTheObjectAsATitle()
        {
            try
            {
                _formatResponse = ((IValueFormatter<string>) _formatter).FormatAsTitle(_context.TestValue,
                                                                                        MemberPath.Empty);
            } 
            catch (Exception ex)
            {
                _context.CaughtException = ex;
            }
        }



        [Then(@"the result is equal to (.*) to string")]
        public void ThenTheResultIsEqualToObjectToString(string type)
        {
             _formatResponse.Should().Be(type == "Null" 
                ? "Null"  
                : _context.TestValue.ToString());
        }
        
        [Then(@"the result is equal to (.*) types name")]
        public void ThenTheResultIsEqualToTypesName(string type)
        {
            _formatResponse.Should().Be(type == "Null"
                ? "Null"
                : _context.TestValue.GetType().Name);
        }

        [Then(@"the result is equal to (.*) to format")]
        public void ThenTheResultIsEqualToToFormat(string type)
        {
            _formatResponse.Should().Be(type == "Null"
                                            ? "Null"
                                            : SPEC_FORMAT(_context.TestValue));
        }

        [Then(@"I expect a FluentAlertFormattingException to be thrown")]
        public void ThenIExpectAFluentAlertFormattingExceptionToBeThrown()
        {
            _context.CaughtException.Should().BeOfType<FluentAlertFormattingException<string>>();

            var ex = _context.CaughtException as FluentAlertFormattingException<string>;
            ex.FormatObject.Should().Be(_context.TestValue);
            ex.FormatObjectMemberPath.Should().BeEquivalentTo(MemberPath.Empty);
        }


        private static string GetFormat(object o)
        {
            return string.Format("Extra {0} Extra", o);
        }

        private static string GetAltFormat(object o)
        {
            return string.Format("Alt {0} Alt", o);
        }

    }
}
