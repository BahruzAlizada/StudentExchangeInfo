

namespace StudentExchangeInfo.Application.ViewModels.Student
{
    public class StudentOtherInformationVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UniName { get; set; }
        public bool? IsBacheolor { get; set; } = true;
        public double? UOMG { get; set; }
    }
}
