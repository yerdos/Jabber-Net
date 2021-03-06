/* --------------------------------------------------------------------------
 * Copyrights
 *
 * Portions created by or assigned to Cursive Systems, Inc. are
 * Copyright (c) 2002-2008 Cursive Systems, Inc.  All Rights Reserved.  Contact
 * information for Cursive Systems, Inc. is available at
 * http://www.cursive.net/.
 *
 * License
 *
 * Jabber-Net is licensed under the LGPL.
 * See licenses/Jabber-Net_LGPLv3.txt for details.
 * --------------------------------------------------------------------------*/

using System.Xml;
using JabberNet.jabber;
using JabberNet.jabber.client;
using JabberNet.jabber.protocol.client;
using JabberNet.jabber.protocol.iq;
using NUnit.Framework;

namespace JabberNet.Test.jabber.client
{
    /// <summary>
    /// Summary description for PPDP.
    /// </summary>
    [TestFixture]
    public class RosterManagerTest
    {
        XmlDocument doc = new XmlDocument();

        [Test] public void Test_Create()
        {
            RosterManager rm = new RosterManager();
            Assert.AreEqual("JabberNet.jabber.client.RosterManager", rm.GetType().FullName);
        }
        public void TestAdd()
        {
            RosterManager rm = new RosterManager();

            RosterIQ riq = new RosterIQ(doc);
            riq.Type = IQType.set;
            Roster r = riq.Instruction;
            Item i = r.AddItem();
            i.JID = new JID("foo", "bar", null);
            i.Nickname = "FOO";
            i.Subscription = Subscription.both;

            rm.AddRoster(riq);
            Assert.AreEqual(Subscription.both, rm["foo@bar"].Subscription);
            Assert.AreEqual("FOO", rm["foo@bar"].Nickname);

            riq = new RosterIQ(doc);
            riq.Type = IQType.set;
            r = riq.Instruction;
            i = r.AddItem();
            i.JID = new JID("foo", "bar", null);
            i.Nickname = "BAR";
            i.Subscription = Subscription.to;
            rm.AddRoster(riq);
            Assert.AreEqual(Subscription.to, rm["foo@bar"].Subscription);
            Assert.AreEqual("BAR", rm["foo@bar"].Nickname);
        }
        public void TestNumeric()
        {
            RosterManager rm = new RosterManager();

            RosterIQ riq = new RosterIQ(doc);
            riq.Type = IQType.set;
            Roster r = riq.Instruction;
            Item i = r.AddItem();
            i.JID = new JID("support", "conference.192.168.32.109", null);
            i.Nickname = "FOO";
            i.Subscription = Subscription.both;

            rm.AddRoster(riq);
            Assert.AreEqual(Subscription.both, rm["support@conference.192.168.32.109"].Subscription);
            Assert.AreEqual("FOO", rm["support@conference.192.168.32.109"].Nickname);
        }
    }
}
