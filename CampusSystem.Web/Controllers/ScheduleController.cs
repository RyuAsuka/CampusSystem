using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CampusSystem.Web.Models;
using System.Web.Security;
using CampusSystem.Data.Models;
using CampusSystem.Data;

namespace CampusSystem.Web.Controllers
{
    [CampusAuthorize(Roles = "student, teacher")]
    public class ScheduleController : Controller
    {
        ICampusRepository repo = new CampusRepository(new CampusContext());

        // GET: Schedule
        public ActionResult Index()
        {
            GetRole();
            string userId = GetUserId();
            List<ScheduleModel> scheduleList = repo.MakeSchedule(userId);


            string[,] viewModel = new string[5, 11];

            #region 生成表格
            foreach (var s in scheduleList)
            {
                switch (s.Week)
                {
                    case Weekday.周一:
                        for (int i = 0; i < 11; i++)
                        {
                            if (s.Times.Contains((i + 1).ToString()))
                            {
                                viewModel[0, i] = s.Name;
                            }
                            else
                            {
                                viewModel[0, i] = null;
                            }
                        }
                        break;
                    case Weekday.周二:
                        for (int i = 0; i < 11; i++)
                        {
                            if (s.Times.Contains((i + 1).ToString()))
                            {
                                viewModel[1, i] = s.Name;
                            }
                            else
                            {
                                viewModel[1, i] = null;
                            }
                        }
                        break;
                    case Weekday.周三:
                        for (int i = 0; i < 11; i++)
                        {
                            if (s.Times.Contains((i + 1).ToString()))
                            {
                                viewModel[2, i] = s.Name;
                            }
                            else
                            {
                                viewModel[2, i] = null;
                            }
                        }
                        break;
                    case Weekday.周四:
                        for (int i = 0; i < 11; i++)
                        {
                            if (s.Times.Contains((i + 1).ToString()))
                            {
                                viewModel[3, i] = s.Name;
                            }
                            else
                            {
                                viewModel[3, i] = null;
                            }
                        }
                        break;
                    case Weekday.周五:
                        for (int i = 0; i < 11; i++)
                        {
                            if (s.Times.Contains((i + 1).ToString()))
                            {
                                viewModel[4, i] = s.Name;
                            }
                            else
                            {
                                viewModel[4, i] = null;
                            }
                        }
                        break;
                }
            }
            #endregion

            return View(viewModel);
        }

        /// <summary>
        /// 从Cookie中取得用户ID
        /// </summary>
        /// <returns>用户ID</returns>
        private string GetUserId()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value)) return null;
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket != null && !string.IsNullOrEmpty(ticket.Name))
                return ticket.Name;
            return null;
        }

        public ActionResult ShowCourseInfo()
        {
            string userId = GetUserId();
            List<Course> courseInfoList = repo.GetCourseInfo(userId);
            List<CourseViewModel> courseViewList = new List<CourseViewModel>();
            foreach (var i in courseInfoList)
            {
                courseViewList.Add(new CourseViewModel
                {
                    CourseId = i.course_id,
                    CourseName = i.name,
                    Creadits = i.credits,
                    Teacher = repo.GetUser(i.teacher_id).name,
                    ExamTime = i.exam_time.ToShortDateString(),
                    Place = i.place
                });
            }

            return PartialView("_showCourseInfo", courseViewList);
        }

        public ActionResult Score(string order)
        {
            GetRole();
            string userId = GetUserId();
            var scoreList = repo.GetScores(userId);

            List<ScoreViewModel> scores = new List<ScoreViewModel>();

            foreach (var i in scoreList)
            {
                scores.Add(new ScoreViewModel
                {
                    CourseName = i.Name,
                    Score = i.Score.ToString()
                });
            }

            switch (order)
            {
                case "name":
                    scores.Sort((ScoreViewModel a, ScoreViewModel b) =>
                    {
                        return a.CourseName.CompareTo(b.CourseName);
                    });
                    break;
                case "score":
                    scores.Sort((ScoreViewModel a, ScoreViewModel b) =>
                        {
                            if (a.Score != "" && b.Score != "")
                            {
                                if (int.Parse(a.Score) > int.Parse(b.Score)) return -1;
                                else if (int.Parse(a.Score) == int.Parse(b.Score))
                                    return a.CourseName.CompareTo(b.CourseName);
                                else return 1;
                            }
                            else if (a.Score == "" && b.Score != "")
                                return 1;
                            else if (a.Score != "" && b.Score == "")
                                return -1;
                            else return a.CourseName.CompareTo(b.CourseName);
                        });
                    break;
                case "null":
                default:
                    break;
            }

            return View(scores);
        }

        #region 教师成绩登记模块
        [CampusAuthorize(Roles = "teacher")]
        public ActionResult EditScores()
        {
            GetRole();
            var selectList = new List<SelectListItem>();
            var allCourses = repo.GetAllCourses();
            foreach (var c in allCourses)
            {
                var selectItem = new SelectListItem();
                selectItem.Value = selectItem.Text = c;
                selectList.Add(selectItem);
            }
            ViewBag.SelectList = selectList;
            return View();
        }

        [CampusAuthorize(Roles = "teacher")]
        public ActionResult showScores(string courseName)
        {
            GetRole();
            var studentsScores = repo.GetScoresOfAllStudentsInOneCourse(repo.GetCourseId(courseName));
            List<CourseScoreViewModel> scores = new List<CourseScoreViewModel>();
            foreach (var item in studentsScores)
            {
                scores.Add(new CourseScoreViewModel() { UserId = item.UserId, UserName = item.UserName, Score = item.Score == null ? "" : item.Score.ToString() });
            }
            ViewBag.CourseId = repo.GetCourseId(courseName);
            return View(scores);
        }

        [HttpPost]
        [CampusAuthorize(Roles = "teacher")]
        public ActionResult SubmitScores(string courseId, FormCollection collection)
        {
            GetRole();
            string str = collection["score"];
            string[] scores = str.Split(',');
            repo.ChangeScores(courseId, scores);
            //TODO: 增加发送消息逻辑
            return RedirectToAction("EditScores");
        }
        #endregion

        public void GetRole()
        {
            string cuId = GetUserId();
            var currentUser = repo.GetUser(cuId);
            ViewBag.UserRole = currentUser.role;
        }
    }

}