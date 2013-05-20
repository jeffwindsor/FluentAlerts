using System;
using System.Linq;
using FluentAlerts.Transformers.Formatters;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace FluentAlerts.Specs
{
    [Binding]
    public class StepsForObjectFormatting
    {
        private string _formatResponse;
        private BaseObjectFormatter<string> _formatter;
        private Func<object, string> MOTHER_FORMAT = (o) => Mother.GetFormat(o);

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
                    _context.TestValue = Mother.GetNestedTestClass(2);
                    break;
                case "NestedTestStruct":
                    _context.TestValue = Mother.GetNestedTestStruct(2);
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
            _formatter = new DefaultFormatter();
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
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "NestedTestStruct":
                    _formatter.FormatRules.InsertFirst((o, path) => o is NestedTestStruct,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "String":
                    _formatter.FormatRules.InsertFirst((o, path) => o is String,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "DateTime":
                    _formatter.FormatRules.InsertFirst((o, path) => o is DateTime,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Integer":
                    _formatter.FormatRules.InsertFirst((o, path) => o is int,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Long":
                    _formatter.FormatRules.InsertFirst((o, path) => o is long,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Float":
                    _formatter.FormatRules.InsertFirst((o, path) => o is float,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Double":
                    _formatter.FormatRules.InsertFirst((o, path) => o is double,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "NumberEnum":
                    _formatter.FormatRules.InsertFirst((o, path) => o is NumberEnum,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                default:
                    throw new ArgumentException("Type Not Known", "type");
            }
        }

        [Given(@"I add a format for the (.*)")]
        public void GivenIAddAFormatForTheType(string type)
        {
            switch (type)
            {
                case "Null":
                    //no rule required
                    break;
                case "NestedTestClass":
                    _formatter.FormatRules.Add((o, path) => o is NestedTestClass,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "NestedTestStruct":
                    _formatter.FormatRules.Add((o, path) => o is NestedTestStruct,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "String":
                    _formatter.FormatRules.Add((o, path) => o is String,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "DateTime":
                    _formatter.FormatRules.Add((o, path) => o is DateTime,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Integer":
                    _formatter.FormatRules.Add((o, path) => o is int,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Long":
                    _formatter.FormatRules.Add((o, path) => o is long,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Float":
                    _formatter.FormatRules.Add((o, path) => o is float,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Double":
                    _formatter.FormatRules.Add((o, path) => o is double,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "NumberEnum":
                    _formatter.FormatRules.Add((o, path) => o is NumberEnum,
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                default:
                    throw new ArgumentException("Type Not Known", "type");
            }
        }
        
        [Given(@"I specify a format for the (.*) at (.*)")]
        public void GivenISpecifyAFormatForTheTypeAtPath(string type, string targetPathString)
        {
            var targetPath = targetPathString.Split('.');
            switch (type)
            {
                case "Null":
                    //no rule required
                    break;
                case "NestedTestClass":
                    _formatter.FormatRules.InsertFirst((o, path) => o is NestedTestClass && path.SequenceEqual(targetPath),
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "NestedTestStruct":
                    _formatter.FormatRules.InsertFirst((o, path) => o is NestedTestStruct && path.SequenceEqual(targetPath),
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "String":
                    _formatter.FormatRules.InsertFirst((o, path) => o is String && path.SequenceEqual(targetPath),
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "DateTime":
                    _formatter.FormatRules.InsertFirst((o, path) => o is DateTime && path.SequenceEqual(targetPath),
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Integer":
                    _formatter.FormatRules.InsertFirst((o, path) => o is int && path.SequenceEqual(targetPath),
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Long":
                    _formatter.FormatRules.InsertFirst((o, path) => o is long && path.SequenceEqual(targetPath),
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Float":
                    _formatter.FormatRules.InsertFirst((o, path) => o is float && path.SequenceEqual(targetPath),
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "Double":
                    _formatter.FormatRules.InsertFirst((o, path) => o is double && path.SequenceEqual(targetPath),
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                case "NumberEnum":
                    _formatter.FormatRules.InsertFirst((o, path) => o is NumberEnum && path.SequenceEqual(targetPath),
                                                       (o, path) => MOTHER_FORMAT(o));
                    break;
                default:
                    throw new ArgumentException("Type Not Known", "type");
            }
        }

        
         
        [When(@"I format the object")]
        public void WhenIFormatTheObject()
        {
            _formatResponse = _formatter.Format(_context.TestValue, new string[] {});
        }

        [When(@"I format the object at (.*)")]
        public void WhenIFormatTheObjectAtPath(string pathString)
        {
            var path = pathString.Split('.');
            _formatResponse = _formatter.Format(_context.TestValue, path);
        }
        
        [When(@"I format the object as a title")]
        public void WhenIFormatTheObjectAsATitle()
        {
            _formatResponse = _formatter.FormatAsTitle(_context.TestValue, new string[] { });
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
                : _context.TestValue.GetType().ToString());
        }

        [Then(@"the result is equal to (.*) to format")]
        public void ThenTheResultIsEqualToToFormat(string type)
        {
            _formatResponse.Should().Be(type == "Null"
                                            ? "Null"
                                            : MOTHER_FORMAT(_context.TestValue));
        }
        
    }
}
