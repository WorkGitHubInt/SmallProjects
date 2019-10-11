using System;

namespace RegistrySystem
{
    public class Student
    {
        public string Name { get; set; } = string.Empty;
        public Sex Sex { get; set; } = Sex.Male;
        public DateTime BirthDate { get; set; } = new DateTime(2019, 1, 1);
        public Education Education { get; set; } = Education.SecondaryGeneral;
        public virtual Group Group { get; set; }
    }
}
