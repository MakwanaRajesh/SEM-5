using System;
using System.Collections.Generic;

namespace APIDemo1.Models;

public partial class Student
{
    public int Id { get; set; }

    public string StudentName { get; set; } = null!;

    public string? StudentGender { get; set; }

    public int? Age { get; set; }

    public int? Standard { get; set; }

    public string FatherName { get; set; } = null!;
}
