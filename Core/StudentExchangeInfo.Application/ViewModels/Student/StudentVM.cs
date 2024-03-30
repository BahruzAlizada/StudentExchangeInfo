using StudentExchangeInfo.Domain.Entities;

namespace StudentExchangeInfo.Application.ViewModels.Student
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public int? UniId { get; set; }
        public string UniverName { get; set; }
        public string Uniİmage { get; set; }
    }
}
