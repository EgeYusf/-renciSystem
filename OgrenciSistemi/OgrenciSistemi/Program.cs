using System;
using System.Collections.Generic;

// Base Class
abstract class Person //Person sınıfı bir abstract class (soyut sınıf) olarak tanımlanmıştır.
{
    public int ID { get; set; }
    public string Name { get; set; }

    public Person(int id, string name)
    {
        ID = id;
        Name = name;
    }

    public abstract void DisplayInfo(); // void herhangi bir değer döndürmez
}

// Interface
interface ILogin 
{
    void Login();
}

// Derived Class: Ogrenci
class Ogrenci : Person, ILogin  //Ogrenci sınıfı, Person sınıfının tüm özelliklerini (ID, Name) ve yapıcı metodunu kullanabilir.
{
    public string StudentNumber { get; set; }

    public Ogrenci(int id, string name, string studentNumber) : base(id, name) // personda tanımlananı çeker
    {
        StudentNumber = studentNumber;
    }

    public override void DisplayInfo() // override Person sınıfında) tanımlı olan bir sanal veya soyut  metodu yeniden tanımlamak için kullanılır.
    {
        Console.WriteLine($"Öğrenci - ID: {ID}, İsim: {Name}, Numara: {StudentNumber}"); // Burada da yazdırıyoruz.
    }

    public void Login() //Login yapıldıgında {} isim ne giriliyorsa
    {
        Console.WriteLine($"Öğrenci {Name} sisteme giriş yaptı.");
    }
}

// Derived Class: OgretimGorevlisi
class OgretimGorevlisi : Person, ILogin
{
    public string Department { get; private set; } = "Henüz Atanmamış";

    public OgretimGorevlisi(int id, string name) : base(id, name) { }

    public void SetDepartment(string newDepartment)
    {
        Department = newDepartment;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Öğretim Görevlisi - ID: {ID}, İsim: {Name}, Bölüm: {Department}");
    }

    public void Login()
    {
        Console.WriteLine($"Öğretim Görevlisi {Name} sisteme giriş yaptı.");
    }
}

// Ders (Course) Class
class Course
{
    public string CourseName { get; set; }
    public int Credits { get; set; }
    public OgretimGorevlisi Instructor { get; set; }
    public List<Ogrenci> EnrolledStudents { get; set; }

    public Course(string courseName, int credits, OgretimGorevlisi instructor)
    {
        CourseName = courseName;
        Credits = credits;
        Instructor = instructor;
        EnrolledStudents = new List<Ogrenci>();
    }

    public void AddStudent(Ogrenci student)
    {
        if (EnrolledStudents.Exists(s => s.ID == student.ID)) //Öğrencinin Önceden Kayıtlı Olup Olmadığını Kontrol Etme
        {
            Console.WriteLine($"{student.Name} zaten bu derse kayıtlı.");
        }
        else //Eğer kayıtlı değilse else bloguna girer ve add metoduyla kaydeder.
        {
            EnrolledStudents.Add(student); 
            Console.WriteLine($"{student.Name} başarıyla {CourseName} dersine eklendi.");
        }
    }

