using ConfigurationLibrary.Factories;
using ConfigurationLibrary.Manager;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationUnitTests
{
    class ConfigReaderUnitTests
    {
        private ILayeredConfigurationFactory _factory;
        private IDictionary<string, string> _layer0Data;
        private IDictionary<string, string> _layer1Data;
        private IDictionary<string, string> _layer2Data;

        [SetUp]
        public void Setup()
        {
            _factory = new LayeredConfigurationFactory();
            _layer0Data = new Dictionary<string, string>();
            _layer1Data = new Dictionary<string, string>();
            _layer2Data = new Dictionary<string, string>();

            _layer0Data.Add("tKey1", "15");
            _layer0Data.Add("tKey2", "big");
            _layer0Data.Add("tKey3", "20.4");
            _layer0Data.Add("tKey4", "00:11:00");
            _layer0Data.Add("tKey5", "verylongstringoflettersevenlongerstill");

            _layer1Data.Add("tKey1", "3");
            _layer1Data.Add("tKey2", "small");
            _layer1Data.Add("tKey3", "20000000055000000005000000050000000000");
            _layer1Data.Add("tKey4", "1:11:13");
            _layer1Data.Add("tKey5", "short");

            _layer2Data.Add("tKey1", "8");
            _layer2Data.Add("tKey4", "01:00:00");
            _layer2Data.Add("tKey5", "Something");
            _layer2Data.Add("tKey6", "unknown");

            IConfigWriter configWriter = new ConfigManager(_factory);

            
            configWriter.AddConfigurationLayer(2, _layer2Data);
            configWriter.AddConfigurationLayer(1, _layer1Data);
            configWriter.AddConfigurationLayer(0, _layer0Data);
        }

        [Test]
        public void TestConfigReaderGetFullConfiguration()
        {
            IConfigReader configReader = new ConfigManager(_factory);

            var configuration = configReader.GetFullConfiguration();

            Assert.AreEqual(6, configuration.Count);
            Assert.AreEqual("small", configuration["tKey2"]);
        }

        [Test]
        public void TestConfigReaderGetConfiguration()
        {
            IConfigReader configReader = new ConfigManager(_factory);

            var configuration = configReader.GetConfiguration(new List<string> { "tKey1", "tKey3", "tKey7" });

            Assert.AreEqual(3, configuration.Count);

            Assert.AreEqual("8", configuration["tKey1"]);
            Assert.AreEqual("20000000055000000005000000050000000000", configuration["tKey3"]);
            Assert.AreEqual("Error", configuration["tKey7"]);
        }

        [Test]
        public void TestConfigReaderGetValueCorrect()
        {
            IConfigReader configReader = new ConfigManager(_factory);

            var configuration = configReader.GetValue<DateTime>("tKey4");

            Assert.AreEqual(true, configuration.Succeeded);

            Assert.AreEqual("01:00:00", configuration.Value.ToLongTimeString());

        }

        [Test]
        public void TestConfigReaderGetValueInCorrect()
        {
            IConfigReader configReader = new ConfigManager(_factory);

            var configuration = configReader.GetValue<int>("tKey5");

            Assert.AreEqual(false, configuration.Succeeded);

            Assert.AreEqual(1, configuration.Errors.Count);

        }
    }
}
