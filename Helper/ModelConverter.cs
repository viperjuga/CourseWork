using CourseWork.Adapter;
using CourseWork.Models.Model;

namespace CourseWork.Helper {
    public static class ModelConverter {
        public static Task ToTask(this TaskAdapter adapter)
        {
            return new Task(){ Id = adapter.Id,
                Name = adapter.Name, Description = adapter.Description,
                StartTime = adapter.StartTime, EndTime = adapter.EndTime,
                Priority = adapter.Priority};
        }
    }
}
