using Assignment2;
using System.Text;
using System;

Course course = new Course();

Address presentAddress = new Address
{
    Street = "184, Begum Rokeya Sarani",
    City = "Dhaka",
    Country = "Bangladesh"
};
Address permanantAddress = new Address
{
    Street = "184, Begum Rokeya Sarani",
    City = "Dhaka",
    Country = "Bangladesh"

};
Phone phone1 = new Phone()
{
    Number = "1794914570",
    Extension = "111",
    CountryCode = "880"
};
Phone phone2 = new Phone()
{
    Number = "1794914571",
    Extension = "111",
    CountryCode = "880"
};
List<Phone> phoneNumbers = new List<Phone> { phone1, phone2 };
Instructor teacher = new Instructor()
{
    Name = "Jalaluddin",
    Email = "jalaluddin@gmail.com",
    PresentAddress = presentAddress,
    PermanentAddress = permanantAddress,
    PhoneNumbers = phoneNumbers,
};

Session session1 = new Session()
{
    DurationInHour = 2,
    LearningObjective = "How Delegates works!"
};
Session session2 = new Session()
{
    DurationInHour = 2,
    LearningObjective = "How Delegates works!"
};

Session session3 = new Session()
{
    DurationInHour = 2,
    LearningObjective = "How Action, Func and Event works!"
};
List<Session> topic1Sessions = new List<Session>();
topic1Sessions.Add(session1);
topic1Sessions.Add(session2);

List<Session> topic2Sessions = new List<Session>();
topic2Sessions.Add(session3);

Topic topic1 = new Topic()
{
    Title = "Delegates",
    Description = "This is a demo text",
    Sessions = topic1Sessions,
};
Topic topic2 = new Topic()
{
    Title = "Delegates",
    Description = "This is a demo text",
    Sessions = topic2Sessions,
};

List<Topic> topics = new List<Topic>();
topics.Add(topic1);
topics.Add(topic2);

AdmissionTest admissionTest1 = new AdmissionTest()
{
    StartDateTime = DateTime.Parse("10:00AM"),
    EndDateTime = DateTime.Parse("01/01/2023 12:00AM"),
    TestFees = 500,
};

List<AdmissionTest> admissionTests = new List<AdmissionTest>();
admissionTests.Add(admissionTest1);


course.Title = "Asp.net";
course.Fees = 30000;
course.Teacher = teacher;
course.Topics = topics;
course.Tests = admissionTests;


Dictionary<string, int> dic = new Dictionary<string, int>();
dic.Add("one", 1);
dic.Add("two", 2);
dic.Add("three", 3);

List<int> list = new List<int>();
list.Add(1);
list.Add(2);
list.Add(3);
    
List<object> list2 = new List<object>();
list2.Add(list);
list2.Add(new Course());
list2.Add(new Course());

List<double> list3 = new List<double>();
list3.Add(1);
list3.Add(2);

Dictionary<int, object> dic3 = new Dictionary<int, object>();
dic3.Add(1, new Course());
dic3.Add(2, 2);
dic3.Add(3, new Instructor());

Dictionary<string, object> dic2 = new Dictionary<string, object>();
dic2.Add("one", new Course());
dic2.Add("two", 2);
dic2.Add("three", new Instructor());

char[] str = new char[] {'a', 'b', 'c' };



Console.WriteLine(XMLFormatter.Convert(course)); 