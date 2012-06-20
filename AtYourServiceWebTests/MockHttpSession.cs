
namespace AtYourService.Web.Tests
{
    using System.Collections.Generic;
    using System.Web;

    public class MockHttpSession : HttpSessionStateBase
    {
        readonly Dictionary<string, object> _sessionStorage = new Dictionary<string, object>();

        public override object this[string name]
        {
            get { return _sessionStorage[name]; }
            set { _sessionStorage[name] = value; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the number of items in the session-state collection.
        /// </summary>
        /// <returns>
        /// The number of items in the collection.
        /// </returns>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override int Count
        {
            get { return _sessionStorage.Count; }
        }
    }
}
