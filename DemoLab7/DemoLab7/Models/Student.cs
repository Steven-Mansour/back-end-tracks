using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DemoLab7.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
    
    [JsonIgnore]
    public virtual List<Class> Classes { get; set; } = new List<Class>();
}
