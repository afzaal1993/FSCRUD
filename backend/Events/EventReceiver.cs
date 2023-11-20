using Common;
using DotNetCore.CAP;
using System.Drawing;

namespace Events
{
    public class EventReceiver : ICapSubscribe
    {
        [CapSubscribe("Events.AddStudentImage")]
        public void AddStudentImage(SaveStudentImage model)
        {
            try
            {
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

                if(!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, model.FileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileStream.Write(model.ImageData, 0, model.ImageData.Length);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
