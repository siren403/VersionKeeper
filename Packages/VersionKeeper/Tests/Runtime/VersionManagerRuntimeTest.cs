using System.Collections;
using NUnit.Framework;
using UnityEngine;
using VersionKeeper.Extensions;

namespace VersionKeeper.Tests
{
    public class VersionManagerRuntimeTest
    {
        private static string _versionStringFromSubsystemRegistration;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void SubsystemInitialize()
        {
            _versionStringFromSubsystemRegistration = VersionManager.GetVersionString();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test]
        public void SubsystemInitializedPasses()
        {
            Assert.IsTrue(_versionStringFromSubsystemRegistration.IsVersionString());
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _versionStringFromSubsystemRegistration = null;
        }
    }
}