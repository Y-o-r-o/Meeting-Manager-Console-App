using Application.Core;
using Application.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringLibraryTest;

[TestClass]
public class MeetingTest
{
    // [TestMethod]
    // public void TestSetGoodNames()
    // {
    //     string[] names = { "PhMeet", "Djkovic", "DjangoRewiew", "CleanCode", "SpeechTraining2" };
    //     foreach (var name in names)
    //     {
    //         Meeting meeting = new Meeting();

    //         Result result = meeting.setName(name);

    //         Assert.IsTrue(result.IsSuccess);
    //         Assert.IsTrue(meeting.getName().Equals(name));
    //     }
    // }

    // [TestMethod]
    // public void TestSetBadNamesLenght()
    // {
    //     string[] names = { "Yo", "MeetingOfKingsUnderTheTable", "O", "lololololololololololol", "5" };
    //     foreach (var name in names)
    //     {
    //         Meeting meeting = new Meeting();

    //         Result result = meeting.setName(name);

    //         Assert.IsTrue(result.Error.Contains("Name lenght should be between"));
    //     }
    // }

    // [TestMethod]
    // public void TestSetNamesWithIlleagalCharracters()
    // {
    //     string[] names = { "@PhMeet", "Djkovic;;", ".DjangoRewiew", "Clean&Code", "SpeechTraining�" };
    //     foreach (var name in names)
    //     {
    //         Meeting meeting = new Meeting();

    //         Result result = meeting.setName(name);

    //         Assert.IsTrue(result.Error.Equals("Name should have only letters or numbers."));
    //     }
    // }

    // [TestMethod]
    // public void TestSetGoodCategories()
    // {
    //     string[] categories = { "CodeMonkey", "CODEMONKEY", "CoDeMONkey", "Hub", "HUB" };
    //     foreach (var category in categories)
    //     {
    //         Meeting meeting = new Meeting();

    //         Result result = meeting.setCategory(category);

    //         Assert.IsTrue(result.IsSuccess);
    //         Assert.IsTrue(meeting.Category.ToString().ToUpper().Equals(category.ToUpper()));
    //     }
    // }

    // [TestMethod]
    // public void TestSetBadCategories()
    // {
    //     string[] categories = { "Code", "CODE", "any", "123", "???" };
    //     foreach (var category in categories)
    //     {
    //         Meeting meeting = new Meeting();

    //         Result result = meeting.setCategory(category);

    //         Assert.IsTrue(!result.IsSuccess);
    //     }
    // }




}