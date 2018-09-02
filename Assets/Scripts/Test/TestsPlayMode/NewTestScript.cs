﻿using System.Collections;
using Main.Domain.Players;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Test.TestsPlayMode
{
    public class NewTestScript {

        [Test]
        public void NewTestScriptSimplePasses() {
            // Use the Assert class to test conditions.
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses() {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }
}
