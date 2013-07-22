using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.Transformers
{
    public class MemberPath //: IEnumerable<string>
    {
        private const char Seperator = '.';
        public static readonly MemberPath Empty = new MemberPath(Enumerable.Empty<string>());
        
        private readonly string[] _memberPath;

        public MemberPath(string memberPath): this(memberPath.Split(Seperator)){}
        public MemberPath(IEnumerable<string> memberPath){ _memberPath =memberPath.ToArray();}

        public int Length {
            get { return _memberPath.Length; }
        }

        public MemberPath Extend(string extension)
        {
            return new MemberPath(_memberPath.Concat(new[] {extension}));
        }

        public override string ToString()
        {
            return string.Join(Seperator.ToString(), _memberPath);
        }
    }
}
