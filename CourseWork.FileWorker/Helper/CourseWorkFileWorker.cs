using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CourseWork.Models.Enum;
using CourseWork.Models.Model;

namespace CourseWork.FileWorker.Helper {
    public class CourseWorkFileWorker {
        private static readonly string FILEPATH = "course_work_tasks.csv";
        public static List<Task> LoadFromFile() {
            var result = new List<Task>();
            if (!File.Exists(FILEPATH)) {
                return result;
            }
            var lines = File.ReadAllLines(FILEPATH, Encoding.UTF8);
            if (lines.Length < 2) return result;
            for (var i = 1; i < lines.Length; i++) {
                var parsedLine = lines[i].Split(new[] { "," }, StringSplitOptions.None).ToList();
                parsedLine.ForEach(e => e = e.Trim());
                if (parsedLine.Count < 6) {
                    throw new ArgumentException($"Wrong line format in saved file. Line {i}");
                }
                try {
                    result.Add(new Task() {
                        Id = int.Parse(parsedLine[0]),
                        Name = parsedLine[1],
                        Description = parsedLine[2],
                        StartTime = DateTime.ParseExact(parsedLine[3], "ddMMyyyyHHmm", CultureInfo.CurrentCulture),
                        EndTime = DateTime.ParseExact(parsedLine[4], "ddMMyyyyHHmm", CultureInfo.CurrentCulture),
                        Priority = (Priority)Enum.Parse(typeof(Priority), parsedLine[5])
                    });
                } catch (Exception ex) {
                    throw new ArgumentException($"Wrong format of data in saved file. Line {i}", ex);
                }
            }
            return result;
        }

        public static void SaveToFile(List<Task> tasks) {
            var data = string.Empty;
            if (tasks.Any()) {
                data += "Id,Name,Description,StartDate,EndDate,Priority\r\n";
            }
            foreach (var task in tasks) {
                data += $"{task.Id},{task.Name},{task.Description},{task.StartTime:ddMMyyyyHHmm},{task.EndTime:ddMMyyyyHHmm},{task.Priority}";
                if (tasks.Last() != task) {
                    data += "\r\n";
                }
            }
            File.WriteAllText(FILEPATH, data);
        }

        public static void GenerateHtmlDoc(string path, List<Task> tasks)
        {
            var data = string.Join("", tasks.Select(e => $"<tr><td><input type=\"checkbox\"></td><td>{e.StartTime:dd/MM/yyyy HH:mm}</td><td>{e.EndTime:dd/MM/yyyy HH:mm}</td><td>{e.Priority}</td><td>{e.Name}</td> <td>{e.Description}</td></tr>").SelectMany(e => e));
            var body = $"<table style=\"width:100 %\" cellspacing=\"2\" border=\"1\" cellpadding=\"5\"><tr><th>Status</th><th>StartTime</th><th>EndTime</th><th>Priority</th><th>Name</th><th>Description</th></tr><tr>{data}</tr></table>";
            var doc =
                $"<!DOCTYPE HTML><html><head><title>TODO List</title><meta charset=\"utf-8\"></head><body><h1 align=\"center\">TODO list on {DateTime.Now:dd/MM/yyyy HH:mm}</h1>{body}</body></html>";
            File.WriteAllText(path, doc);
        }

    }
}
