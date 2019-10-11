using System;
using System.Collections.Generic;

namespace RegistrySystem
{
    public class Group
    {
        public int Id { get; set; }
        public string Num { get; set; } = string.Empty;
        public string OrderInNum { get; set; } = string.Empty;
        public DateTime DateIn { get; set; } = new DateTime(2019, 1, 1);
        public string OrderOutNum { get; set; } = string.Empty;
        public DateTime DateOut { get; set; } = new DateTime(2019, 1, 1);
        public DateTime DateStart { get; set; } = new DateTime(2019, 1, 1);
        public DateTime DateEnd { get; set; } = new DateTime(2019, 1, 1);
        public int ProgramHours { get; set; } = 0;
        public List<Contract> Contracts { get; set; } = new List<Contract>();
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
