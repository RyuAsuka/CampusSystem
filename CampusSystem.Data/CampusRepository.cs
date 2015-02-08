using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampusSystem.Data.Models;

namespace CampusSystem.Data
{
    public class CampusRepository : ICampusRepository
    {
        readonly CampusContext dbcontext;

        public CampusRepository(CampusContext context)
        {
            dbcontext = context;
        }

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="userId">用户账号</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public bool VerifyUser(string userId, string password)
        {
            var user = GetUser(userId);
            if (user == null)
            {
                //log.Warn("用户" + userId + "不存在");
                return false;
            }
            if (password != user.password)
            {
                //log.Warn("用户" + userId + "试图登录失败，密码不正确");
                return false;
            }
            //log.Info("用户" + userId + "登录成功");
            return true;
        }

        /// <summary>
        /// 根据用户账号查找用户
        /// </summary>
        /// <param name="userId">用户账号</param>
        /// <returns>查找到的用户实体</returns>
        public User GetUser(string userId)
        {
            return dbcontext.Users.Find(userId);
        }

        /// <summary>
        /// 校车查询
        /// </summary>
        /// <param name="weekday">星期</param>
        /// <param name="startLocation">起点</param>
        /// <param name="endLocation">终点</param>
        /// <returns>包含所有满足条件的校车班次列表</returns>
        public List<Shuttle> SearchShuttle(string weekday, string startLocation, string endLocation)
        {
            List<Shuttle> ls = new List<Shuttle>();
            var shuttles =
                from s in dbcontext.Shuttles
                where s.start_location == startLocation &&
                      s.end_location == endLocation &&
                      s.weekday.Contains(weekday)
                select s;
            foreach (var item in shuttles)
            {
                ls.Add(item);
            }
            return ls;
        }

