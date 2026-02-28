// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using Domain.Models;
using Infrastructure.Service;
using Infrastructure.Interfaces;


IStudent StudentService = new StudentService();
ICourse CourseService = new CourseService();
IMentor MentorService = new MentorService();
IGroup GroupService = new GroupService();
IStudentGroup StudentGroupService = new StudentGroupService();

while (true)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║     МАҒОЗАИ ИНТЕРНЕТӢ (CRUD)         ║");
    Console.WriteLine("╠══════════════════════════════════════╣");
    Console.WriteLine("║  1. Идоракунии донишҷӯён (Students)  ║");
    Console.WriteLine("║  2. Идоракунии курсҳо (Courses)      ║");
    Console.WriteLine("║  3. Идоракунии мураббиён (Mentors)   ║");
    Console.WriteLine("║  4. Идоракунии гурӯҳҳо (Groups)      ║");
    Console.WriteLine("║  5. Идоракунии тақсимоти донишҷӯён   ║");
    Console.WriteLine("║  6. AnalyticsService                 ║");
    Console.WriteLine("║  0. Баромад                          ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.Write("\nБахшро интихоб кунед: ");

    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1": StudentsMenu(); break;
            case "2": CourseMenu(); break;
            case "3": MentorsMenu(); break;
            case "4": GroupsMenu(); break;
            case "5": StudentGroupsMenu(); break;
            case "6": AnalyticsMenu(); break;
            case "0": return;
            default: Console.WriteLine("Интихоби нодуруст!"); break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nХатогӣ: {ex.Message}");
    }

    Console.WriteLine("\nБарои идома додан тугмаи дилхоҳро пахш кунед...");
    Console.ReadKey();

    void StudentsMenu()
    {
        Console.WriteLine("=== ИСТИФОДАБАРАНДАГОН ===");
        Console.WriteLine("1. Илова кардан");
        Console.WriteLine("2. Намоиши рӯйхати донишҷӯён");
        Console.WriteLine("3. Ёфтан аз рӯи ID");
        Console.WriteLine("4.Таҳрир");
        Console.WriteLine("5. - Ҳазф (танҳо агар дар гурӯҳ набошад)");
        Console.Write("Интихоб: ");

        switch (Console.ReadLine())
        {
            case "1":
                var Student = new Student();
                Console.Write("Ному насаб: "); Student.FullName = Console.ReadLine()!;
                Console.Write("Email: "); Student.Email = Console.ReadLine()!;
                Console.Write("Рақами телефон: "); Student.Phone = Console.ReadLine()!;
                Console.Write("EnrollmentDate: "); Student.EnrollmentDate = DateOnly.Parse(Console.ReadLine()!);
                StudentService.AddStudent(Student);
                break;

            case "2":
                var Students = StudentService.GetAllStudent();
                foreach (var u in Students)
                    Console.WriteLine($"{u.StudentId}: {u.FullName} | {u.Email} | {u.Phone}");
                break;

            case "3":
                Console.Write("ID: ");
                var found = StudentService.GetStudentById(Convert.ToInt32(Console.ReadLine()!));
                if (found != null)
                    Console.WriteLine($"{found.StudentId}: {found.FullName}\n{found.Email}\n{found.Phone}\n{found.EnrollmentDate}");
                else
                    Console.WriteLine("Ёфт нашуд!");
                break;

            case "4":
                var upd = new Student();
                Console.Write("ID: "); upd.StudentId = Convert.ToInt32(Console.ReadLine()!);
                Console.Write("Ному насаб: "); upd.FullName = Console.ReadLine()!;
                Console.Write("Email: "); upd.Email = Console.ReadLine()!;
                Console.Write("Рақами телефон: "); upd.Phone = Console.ReadLine()!;
                Console.Write("EnrollmentDate: "); upd.EnrollmentDate = DateOnly.Parse(Console.ReadLine()!);
                StudentService.UpdateStudent(upd);
                break;

            case "5":
                Console.Write("ID барои нест кардан: ");
                StudentService.DeleteStudent(Convert.ToInt32(Console.ReadLine()!));
                break;
        }
    }

    void CourseMenu()
    {
        Console.WriteLine("=== КАТЕГОРИЯҲО ===");
        Console.WriteLine("1. Илова кардан");
        Console.WriteLine("2. Намоиши ҳама");
        Console.WriteLine("3. Ёфтан аз рӯи ID");
        Console.WriteLine("4. Навсозӣ кардан");
        Console.WriteLine("5. Нест кардан");
        Console.Write("Интихоб: ");

        switch (Console.ReadLine())
        {
            case "1":
                var cat = new Course();
                Console.Write("номи курс: "); cat.Title = Console.ReadLine()!;
                Console.Write("тавсифи курс: "); cat.Description = Console.ReadLine()!;
                Console.Write("давомнокии курс (ҳафта): ");
                cat.DurationWeeks = Convert.ToInt32(Console.ReadLine()!);
                CourseService.AddCourse(cat);
                break;

            case "2":
                var cats = CourseService.GetAllCourse();
                foreach (var ce in cats)
                    Console.WriteLine($"{ce.CourseId}: {ce.Title} | тавсифи курс: {ce.Description} | давомнокии курс (ҳафта): {ce.DurationWeeks}");
                break;

            case "3":
                Console.Write("ID: ");
                var c = CourseService.GetCourseById(Convert.ToInt32(Console.ReadLine()!));
                if (c != null)
                    Console.WriteLine($"{c.CourseId}: {c.Title} | тавсифи курс: {c.Description} | давомнокии курс (ҳафта): {c.DurationWeeks}");
                else
                    Console.WriteLine("Ёфт нашуд!");
                break;

            case "4":
                var upd = new Course();
                Console.Write("номи курс: "); upd.Title = Console.ReadLine()!;
                Console.Write("тавсифи курс: "); upd.Description = Console.ReadLine()!;
                Console.Write("давомнокии курс (ҳафта): ");
                upd.DurationWeeks = Convert.ToInt32(Console.ReadLine()!);
                CourseService.UpdateCourse(upd);
                break;

            case "5":
                Console.Write("ID барои нест кардан: ");
                CourseService.DeleteCourse(Convert.ToInt32(Console.ReadLine()!));
                break;
        }
    }

    void MentorsMenu()
    {
        Console.WriteLine("=== МАҲСУЛОТ ===");
        Console.WriteLine("1. Илова кардан");
        Console.WriteLine("2. Намоиши ҳама");
        Console.WriteLine("3. Ёфтан аз рӯи ID");
        Console.WriteLine("4. Навсозӣ кардан");
        Console.WriteLine("5. Нест кардан");
        Console.Write("Интихоб: ");

        switch (Console.ReadLine())
        {
            case "1":
                var prod = new Mentor();
                Console.Write("Ном: "); prod.FullName = Console.ReadLine()!;
                Console.Write("Тавсиф: "); prod.Email = Console.ReadLine()!;
                Console.Write("Нарх: "); prod.Phone = Console.ReadLine()!;
                Console.Write("Миқдор: "); prod.Specialization = Console.ReadLine()!;
                MentorService.AddMentor(prod);
                break;

            case "2":
                var prods = MentorService.GetAllMentor();
                foreach (var p in prods)
                    Console.WriteLine($"MentorId: {p.MentorId}  Номи пурра: {p.FullName} | почтаи электронӣ: {p.Email} | рақами телефон: {p.Phone} ихтисос : {p.Specialization}");
                break;

            case "3":
                Console.Write("ID: ");
                var found = MentorService.GetMentorById(Convert.ToInt32(Console.ReadLine()!));
                if (found != null)
                    Console.WriteLine($"MentorId: {found.MentorId}  Номи пурра: {found.FullName} | почтаи электронӣ: {found.Email} | рақами телефон: {found.Phone} ихтисос : {found.Specialization}");
                else
                    Console.WriteLine("Ёфт нашуд!");
                break;

            case "4":
                var upd = new Mentor();
                Console.Write("ID: "); upd.MentorId = Convert.ToInt32(Console.ReadLine()!);
                Console.Write("Ном: "); upd.FullName = Console.ReadLine()!;
                Console.Write("Тавсиф: "); upd.Email = Console.ReadLine()!;
                Console.Write("Нарх: "); upd.Phone = Console.ReadLine()!;
                Console.Write("Миқдор: "); upd.Specialization = Console.ReadLine()!;
                MentorService.UpdateMentor(upd);
                break;

            case "5":
                Console.Write("ID барои нест кардан: ");
                MentorService.DeleteMentor(Convert.ToInt32(Console.ReadLine()!));
                break;
        }
    }

    void GroupsMenu()
    {
        Console.WriteLine("=== Гурӯҳҳо ===");
        Console.WriteLine("1. Эҷод кардан");
        Console.WriteLine("2. Намоиши ҳама");
        Console.WriteLine("3. Ёфтан аз рӯи ID");
        Console.WriteLine("4. Навсозӣ кардан");
        Console.WriteLine("5. Ҳазф кардан");
        Console.Write("Интихоб: ");

        switch (Console.ReadLine())
        {
            case "1":
                var group = new Group();
                Console.Write("Номи гурӯҳ: "); group.GroupName = Console.ReadLine()!;
                Console.Write("ID Курси вобаста: "); group.CourseId = Convert.ToInt32(Console.ReadLine()!);
                Console.Write("ID Мураббӣ: "); group.MentorId = Convert.ToInt32(Console.ReadLine()!);
                Console.Write("Санаи оғоз (yyyy-MM-dd): "); group.StartDate = DateOnly.Parse(Console.ReadLine()!);
                Console.Write("Санаи анҷом (yyyy-MM-dd): "); group.EndDate = DateOnly.Parse(Console.ReadLine()!);
                GroupService.AddGroup(group);
                break;

            case "2":
                var groups = GroupService.GetAllGroup();
                foreach (var g in groups)
                    Console.WriteLine($"GroupId: {g.GroupId} | Номи гурӯҳ: {g.GroupName} | CourseId: {g.CourseId} | MentorId: {g.MentorId} | Start: {g.StartDate:yyyy-MM-dd} | End: {g.EndDate:yyyy-MM-dd}");
                break;

            case "3":
                Console.Write("ID: ");
                var found = GroupService.GetGroupById(Convert.ToInt32(Console.ReadLine()!));
                if (found != null)
                    Console.WriteLine($"GroupId: {found.GroupId} | Номи гурӯҳ: {found.GroupName} | CourseId: {found.CourseId} | MentorId: {found.MentorId} | Start: {found.StartDate:yyyy-MM-dd} | End: {found.EndDate:yyyy-MM-dd}");
                else
                    Console.WriteLine("Ёфт нашуд!");
                break;

            case "4":
                var upd = new Group();
                Console.Write("ID: "); upd.GroupId = Convert.ToInt32(Console.ReadLine()!);
                Console.Write("Номи гурӯҳ: "); upd.GroupName = Console.ReadLine()!;
                Console.Write("ID Курси вобаста: "); upd.CourseId = Convert.ToInt32(Console.ReadLine()!);
                Console.Write("ID Мураббӣ: "); upd.MentorId = Convert.ToInt32(Console.ReadLine()!);
                Console.Write("Санаи оғоз (yyyy-MM-dd): "); upd.StartDate = DateOnly.Parse(Console.ReadLine()!);
                Console.Write("Санаи анҷом (yyyy-MM-dd): "); upd.EndDate = DateOnly.Parse(Console.ReadLine()!);
                GroupService.UpdateGroup(upd);
                break;

            case "5":
                Console.Write("ID барои ҳазф: ");
                GroupService.DeleteGroup(Convert.ToInt32(Console.ReadLine()!));
                break;

            default:
                Console.WriteLine("Интихоби нодуруст!");
                break;
        }
    }
    void StudentGroupsMenu()
    {
        Console.WriteLine("=== Донишҷӯён дар гурӯҳ ===");
        Console.WriteLine("1. Иловаи донишҷӯ ба гурӯҳ");
        Console.WriteLine("2. Намоиши донишҷӯён дар гурӯҳ");
        Console.WriteLine("3. Навсозии Status");
        Console.WriteLine("4. Ҳазфи донишҷӯ аз гурӯҳ");
        Console.Write("Интихоб: ");

        switch (Console.ReadLine())
        {
            case "1":
                var sg = new Domain.Models.StudentGroup();
                Console.Write("ID Донишҷӯ: "); sg.StudentId = Convert.ToInt32(Console.ReadLine()!);
                Console.Write("ID Гурӯҳ: "); sg.GroupId = Convert.ToInt32(Console.ReadLine()!);
                Console.WriteLine("Status интихоб кунед: 1=Фаъол, 2=Ихроҷшуда, 3=Хатмкарда");
                int statusInput = Convert.ToInt32(Console.ReadLine()!);
                sg.Status = (StudentStatus)statusInput;
                StudentGroupService.AddStudentToGroup(sg);
                break;

            case "2":
                Console.Write("ID Гурӯҳ: ");
                int groupId = Convert.ToInt32(Console.ReadLine()!);
                var students = StudentGroupService.GetStudentsByGroup(groupId);
                foreach (var s in students)
                    Console.WriteLine($"StudentGroupId: {s.StudentGroupId} | StudentId: {s.StudentId} | Status: {s.Status}");
                break;

            case "3":
                Console.Write("ID StudentGroup: ");
                int sgId = Convert.ToInt32(Console.ReadLine()!);
                Console.Write("Status нав (Фаъол / Ихроҷшуда / Хатмкарда): ");
                Console.WriteLine("Status интихоб кунед: 1=Фаъол, 2=Ихроҷшуда, 3=Хатмкарда");
                int newstatusInput = Convert.ToInt32(Console.ReadLine()!);
                StudentStatus newStatus = (StudentStatus)newstatusInput;
                StudentGroupService.UpdateStatus(sgId, newStatus);
                break;

            case "4":
                Console.Write("ID StudentGroup барои ҳазф: ");
                int removeId = Convert.ToInt32(Console.ReadLine()!);
                StudentGroupService.RemoveStudentFromGroup(removeId);
                break;

            default:
                Console.WriteLine("Интихоби нодуруст!");
                break;
        }
    }
    void AnalyticsMenu()
    {
        IStatisticsService statisticsService = new StatisticsService();
        IAnalyticsService analyticsService = new AnalyticsService();

        Console.WriteLine("\n=== АНАЛИТИКА ===");
        Console.WriteLine("1. Умумии донишҷӯён");
        Console.WriteLine("2. Умумии гурӯҳҳо");
        Console.WriteLine("3. Умумии курсҳо");
        Console.WriteLine("4. Умумии мураббиён");
        Console.WriteLine("5. Рӯйхати ҳамаи санаҳои оғоз");
        Console.WriteLine("6. Шумораи донишҷӯён дар ҳар гурӯҳ");
        Console.WriteLine("7. Шумораи донишҷӯён дар ҳар курс");
        Console.WriteLine("8. Курси маъмултарин");
        Console.WriteLine("9. 3 курси камтарин");
        Console.WriteLine("10. 3 курси маъмултарин");
        Console.WriteLine("11. Мураббӣ бо шумораи зиёди донишҷӯ");
        Console.WriteLine("12. Мураббиёни бо зиёда аз як курс");
        Console.WriteLine("13. Фоизи донишҷӯёни хатмкарда");
        Console.Write("Интихоб: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine($"Умумии донишҷӯён: {statisticsService.GetTotalStudentsCount()}");
                break;

            case "2":
                Console.WriteLine($"Умумии гурӯҳҳо: {statisticsService.GetTotalGroupsCount()}");
                break;

            case "3":
                Console.WriteLine($"Умумии курсҳо: {statisticsService.GetTotalCoursesCount()}");
                break;

            case "4":
                Console.WriteLine($"Умумии мураббиён: {statisticsService.GetTotalMentorsCount()}");
                break;

            case "5":
                Console.WriteLine("Рӯйхати ҳамаи санаҳои оғоз:");
                foreach (var date in statisticsService.GetAllStartDates())
                    Console.WriteLine($"  {date:yyyy-MM-dd}");
                break;

            case "6":
                Console.WriteLine("Шумораи донишҷӯён дар ҳар гурӯҳ:");
                foreach (var kv in GroupService.GetStudentsPerGroup())
                    Console.WriteLine($"  Гурӯҳ {kv.GroupId}: {kv.StudentCount} донишҷӯ");
                break;

            case "7":
                Console.WriteLine("Шумораи донишҷӯён дар ҳар курс:");
                foreach (var kv in CourseService.GetStudentsPerCourse())
                    Console.WriteLine($"  Курс {kv.CourseId}: {kv.StudentCount} донишҷӯ"); break;

            case "8":
                Console.WriteLine($"Курси маъмултарин: {CourseService.GetMostPopularCourse()}");
                break;

            case "9":
                Console.WriteLine("3 курси камтарин:");
                var leastPopular = CourseService.GetLeastPopularCourses();
                for (int i = 0; i < leastPopular.Count; i++)
                    Console.WriteLine($"  {i + 1}. {leastPopular[i]}");
                break;

            case "10":
                Console.WriteLine("3 курси маъмултарин:");
                var topThree = CourseService.GetTopThreeCourses();
                for (int i = 0; i < topThree.Count; i++)
                    Console.WriteLine($"  {i + 1}. {topThree[i]}");
                break;

            case "11":
                Console.WriteLine($"Мураббӣ бо шумораи зиёди донишҷӯ: {MentorService.GetMentorWithMostStudents()}");
                break;

            case "12":
                Console.WriteLine("Мураббиёни бо зиёда аз як курс:");
                foreach (var mentor in MentorService.GetMentorsWithMultipleCourses())
                    Console.WriteLine($"  {mentor}");
                break;

            case "13":
                Console.WriteLine($"Фоизи донишҷӯёни хатмкарда: {analyticsService.GetCompletionRate():F2}%");
                break;

            default:
                Console.WriteLine("Интихоби нодуруст!");
                break;
        }
    }
}