using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Transactions;
using Xunit;

namespace Chatter.Test.API
{
    public abstract class BaseIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        internal readonly WebApplicationFactory<Startup> _factory; 
        private TransactionScope _transactionScope;
        public BaseIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }
        public virtual void Dispose()
        {
            _transactionScope.Dispose();
        }
    }
}
