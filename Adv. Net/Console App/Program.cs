using System.Collections;
using Console_App;
//using StudentDemo;


List<Student> GetAllStudents()
{
    List<Student> students = new List<Student>();

    students.Add(new Student
    {
        RollNo = 1,
        Name = "Harshil",
        Age = 20,
        Branch = "CSE",
        Cpi = 9.8,
        Sem = 5
    });
    students.Add(new Student
    {
        RollNo = 2,
        Name = "Rajesh",
        Age = 18,
        Branch = "CSE",
        Cpi = 7.6,
        Sem = 4
    });
    students.Add(new Student
    {
        RollNo = 3,
        Name = "Mitesh",
        Age = 16,
        Branch = "CSE",
        Cpi = 5.8,
        Sem = 3
    });
    students.Add(new Student
    {
        RollNo = 4,
        Name = "Haril",
        Age = 20,
        Branch = "CSE",
        Cpi = 6.4,
        Sem = 7
    });
    students.Add(new Student
    {
        RollNo = 5,
        Name = "Meet",
        Age = 19,
        Branch = "CSE",
        Cpi = 7.2,
        Sem = 6
    });

    return students;
}

var students = GetAllStudents();

foreach (Student stu in students)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}

// 1. select * from Students  where cpi > 8

Console.WriteLine("Students with CPI greater than 8:");

var Greatherthan8 = students.Where(s => s.Cpi > 8).ToList();

foreach (Student stu in Greatherthan8)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}


Console.WriteLine();
Console.WriteLine("--------------------------------------------------");

// 2. get Students with Cpi less then 8

Console.WriteLine("Students with CPI less than 8:");

var lessthen8 = students.Where(s => s.Cpi < 8).ToList();

foreach(Student stu in lessthen8)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}

Console.WriteLine();
Console.WriteLine("--------------------------------------------------");

// 3. get students with cpi between 7 and 8

Console.WriteLine("Students with CPI between 7 and 8:");

var between7and8 = students.Where(s => s.Cpi >= 7 && s.Cpi <= 8).ToList();

foreach(Student stu in between7and8)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}

Console.WriteLine();
Console.WriteLine("--------------------------------------------------");

// 4. get students with greater then 8 and sem is 3

Console.WriteLine("Students with CPI greater than 8 and in semester 3:");

var greaterthan8andsem3 = students.Where(s => s.Cpi > 8 && s.Sem == 3).ToList();

foreach(Student stu in greaterthan8andsem3)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}


Console.WriteLine();
Console.WriteLine("--------------------------------------------------");
// 5. Get Studend where name Start with 'R'

Console.WriteLine("Students with names starting with 'R':");

var namestartwithr = students.Where(s => s.Name.StartsWith("R")).ToList();

foreach(Student stu in namestartwithr)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}

Console.WriteLine();
Console.WriteLine("--------------------------------------------------");


// 6. Get Students where name ends with 'h'

Console.WriteLine("Students with names ending with 'h':");

var nameendswithh = students.Where(s => s.Name.EndsWith("h")).ToList();

foreach(Student stu in nameendswithh)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}


Console.WriteLine();

Console.WriteLine("--------------------------------------------------");

// 7. get students where name contains 'e'

Console.WriteLine("Students with names containing 'e':");

var namecontainsE = students.Where(s => s.Name.Contains("e")).ToList();

foreach(Student stu in namecontainsE)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}

Console.WriteLine();
Console.WriteLine("--------------------------------------------------");



// 8. get students where name contains 'e' and Sem is 3

Console.WriteLine("Students with names containing 'e' and in semester 3:");

var namecontainsEnadsemis3 = students.Where(s => s.Name.Contains("e") && s.Sem == 3).ToList();

foreach(Student stu in namecontainsEnadsemis3)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}

Console.WriteLine();
Console.WriteLine("--------------------------------------------------");



// 9. get students whose name contains 'e' or Sem is 3 But only Display Rollno , name . branch , Cpi

Console.WriteLine("Students with names containing 'e' or in semester 3, displaying RollNo, Name, Branch, and CPI:");

var namecontainsEorsemis3 = students.Where(s => s.Name.Contains("e") || s.Sem == 3).Select(s => new { s.RollNo, s.Name, s.Branch, s.Cpi })
    .ToList();

foreach(var stu in namecontainsEorsemis3)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Branch: {stu.Branch}, CPI:{stu.Cpi}");
}

Console.WriteLine();
Console.WriteLine("--------------------------------------------------");



// 10. get Minimum Cpi from List of Students

Console.WriteLine("Minimum CPI from the list of students:");

var minCpi = students.Min(s => s.Cpi);

foreach(Student stu in students)
{
    if (stu.Cpi == minCpi)
    {
        Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
    }
}

Console.WriteLine();
Console.WriteLine("--------------------------------------------------");

// 11. get Maximum Cpi from List of Students

Console.WriteLine("Maximum CPI from the list of students:");

var maxCpi = students.Max(s => s.Cpi);
foreach(Student stu in students)
{
    if (stu.Cpi == maxCpi)
    {
        Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
    }
}
Console.WriteLine();
Console.WriteLine("--------------------------------------------------");

// 12. get first studentds with cpi greather than 8

Console.WriteLine("First student with CPI greater than 8:");

var firstStudentWithCpiGreaterThan8 = students.FirstOrDefault(s => s.Cpi > 8);

foreach(Student stu in students)
    {
    if (stu == firstStudentWithCpiGreaterThan8)
    {
        Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
    }
}
Console.WriteLine();
Console.WriteLine("--------------------------------------------------");

// 13. get last student with cpi less than 8

Console.WriteLine("Last student with CPI less than 8:");

var lastStudentWithCpiLessThan8 = students.LastOrDefault(s => s.Cpi < 8);
foreach(Student stu in students)
{
    if (stu == lastStudentWithCpiLessThan8)
    {
        Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
    }
}
Console.WriteLine();
Console.WriteLine("--------------------------------------------------");

// 14. group by branch and sem 

Console.WriteLine("Grouping students by Branch and Semester:");

var groupedByBranchAndSem = students.GroupBy(s => new { s.Branch, s.Sem });
foreach (var group in groupedByBranchAndSem)
{
    Console.WriteLine($"Branch: {group.Key.Branch}, Semester: {group.Key.Sem}");
    foreach (var stu in group)
    {
        Console.WriteLine($"  RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, CPI:{stu.Cpi}");
    }
}
Console.WriteLine();
Console.WriteLine("--------------------------------------------------");

// 15. take first 3 student from the list 

Console.WriteLine("Taking the first 3 students from the list:");
var firstThreeStudents = students.Take(3).ToList();
foreach (Student stu in firstThreeStudents)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}
Console.WriteLine();
Console.WriteLine("--------------------------------------------------");


// 16. skip first 2 students and take next 3 students

Console.WriteLine("Skipping the first 2 students and taking the next 3 students:");

var skippedFirstTwoAndNextThree = students.Skip(2).Take(3).ToList();

foreach (Student stu in skippedFirstTwoAndNextThree)
{
    Console.WriteLine($"RollNo: {stu.RollNo}, Name: {stu.Name}, Age: {stu.Age}, Branch: {stu.Branch}, CPI:{stu.Cpi}, Sem:{stu.Sem}");
}
Console.WriteLine();
Console.WriteLine("--------------------------------------------------");

// 17. 