using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.TestesController
{
    public class MockHttpSession : ISession
    {
        readonly Dictionary<string, object> _sessionStorage = new Dictionary<string, object>();

        string ISession.Id => "sessionID";

        bool ISession.IsAvailable => true;

        IEnumerable<string> ISession.Keys => _sessionStorage.Keys;

        void ISession.Clear()
        {
            _sessionStorage.Clear();
        }
        Task ISession.CommitAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
        Task ISession.LoadAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
        void ISession.Remove(string key)
        {
            _sessionStorage.Remove(key);
        }
        void ISession.Set(string key, byte[] value)
        {
            _sessionStorage[key] = Encoding.UTF8.GetString(value);
        }
        //public string GetString(string key)
        //{
        //	return key = "123";
        //}
        bool ISession.TryGetValue(string key, out byte[] value)
        {
            value = Encoding.ASCII.GetBytes("123");
            return true;
        }
    }
}
