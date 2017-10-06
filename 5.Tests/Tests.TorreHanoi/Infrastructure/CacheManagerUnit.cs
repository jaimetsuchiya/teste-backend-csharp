using Infrastructure.TorreHanoi.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.TorreHanoi.Infrastructure
{
    [TestClass]
    public class CacheManagerUnit
    {
        private const string CategoriaTeste = "Infrastructure/CacheManager";

        private ICacheManager _cacheManager;

        [TestInitialize]
        public void SetUp()
        {
            _cacheManager = new CacheManager();
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void SetCache_Deve_Retornar_Sucesso()
        {
            _cacheManager.DataSource = "Teste";
            _cacheManager.Set("TesteUnitarioKey");

            Assert.IsNotNull(_cacheManager);
            Assert.IsNotNull(_cacheManager.DataSource);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void GetCache_Deverar_Retornar_Sucesso()
        {
            _cacheManager.DataSource = "Teste";
            _cacheManager.Set("TesteUnitarioKey");

            Assert.IsNotNull(_cacheManager);
            Assert.IsNotNull(_cacheManager.DataSource);

            var valorCache = _cacheManager.Get("TesteUnitarioKey");

            Assert.AreEqual(valorCache, "Teste");
        }
    }
}
