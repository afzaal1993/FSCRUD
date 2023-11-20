using Common;
using DotNetCore.CAP;

namespace Events
{
    public class EventReceiver : ICapSubscribe
    {
        [CapSubscribe("Events.AddStudentImage")]
        public void AddStudentImage(SaveStudentImage model)
        {
            string a = "I am triggered";
            string b = a;
        }
    }
}
