using System;
using CourseWork.Models.Enum;

namespace CourseWork.Models.Model {
    public class Task {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime EndTime { get; set; } = DateTime.MaxValue;
        public Priority Priority { get; set; } = Priority.Minor;
    }
}
