using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAlerts.Transformers
{
    public class MemberPath : IEnumerable<string>
    {
        public static readonly char PathSeperator = '.';

        private readonly IEnumerable<string> _memberPath;
        public MemberPath(string memberPathString)
        {
            _memberPath = memberPathString.Split(PathSeperator);
        }
        
        public MemberPath(IEnumerable<string> memberPath)
        {
            _memberPath = memberPath;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _memberPath.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _memberPath.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(PathSeperator.ToString(), _memberPath);
        }
    }
}