    public void DisplayCourseInfo()
    {
        Console.WriteLine($"Ders: {CourseName}, Kredi: {Credits}, Öğretim Görevlisi: {Instructor.Name}");
        Console.WriteLine("Kayıtlı Öğrenciler:");
        if (EnrolledStudents.Count == 0) // Derse kayıtlı öğrencilerin sayısını kontrol eder.
        {
            Console.WriteLine(" - Henüz öğrenci yok.");  // Yoksa yok mesajı verilir.
        }
        else // Var ise foreach döngüsüyle öğrencileri yazdırır.
        {
            foreach (var student in EnrolledStudents)
            {
                Console.WriteLine($" - {student.Name} ({student.StudentNumber})");
            }
        }
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        List<Ogrenci> students = new List<Ogrenci>();  // Sistemdeki tüm öğrencileri tutar.
        List<OgretimGorevlisi> instructors = new List<OgretimGorevlisi>(); //Sistemdeki öğretim görevlilerini tutar.
        List<Course> courses = new List<Course>(); //Sistemdeki dersleri tutar.

        while (true) // Sonsuz döngü olusturduk kullanıcı 8 i seçene kadar program çalışır
        { // Menü kısmı
        
            Console.WriteLine("\n--- Öğrenci ve Ders Yönetim Sistemi ---");
            Console.WriteLine("1. Öğrenci Ekle");
            Console.WriteLine("2. Öğretim Görevlisi Ekle");
            Console.WriteLine("3. Ders Ekle");
            Console.WriteLine("4. Derse Öğrenci Ekle");
            Console.WriteLine("5. Ders Bilgilerini Göster");
            Console.WriteLine("6. Öğretim Görevlisi Bölüm Atama");
            Console.WriteLine("7. Listeleme (Öğrenci/Öğretim Görevlisi)");
            Console.WriteLine("8. Çıkış");
            Console.Write("Seçiminiz: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: //1. Öğrenci Ekle (Case 1)
                    Console.Write("Öğrenci ID: ");
                    int studentId = int.Parse(Console.ReadLine());
                    Console.Write("Öğrenci Adı: ");
                    string studentName = Console.ReadLine();
                    Console.Write("Öğrenci Numarası: ");
                    string studentNumber = Console.ReadLine();
                    students.Add(new Ogrenci(studentId, studentName, studentNumber));
                    Console.WriteLine("Öğrenci başarıyla eklendi!");
                    break;

                case 2:  //2. Öğretim Görevlisi Ekle (Case 2)
                    Console.Write("Öğretim Görevlisi ID: ");
                    int instructorId = int.Parse(Console.ReadLine());
                    Console.Write("Öğretim Görevlisi Adı: ");
                    string instructorName = Console.ReadLine();
                    instructors.Add(new OgretimGorevlisi(instructorId, instructorName));
                    Console.WriteLine("Öğretim Görevlisi başarıyla eklendi!");
                    break;

                case 3: //3. Ders Ekle (Case 3)
                    Console.Write("Ders Adı: ");
                    string courseName = Console.ReadLine();
                    Console.Write("Kredisi: ");
                    int credits = int.Parse(Console.ReadLine());
                    Console.WriteLine("Öğretim Görevlisi Seçin:");
                    for (int i = 0; i < instructors.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {instructors[i].Name}");
                    }
                    int instructorIndex = int.Parse(Console.ReadLine()) - 1;
                    courses.Add(new Course(courseName, credits, instructors[instructorIndex]));
                    Console.WriteLine("Ders başarıyla eklendi!");
                    break;

                case 4:  //4. Derse Öğrenci Ekle (Case 4)
                    Console.WriteLine("Ders Seçin:");
                    for (int i = 0; i < courses.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {courses[i].CourseName}");
                    }
                    int courseIndex = int.Parse(Console.ReadLine()) - 1;

                    Console.WriteLine("Öğrenci Seçin:");
                    for (int i = 0; i < students.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {students[i].Name}");
                    }
                    int studentIndex = int.Parse(Console.ReadLine()) - 1;

                    courses[courseIndex].AddStudent(students[studentIndex]);
                    break;

                case 5:
                    Console.WriteLine("Ders Seçin:");
                    for (int i = 0; i < courses.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {courses[i].CourseName}");
                    }
                    int selectedCourseIndex = int.Parse(Console.ReadLine()) - 1;
                    courses[selectedCourseIndex].DisplayCourseInfo();
                    break;

                case 6:
                    Console.WriteLine("Öğretim Görevlisi Seçin:");
                    for (int i = 0; i < instructors.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {instructors[i].Name}");
                    }
                    int selectedInstructorIndex = int.Parse(Console.ReadLine()) - 1;
                    Console.Write("Yeni Bölüm: ");
                    string newDepartment = Console.ReadLine();
                    instructors[selectedInstructorIndex].SetDepartment(newDepartment);
                    Console.WriteLine("Bölüm başarıyla güncellendi!");
                    break;

                case 7:
                    Console.WriteLine("\n--- Listeleme ---");
                    Console.WriteLine("1. Öğrencileri Listele");
                    Console.WriteLine("2. Öğretim Görevlilerini Listele");
                    Console.Write("Seçiminiz: ");
                    int listChoice = int.Parse(Console.ReadLine());

                    if (listChoice == 1)
                    {
                        Console.WriteLine("\n--- Öğrenciler ---");
                        if (students.Count == 0)
                        {
                            Console.WriteLine("Kayıtlı öğrenci bulunmamaktadır.");
                        }
                        else
                        {
                            foreach (var student in students)
                            {
                                student.DisplayInfo();
                            }
                        }
                    }
                    else if (listChoice == 2)
                    {
                        Console.WriteLine("\n--- Öğretim Görevlileri ---");
                        if (instructors.Count == 0)
                        {
                            Console.WriteLine("Kayıtlı öğretim görevlisi bulunmamaktadır.");
                        }
                        else
                        {
                            foreach (var instructor in instructors)
                            {
                                instructor.DisplayInfo();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz seçim! Tekrar deneyin.");
                    }
                    break;

                case 8:
                    Console.WriteLine("Çıkış yapılıyor...");
                    return;

                default:
                    Console.WriteLine("Geçersiz seçim! Tekrar deneyin.");
                    break;
            }
        }
    }
}

