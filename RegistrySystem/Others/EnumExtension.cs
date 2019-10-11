using System;
using System.ComponentModel;
using System.Reflection;

namespace RegistrySystem
{
    public static class EnumExtension
    {
        public static string ToStringEnums(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
    }

    public enum Customer
    {
        [Description("Физ. лицо")]
        Individual = 0,
        [Description("Юр. лицо")]
        Entity = 1,
    }

    public enum EducationForm
    {
        [Description("ПО")]
        PO = 0,
        [Description("ПО(157)")]
        PO157 = 1,
        [Description("ДПО")]
        DPO = 2,
    }

    public enum ProgramType
    {
        [Description("ПП")]
        PP = 0,
        [Description("ПК")]
        PK = 1,
    }

    public enum Sex
    {
        [Description("Муж.")]
        Male = 0,
        [Description("Жен.")]
        Female = 1,
    }

    public enum Education
    {
        [Description("Среднее общее")]
        SecondaryGeneral = 0,
        [Description("Среднее специальное")]
        SecondarySpecial = 1,
        [Description("Высшее бакалавр")]
        HigherUndergraduate = 2,
        [Description("Высшее специалист")]
        HigherSpecialty = 3,
        [Description("Высшее магистр")]
        HigherMagistracy = 4,
    }
}
