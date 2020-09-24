using ConfigurationLibrary.Factories;
using ConfigurationLibrary.Manager;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ConfigurationUnitTests
{
    public class ConfigWriterUnitTests
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
        }

        [Test]
        public void TestConfigWriter()
        {
            IConfigWriter configWriter = new ConfigManager(_factory);

            configWriter.AddConfigurationLayer(0, _layer0Data);
            configWriter.AddConfigurationLayer(1, _layer1Data);
            configWriter.AddConfigurationLayer(2, _layer2Data);

                        
            Assert.Pass();
        }
    }
}