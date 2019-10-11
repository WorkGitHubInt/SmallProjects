using System;

namespace RegistrySystem
{
    public class Contract
    {
        public int Id { get; set; }
        public string Num { get; set; } = string.Empty;
        public DateTime Date { get; set; } = new DateTime(2019, 1, 1);
        public Customer Customer { get; set; } = Customer.Individual;
        public string EntityName { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public EducationForm EducationForm { get; set; } = EducationForm.PO;
        public string ProgramName { get; set; } = string.Empty;
        public ProgramType ProgramType { get; set; } = ProgramType.PK;
        public string PlaneType { get; set; } = string.Empty;
    }
}
