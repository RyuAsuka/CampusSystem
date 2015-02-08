using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampusSystem.Data.Models;

namespace CampusSystem.Data
{
    /// <summary>
    /// 数据访问接口
    /// </summary>
    public interface ICampusRepository
    {
        User GetUser(string userId);
        List<string> GetAllStudents();
        bool VerifyUser(string userId, string password);

        List<Shuttle> SearchShuttle(string weekday, string startLocation, string endLocation);

        List<Lend> GetBooksLent(string userId);
        string GetBookNameByCopyId(string copyId);
        void ContinueLend(int id);

        List<ScheduleModel> MakeSchedule(string userId);

        List<Course> GetCourseInfo(string userId);
        List<ScoresOfUsers> GetScores(string userId);
        void ChangeScores(string courseId, string[] scores);
        List<string> GetAllCourses();

        string GetCourseId(string courseName);

        List<ScoreOfCourses> GetScoresOfAllStudentsInOneCourse(string courseId);

        List<Shuttle> GetAllShuttleInfo();

        void CreateShuttle(string time, string start, string end, string weekday);
        Shuttle GetShuttleInfo(int id);
        void EditShuttle(int id, string time, string start, string end, string weekday);
        void DeleteShuttle(int id);

        List<BookLendingModel> GetLibraryInfo(string userId);

        Book SearchCopy(string copyId);

        void ReturnBook(string id);

        void LendBook(string copyId, string userId);

        List<User> GetFriendList(string userId);

        void SendMessage(string content, string sender, DateTime sendTime, List<string> targets);

        List<Message> ReceiveMessage(string userId);
    }
}
