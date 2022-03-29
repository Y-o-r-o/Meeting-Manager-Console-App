using System;
using System.Collections.Generic;
using Application;
using Application.Core;
using Application.Extensions;
using Application.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringLibraryTest;

[TestClass]
public class MeetingListExtensionsTest
{
    private List<Meeting> meetings;

    public MeetingListExtensionsTest()
    {
        prepareMeetings();
    }

    public void prepareMeetings()
    {
        var serializer = new Serializer<List<Meeting>>("testMeetings");
        var result = serializer.deserialize();
        if (result.IsSuccess)
        {
            meetings = result.Value;
        }
        else throw new Exception(result.Error);
    }

    [TestMethod]
    public void TestGetMeetingByName()
    {
        string[] names = { "Coolas", "Coolas2", "KingOfTheTable3" };
        foreach(string name in names){
            var meeting = meetings.GetMeetingByName(name);
            
            Assert.IsTrue(meeting.IsSuccess);
            Assert.AreEqual(meeting.Value.getName(), name);
        }
    }



}