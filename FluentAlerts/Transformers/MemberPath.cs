using System.Collections.Generic;

namespace FluentAlerts.Transformers
{
    public class MemberPath : IEnumerable<string>
    {
        private readonly IEnumerable<string> _memberPath;
        private readonly char _pathSeperator;
        
        public MemberPath(string memberPathString, IFluentAlertSettings fluentAlertSettings)
        {
            _pathSeperator = fluentAlertSettings.MemberPathSeperator();
            _memberPath = memberPathString.Split(_pathSeperator);
        }

        public MemberPath(IEnumerable<string> memberPath, IFluentAlertSettings fluentAlertSettings)
        {
            _pathSeperator = fluentAlertSettings.MemberPathSeperator();
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
            return string.Join(_pathSeperator.ToString(), _memberPath);
        }
    }
}