        /// <summary>
        /// 图书借阅查询
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>该用户所有借阅图书的列表</returns>
        public List<Lend> GetBooksLent(string userId)
        {
            List<Lend> result = new List<Lend>();
            var lentList = from b in dbcontext.Lends where b.user_id == userId select b;
            foreach (var item in lentList)
            {
                result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 根据条形码查询书籍名称
        /// </summary>
        /// <param name="copyId">条形码</param>
        /// <returns>图书名称</returns>
        public string GetBookNameByCopyId(string copyId)
        {
            return dbcontext.Copies.Find(copyId).Book.name;
        }

        /// <summary>
        /// 续借指定id的借阅
        /// </summary>
        /// <param name="id">lend_id</param>
        public void ContinueLend(int id)
        {
            using (var t = dbcontext.Database.BeginTransaction())
            {
                try
                {
                    dbcontext.Lends.Find(id).expire_time += new TimeSpan(14, 0, 0, 0);
                    dbcontext.SaveChanges();
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                }

            }

        }

        /// <summary>
        /// 根据用户id生成包含课程信息的列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns>包含该用户所有课程信息的列表</returns>
        public List<ScheduleModel> MakeSchedule(string userId)
        {
            string course_id = string.Empty;
            string name = string.Empty;
            string allTime = string.Empty;

            //ScheduleModel schedule = new ScheduleModel();
            List<ScheduleModel> scheduleList = new List<ScheduleModel>();

            var mySchedule = from ms in dbcontext.Schedules where ms.student_id == userId select ms.course_id;
            foreach (var courseId in mySchedule)
            {
                var courseInfo = from ci in dbcontext.Courses where ci.course_id == courseId select new { ci.course_id, ci.name, ci.time, ci.place };
                foreach (var c in courseInfo)
                {

                    course_id = c.course_id;
                    name = c.name;
                    allTime = c.time;
                    string[] oneTimes = allTime.Split(';');
                    foreach (string oneTime in oneTimes)
                    {
                        string[] weekAndTime = oneTime.Split(',');
                        string week = weekAndTime[0];
                        string[] times = weekAndTime[1].Split('.');

                        ScheduleModel schedule = new ScheduleModel();
                        schedule.CourseId = course_id;
                        schedule.Name = name;
                        schedule.Place = c.place;
                        schedule.Times = times;
                        switch (week)
                        {
                            case "周一":
                                schedule.Week = Weekday.周一;
                                break;
                            case "周二":
                                schedule.Week = Weekday.周二;
                                break;
                            case "周三":
                                schedule.Week = Weekday.周三;
                                break;
                            case "周四":
                                schedule.Week = Weekday.周四;
                                break;
                            case "周五":
                                schedule.Week = Weekday.周五;
                                break;
                            default:
                                break;
                        }
                        scheduleList.Add(schedule);
                    }
                }
            }

            scheduleList.Sort((ScheduleModel modelA, ScheduleModel modelB) =>
                {
                    if (modelA.Week == modelB.Week)
                        return modelA.Times[0].CompareTo(modelB.Times[0]);
                    else
                        return modelA.Week.CompareTo(modelB.Week);
                });

            return scheduleList;
        }

        /// <summary>
        /// 获取课程详细信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns>包含课程详细信息的列表</returns>
        public List<Course> GetCourseInfo(string userId)
        {
            List<Course> retVal = new List<Course>();
            var schedule = from s in dbcontext.Schedules where s.student_id == userId select s.course_id;
            foreach (var cid in schedule)
            {
                var courseList = from c in dbcontext.Courses where c.course_id == cid select c;
                foreach (var item in courseList)
                {
                    retVal.Add(item);
                }
            }

            return retVal;
        }

        /// <summary>
        /// 根据用户id查询他所有课程的成绩
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>包含该用户所有课程成绩的列表</returns>
        public List<ScoresOfUsers> GetScores(string userId)
        {
            List<ScoresOfUsers> retVal = new List<ScoresOfUsers>();
            var list = from s in dbcontext.Schedules where s.student_id == userId select new { s.Course.name, s.score };
            foreach (var item in list)
            {
                retVal.Add(new ScoresOfUsers { Name = item.name, Score = item.score });
            }
            return retVal;
        }

        /// <summary>
        /// 获得所有学生名单
        /// </summary>
        /// <returns>返回所有role为student的用户名的列表</returns>
        public List<string> GetAllStudents()
        {
            return dbcontext.Users.Where(s => s.role == "student").Select(s => s.user_id).ToList();
        }

        /// <summary>
        /// 修改分数
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="scores">分数</param>
        public void ChangeScores(string courseId, string[] scores)
        {
            using (var t = dbcontext.Database.BeginTransaction())
            {
                var list = dbcontext.Schedules.Where(m => m.course_id == courseId);
                int i = 0;
                foreach (var item in list)
                {
                    item.score = int.Parse(scores[i]);
                    i++;
                }

                try
                {
                    dbcontext.SaveChanges();
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                }
            }

        }

        /// <summary>
        /// 获得所有课程的列表
        /// </summary>
        /// <returns>包含所有课程名称的列表</returns>
        public List<string> GetAllCourses()
        {
            return dbcontext.Courses.Select(m => m.name).ToList();
        }

        /// <summary>
        /// 根据课程名称查询课程Id
        /// </summary>
        /// <param name="courseName">课程名称</param>
        /// <returns>课程ID</returns>
        public string GetCourseId(string courseName)
        {
            return dbcontext.Courses.FirstOrDefault(m => m.name == courseName).course_id;
        }

        /// <summary>
        /// 返回选择某课程的所有学生的成绩列表
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>包含选择该课程的所有学生的成绩列表</returns>
        public List<ScoreOfCourses> GetScoresOfAllStudentsInOneCourse(string courseId)
        {
            var query = dbcontext.Schedules.Where(m => m.course_id == courseId).Select(m => new { m.student_id, m.User.name, m.score });
            List<ScoreOfCourses> retval = new List<ScoreOfCourses>();
            foreach (var q in query)
            {
                retval.Add(new ScoreOfCourses() { UserId = q.student_id, UserName = q.name, Score = q.score });
            }
            return retval;
        }

        /// <summary>
        /// 返回所有校车信息
        /// </summary>
        /// <returns>包含所有校车信息的列表</returns>
        public List<Shuttle> GetAllShuttleInfo()
        {
            return dbcontext.Shuttles.ToList();
        }

        /// <summary>
        /// 添加校车信息
        /// </summary>
        /// <param name="time">表示时间的字符串</param>
        /// <param name="start">起点</param>
        /// <param name="end">终点</param>
        /// <param name="weekday">星期</param>
        public void CreateShuttle(string time, string start, string end, string weekday)
        {
            using (var t = dbcontext.Database.BeginTransaction())
            {
                //解析时间
                string[] tt = time.Split(':');
                TimeSpan ts = new TimeSpan(int.Parse(tt[0]), int.Parse(tt[1]), int.Parse(tt[2]));

                dbcontext.Shuttles.Add(new Shuttle() { time = ts, start_location = start, end_location = end, weekday = weekday });
                try
                {
                    dbcontext.SaveChanges();
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                }
            }
        }

        /// <summary>
        /// 根据id查找校车信息
        /// </summary>
        /// <param name="id">校车班次ID</param>
        /// <returns>校车信息</returns>
        public Shuttle GetShuttleInfo(int id)
        {
            return dbcontext.Shuttles.Find(id);
        }

        /// <summary>
        /// 编辑校车信息
        /// </summary>
        /// <param name="id">校车班次ID</param>
        /// <param name="time">时间</param>
        /// <param name="start">起点</param>
        /// <param name="end">终点</param>
        /// <param name="weekday">星期</param>
        public void EditShuttle(int id, string time, string start, string end, string weekday)
        {
            using (var t = dbcontext.Database.BeginTransaction())
            {
                //解析时间
                string[] tt = time.Split(':');
                TimeSpan ts = new TimeSpan(int.Parse(tt[0]), int.Parse(tt[1]), int.Parse(tt[2]));

                Shuttle itemToEdit = dbcontext.Shuttles.Find(id);
                itemToEdit.time = ts;
                itemToEdit.start_location = start;
                itemToEdit.end_location = end;
                itemToEdit.weekday = weekday;
                try
                {
                    dbcontext.SaveChanges();
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                }
            }
        }

        /// <summary>
        /// 删除校车信息
        /// </summary>
        /// <param name="id">校车班次ID</param>
        public void DeleteShuttle(int id)
        {
            using (var t = dbcontext.Database.BeginTransaction())
            {
                Shuttle itemToDelete = dbcontext.Shuttles.Find(id);
                dbcontext.Shuttles.Remove(itemToDelete);
                try
                {
                    dbcontext.SaveChanges();
                    t.Commit();
                }
                catch (Exception ex)
                {
                    t.Rollback();
                }
            }

        }

        /// <summary>
        /// 获取用户图书借阅信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>包含用户图书借阅信息的列表</returns>
        public List<BookLendingModel> GetLibraryInfo(string userId)
        {
            var search = from l in dbcontext.Lends
                         where l.user_id == userId
                         select new
                         {
                             l.lend_id,
                             l.copy_id,
                             l.Copy.isbn,
                             l.Copy.Book.name,
                             l.Copy.Book.book_index,
                             l.lend_time,
                             l.expire_time,
                             l.return_time,
                             l.Copy.status
                         };
            List<BookLendingModel> retVal = new List<BookLendingModel>();
            foreach (var item in search)
            {
                retVal.Add(new BookLendingModel()
                {
                    Id = item.lend_id,
                    CopyId = item.copy_id,
                    ISBN = item.isbn,
                    BookName = item.name,
                    Index = item.book_index,
                    LendTime = item.lend_time,
                    ExpireTime = item.expire_time,
                    ReturnTime = item.return_time,
                    Status = item.status,
                    UserId = userId
                });
            }

            return retVal;
        }

        /// <summary>
        /// 查询书目
        /// </summary>
        /// <param name="copyId">书目副本ID</param>
        /// <returns>书籍信息</returns>
        public Book SearchCopy(string copyId)
        {
            if (dbcontext.Copies.Find(copyId).status == true)
                return null;
            return dbcontext.Copies.Find(copyId).Book;
        }

        /// <summary>
        /// 还书
        /// </summary>
        /// <param name="id">借阅ID</param>
        public void ReturnBook(string id)
        {
            using (var t = dbcontext.Database.BeginTransaction())
            {
                var lend = dbcontext.Lends.Find(int.Parse(id));
                if (lend.return_time != null) return;
                lend.return_time = DateTime.Now;
                lend.Copy.status = false;
                try
                {
                    dbcontext.SaveChanges();
                    t.Commit();
                }
                catch (Exception ex)
                {
                    t.Rollback();
                }
            }

        }

        /// <summary>
        /// 借书
        /// </summary>
        /// <param name="copyId">副本ID</param>
        /// <param name="userId">用户ID</param>
        public void LendBook(string copyId, string userId)
        {
            using (var t = dbcontext.Database.BeginTransaction())
            {
                Lend newLend = new Lend()
                {
                    user_id = userId,
                    copy_id = copyId,
                    lend_time = DateTime.Now,
                    expire_time = DateTime.Now + new TimeSpan(14, 0, 0, 0),
                    return_time = null
                };
                dbcontext.Lends.Add(newLend);
                dbcontext.Copies.Find(copyId).status = true;
                try
                {
                    dbcontext.SaveChanges();
                    t.Commit();
                }
                catch(Exception ex)
                {
                    t.Rollback();
                }
            }
        }

        /// <summary>
        /// 获取用户全部好友列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户的所有好友列表</returns>
        public List<User> GetFriendList(string userId)
        {
            List<User> friends = new List<User>();
            var user = dbcontext.Users.Find(userId);
            var fl = from fs in dbcontext.Users where fs.class_id == user.class_id select fs;
            foreach (var item in fl)
            {
                if (item.user_id != userId)
                    friends.Add(item);
            }
            return friends;
        }

        /// <summary>
        /// 发送消息逻辑
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="sender">发送者</param>
        /// <param name="sendTime">发送时间</param>
        /// <param name="targets">发送目标</param>
        public void SendMessage(string content, string sender, DateTime sendTime, List<string> targets)
        {
            using (var t = dbcontext.Database.BeginTransaction())
            {
                int m_id = 0;
                Message m = new Message()
                {
                    sender = sender,
                    send_time = sendTime,
                    message_content = content
                };
                dbcontext.Messages.Add(m);
                try
                {
                    dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    t.Rollback();
                }

                m_id = (int)m.message_id;
                try
                {
                    foreach (string target in targets)
                    {
                        dbcontext.SendToes.Add(new SendTo()
                        {
                            message_id = m_id,
                            receiver = target,
                            is_sent = false,
                        });
                    }
                    dbcontext.SaveChanges();
                    t.Commit();
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    t.Rollback();
                }
            }
        }

        /// <summary>
        /// 接收消息方法
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>包含收件人是该用户的所有消息的列表</returns>
        public List<Message> ReceiveMessage(string userId)
        {
            List<Message> retVal = new List<Message>();
            var query = dbcontext.Messages.Join(
                dbcontext.SendToes,
                m => m.message_id,
                s => s.message_id,
                (m, s) => new
                {
                    s.message_id,
                    s.receiver,
                    s.is_sent,
                    m.sender,
                    m.send_time,
                    m.message_content
                }).Where(m => m.receiver == userId).Select(m => m).OrderByDescending(m => m.send_time);

            foreach (var item in query)
            {
                retVal.Add(new Message()
                {
                    message_id = item.message_id,
                    message_content = item.message_content,
                    sender = item.sender,
                    send_time = item.send_time
                });

                dbcontext.SendToes.Find(item.message_id, userId).is_sent = true;

            }

            return retVal;
        }
    }
}
