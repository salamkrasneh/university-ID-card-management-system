using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Versioning;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Eventing.Reader;



namespace ours
{

    abstract class Person
    {
        public abstract void rechargeCard();


    }
    class Student : Person
    {
        Transaction transaction;
        private string userID;
        private string name;
        private string registedCourses;
        Cards card = null;
        public Student(string name, string userID, string registedCourses)
        {
            this.name = name;
            this.userID = userID;
            this.registedCourses = registedCourses;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string RegistedCourses
        {
            get { return registedCourses; }
            set { registedCourses = value; }
        }

        // one

        public override void rechargeCard()
        {
            // is doneeeeeeeeeeeeeeeeeeee


            Console.WriteLine("Please enter your card number:");
            int cardNumber = Convert.ToInt32(Console.ReadLine());
            string cardsFile = "cards.json";
            List<Cards> cardsList = Program.loadCards(cardsFile);

            Cards card = cardsList.Find(c => c.Number == cardNumber && c.Type == "Student");
            if (card == null)
            {
                Console.WriteLine("Card not found or not a student card.");
                return;
            }
            Console.WriteLine($"Card Number: {card.Number}, Balance: {card.Balance}, Type: {card.Type}, Status: {card.Status}");


            Console.WriteLine("Please enter the amount to recharge:");
            double rechargeAmount = Convert.ToDouble(Console.ReadLine());


            double oldBalance = card.Balance;
            card.Balance += rechargeAmount;
            Console.WriteLine($"New balance:  = {card.Balance}");


            Program.SaveCards(cardsFile, cardsList);


            Console.WriteLine("Please enter transaction ID :");
            int transactionId = Convert.ToInt32(Console.ReadLine());
            Transaction newTransaction = new Transaction(transactionId, DateTime.Now.ToString(), "Recharge", rechargeAmount, cardNumber);


            string transactionsFile = "transactions.json";
            List<Transaction> transactions = Program.LoadTransaction(transactionsFile) ?? new List<Transaction>();
            transactions.Add(newTransaction);
            Program.SaveTransactions(transactionsFile, transactions);

            Console.WriteLine($"Transaction recorded successfully. Transaction ID: {transactionId}");






        }

        public void recordLectureAttendancce()

        {

            ///IS DONEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
            Console.WriteLine("please enter your ID :");
            string h = Console.ReadLine();



            string fname = "students.json";
            List<Student> loadedStudents = Program.LoadStudents(fname);
            Student a = loadedStudents.Find(c => c.UserID != h);

            if (a != null)

                Console.WriteLine($"RegistedCourses: {a.RegistedCourses}");

            Console.WriteLine("please enter your course ID :");
            string q = Console.ReadLine();
            Console.WriteLine("please enter your Date :");
            string d = Console.ReadLine();

            string attendanceFile = "attendance.json";
            List<AttendanceRecord> attendanceRecords;
            if (File.Exists(attendanceFile))
            {
                string json = File.ReadAllText(attendanceFile);
                attendanceRecords = JsonConvert.DeserializeObject<List<AttendanceRecord>>(json) ?? new List<AttendanceRecord>();
            }
            else
            {
                attendanceRecords = new List<AttendanceRecord>();
            }


            AttendanceRecord record = attendanceRecords.Find(c => c.CourseId == q && c.Date == d);
            if (record == null)
            {
                record = new AttendanceRecord
                {
                    CourseId = q,
                    Date = d,
                    Attendees = new List<string>()
                };
                attendanceRecords.Add(record);
            }

            if (!record.Attendees.Contains(h))
            {
                record.Attendees.Add(h);
                Console.WriteLine("Attendance recorded.");
            }
            else
            {
                Console.WriteLine("You have already been marked as attended for this lecture.");
            }

            File.WriteAllText(attendanceFile, JsonConvert.SerializeObject(attendanceRecords, Formatting.Indented));

            Console.WriteLine("Please enter transaction ID:");
            int transactionId = Convert.ToInt32(Console.ReadLine());
            Transaction attendanceTransaction = new Transaction(transactionId, DateTime.Now.ToString(), "Attendance", 0, 0);

            string transactionsFile = "transactions.json";
            List<Transaction> transactions = Program.LoadTransaction(transactionsFile) ?? new List<Transaction>();
            transactions.Add(attendanceTransaction);
            Program.SaveTransactions(transactionsFile, transactions);

            Console.WriteLine("Attendance transaction recorded successfully.");



        }

        public void payForCafeteria()
        {
            // is doneeeeeeeeeeeeeeeeeeee
            Console.WriteLine("Enter your Card number:");
            int c = Convert.ToInt32(Console.ReadLine());
            string fname3 = "cards.json";
            List<Cards> loadCards = Program.loadCards(fname3);


            int i;
            double Totale = 0;

            do
            {
                Console.WriteLine("Cafeteria Menu:");
                Console.WriteLine("ITEM 1 : steak , cost 8JD");
                Console.WriteLine("ITEM 2 : soup , cost 2JD");
                Console.WriteLine("ITEM 3 : salad , cost 4JD");
                Console.WriteLine("ITEM 4: sandwich , cost 3JD");
                Console.WriteLine("   ");
                Console.WriteLine("ITEM 5: Tea , cost 2JD");
                Console.WriteLine("ITEM 6: juice , cost 3JD");
                Console.WriteLine("ITEM 7: cake , cost 5JD");
                Console.WriteLine("ITEM 8: water , cost 1JD");
                Console.WriteLine(" 9 Exit from menu");
                Console.WriteLine("   ");
                Console.WriteLine(" Enter your choices:");
                i = Convert.ToInt32(Console.ReadLine());
                switch (i)
                {
                    case 1: Totale += 8; break;
                    case 2: Totale += 2; break;
                    case 3: Totale += 4; break;
                    case 4: Totale += 3; break;
                    case 5: Totale += 2; break;
                    case 6: Totale += 3; break;
                    case 7: Totale += 5; break;
                    case 8: Totale += 1; break;
                    case 9: Console.WriteLine("EXITING MENU"); break;
                    default:
                        Console.WriteLine("invalide choice");
                        break;

                }
            }
            while (i != 9);

            double p = Totale;
            foreach (Cards C in loadCards)
            {
                if (C.Number == c)
                {



                    C.Balance -= p;
                    Console.WriteLine("your balance: " + C.Balance);

                }
            }

            Program.SaveCards(fname3, loadCards);


            Console.WriteLine("Please enter transaction ID :");
            int transactionId = Convert.ToInt32(Console.ReadLine());
            Transaction paytransaction = new Transaction(transactionId, DateTime.Now.ToString(), "Payment", p, c);

            string fname4 = "transactions.json";
            List<Transaction> LoadTransaction = Program.LoadTransaction(fname4) ?? new List<Transaction>();
            LoadTransaction.Add(paytransaction);

            Program.SaveTransactions(fname4, LoadTransaction);

            Console.WriteLine("Payment transaction recorded successfully.");



        }



        public void payForBusRide()
        {
            //is doneeeeeeeeeeeeeeeeeeee
            Console.WriteLine("Enter Your Card number:");
            int c = Convert.ToInt32(Console.ReadLine());
            string fname3 = "cards.json";
            List<Cards> loadCards = Program.loadCards(fname3);

            double totale = 0;
            Console.WriteLine("Track 1 : from main gate to Northen Building cost 3JD");
            Console.WriteLine("Track 2 : from main gate to Southren Building cost 4JD");
            Console.WriteLine("Track 3 : from main gate to Library cost 5JD");
            Console.WriteLine("   ");
            Console.WriteLine("please enter your Track (D1 ,D2 ,D3 ) : ");

            switch (Console.ReadLine())
            {
                case "D1":

                    totale = 3;
                    break;
                case "D2":
                    totale = 4;
                    break;
                case "D3":
                    totale = 5;
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
            double p = totale;

            foreach (Cards C in loadCards)
            {
                if (C.Number == c)
                {
                    C.Balance -= p;
                    Console.WriteLine("your balance: " + C.Balance);

                }

            }
            Program.SaveCards(fname3, loadCards);

            Console.WriteLine("Please enter transaction ID :");
            int transactionId = Convert.ToInt32(Console.ReadLine());
            Transaction paytransaction = new Transaction(transactionId, DateTime.Now.ToString(), "Payment", p, c);

            string fname4 = "transactions.json";
            List<Transaction> LoadTransaction = Program.LoadTransaction(fname4) ?? new List<Transaction>();
            LoadTransaction.Add(paytransaction);

            Program.SaveTransactions(fname4, LoadTransaction);

            Console.WriteLine("Payment transaction recorded successfully.");







        }
        public void viewTransactionHistory()
        {
            //is doneeeeeeeeeeeeeeeeeeee
            Console.WriteLine("Please Enter your CARD number:   ");
            int h = Convert.ToInt32(Console.ReadLine());
            string fname4 = "transactions.json";
            List<Transaction> LoadTransaction = Program.LoadTransaction(fname4);
            Console.WriteLine("Your Transactions:");
            foreach (Transaction T in LoadTransaction)
            {
                if (T.CARDNUMBER == h)
                {
                    transaction = T;
                    Console.WriteLine($" Transaction ID: {T.ID}, Date: {T.Date}, Type: {T.Type}, Amount: {T.Amount}, CARDNUMBER:{T.CARDNUMBER}");
                }
            }

        }
    }

    class FacultyMember : Person
    {
        Transaction transaction;
        private string userID;
        private string name;
        private string taughtCourses;
        Cards card = null;
        public FacultyMember(string name, string userID, string taughtCourses)
        {
            this.name = name;
            this.userID = userID;
            this.taughtCourses = taughtCourses;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string TaughtCourses
        {
            get { return taughtCourses; }
            set { taughtCourses = value; }
        }

        public double pay(double t)
        {
            double T = t;
            Console.WriteLine("Totale =" + t);

            return t;
        }
        public override void rechargeCard()
        {
            // is doneeeeeeeeeeeeeeeeeeee   
            Console.WriteLine("Pleas Enter your card number");

            int x = Convert.ToInt32(Console.ReadLine());
            string fname3 = "cards.json";

            List<Cards> loadCards = Program.loadCards(fname3);

            loadCards.Find(c => c.Number == x);

            foreach (Cards C in loadCards)
            {
                if (C.Number == x && C.Type == "FaculyMember")


                {
                    Console.WriteLine($"  Card Number: {C.Number}, Balance: {C.Balance}, Type: {C.Type}, Status: {C.Status}");
                    card = C;

                    Console.WriteLine("Pleas Enter the amount to recharge");
                    double y = Convert.ToDouble(Console.ReadLine());
                    card.Balance += y;
                    Console.WriteLine("New balance: " + card.Balance);

                    Program.SaveCards(fname3, loadCards);

                    /////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("Please enter transaction ID :");
                    int transactionId = Convert.ToInt32(Console.ReadLine());
                    Transaction paytransaction = new Transaction(transactionId, DateTime.Now.ToString(), "Payment", y, x);

                    string fname4 = "transactions.json";
                    List<Transaction> LoadTransaction = Program.LoadTransaction(fname4) ?? new List<Transaction>();
                    LoadTransaction.Add(paytransaction);

                    Program.SaveTransactions(fname4, LoadTransaction);

                    Console.WriteLine("Payment transaction recorded successfully.");


                    ///////////////////////////////////////////////////////////////////////////////////////////////////

                }
                else if (C.Number == x && C.Type != "FaculyMember")
                {
                    Console.WriteLine("This card is not for faculty member");
                    card = C;
                }
            }


        }



        public void accessCarParking()
        {
            //is doneeeeeeeeeeeeeeeeeeee
            Console.WriteLine("Please Enter your CARD number:   ");
            int h = Convert.ToInt32(Console.ReadLine());
            string fname3 = "cards.json";
            List<Cards> loadCards = Program.loadCards(fname3);


            Console.WriteLine("Parking Fees :");
            Console.WriteLine("1st hour : 5JD");
            Console.WriteLine("2ed hour : 4JD");
            Console.WriteLine("3rd hour : 3JD");
            Console.WriteLine("4th hour : 2JD");
            Console.WriteLine("5th hour : 1JD");

            Console.WriteLine("Enter your duration of parking");
            int e = Convert.ToInt32(Console.ReadLine());
            int totale = 0;
            if (e >= 0)
            {
                switch (e)
                {
                    case 1:
                        totale = 5;
                        break;

                    case 2:
                        totale = 5 + 4;
                        break;
                    case 3:
                        totale = 3 + 5 + 4;
                        break;
                    case 4:
                        totale = 2 + 3 + 5 + 4;

                        break;
                    case 5:
                        totale = 1 + 2 + 3 + 5 + 4;
                        break;
                    default:
                        totale = 1 + 2 + 3 + 5 + 4;

                        Console.WriteLine("above 5 hours it's for free");
                        break;
                }
            }
            double p = totale;

            foreach (Cards C in loadCards)
            {
                if (C.Number == h)
                {
                    C.Balance -= p;
                    Console.WriteLine("your balance: " + C.Balance);
                }

            }
            Program.SaveCards(fname3, loadCards);

            Console.WriteLine("Please enter transaction ID :");
            int transactionId = Convert.ToInt32(Console.ReadLine());
            Transaction paytransaction = new Transaction(transactionId, DateTime.Now.ToString(), "Payment", p, h);

            string fname4 = "transactions.json";
            List<Transaction> LoadTransaction = Program.LoadTransaction(fname4) ?? new List<Transaction>();
            LoadTransaction.Add(paytransaction);

            Program.SaveTransactions(fname4, LoadTransaction);

            Console.WriteLine("Payment transaction recorded successfully.");




        }
        public void generateAttendanceReport()
        {
            //is doneeeeeeeeeeeeeeeeeeee
            Console.WriteLine("Enter your ID");
            string h = Console.ReadLine();
            string fname = "facultyMembers.json";
            List<FacultyMember> loadFaculty1 = Program.loadFaculty(fname);
            FacultyMember a = loadFaculty1.Find(c => c.UserID == h);
            if (a != null)
                Console.WriteLine($"TaughtCourses: {a.TaughtCourses}");

            Console.WriteLine("Enter course ID:");
            string q = Console.ReadLine();
            Console.WriteLine("Enter the Date of course");
            string d = Console.ReadLine();

            string attendanceFile = "attendance.json";
            if (!File.Exists(attendanceFile))
            {
                Console.WriteLine("No attendance records found.");
                return;
            }

            string json = File.ReadAllText(attendanceFile);
            List<AttendanceRecord> attendanceRecords = JsonConvert.DeserializeObject<List<AttendanceRecord>>(json) ?? new List<AttendanceRecord>();

            AttendanceRecord record = attendanceRecords.Find(r => r.CourseId == q && r.Date == d);

            if (record == null || record.Attendees == null || record.Attendees.Count == 0)
            {
                Console.WriteLine("No students attended this course on this date.");
                return;
            }


            List<Student> students = Program.LoadStudents("students.json");

            Console.WriteLine("Attending students:");
            foreach (string studentId in record.Attendees)
            {
                Student student = students.Find(s => s.UserID == studentId);
                if (student != null)
                    Console.WriteLine($" {student.Name}  Student ID : {student.UserID}" +
                        $"");
                else
                    Console.WriteLine($"Student ID : {studentId}");
            }






        }


    }
    class Transaction
    {


        private int Id;
        private string date;
        private string type;
        private double amount;
        private int cardnumber;


        public Transaction()
        {
        }
        public Transaction(int id, string date, string type, double amount, int cardnumber)
        {
            this.Id = id;
            this.date = date;
            this.type = type;
            this.amount = amount;
            this.cardnumber = cardnumber;
        }
        public int CARDNUMBER
        {
            set { cardnumber = value; }
            get { return cardnumber; }
        }

        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }

        public Transaction(string date, string type, double amount)
        {
            this.date = date;
            this.type = type;
            this.amount = amount;
        }
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }
    class Administrator
    {
        public void issueCard()
        {
            // is doneeeeeeeeeeeeeeeeeeee

            Console.WriteLine("Please enter card number:");
            int cardNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter card type:");
            string cardType = Console.ReadLine();

            Console.WriteLine("Please enter user ID:");
            string userId = Console.ReadLine();


            Cards newCard = new Cards(cardNumber, 50.0, cardType, "unblocked");


            string cardsFile = "cards.json";
            List<Cards> cardsList = Program.loadCards(cardsFile) ?? new List<Cards>();
            cardsList.Add(newCard);
            Program.SaveCards(cardsFile, cardsList);

            Console.WriteLine("Card issued successfully.");
        }
        public void blockCard()
        {
            string fname3 = "cards.json";
            List<Cards> loadCards = Program.loadCards(fname3);

            foreach (Cards C in loadCards)
            {
                if (C.Status == "unblocked")

                    Console.WriteLine($"  Card Number: {C.Number}, Balance: {C.Balance}, Type: {C.Type}, Status: {C.Status}");

            }
            Console.WriteLine("please Enter the card number to block");
            int x = Convert.ToInt32(Console.ReadLine());
            loadCards.Find(c => c.Number == x);
            foreach (Cards C in loadCards)
            {
                if (C.Number == x)
                {
                    C.Status = "blocked";
                    Console.WriteLine("Card blocked successfully");

                }

            }
            Program.SaveCards(fname3, loadCards);


        }
        public void unblockCard()
        {
            string fname3 = "cards.json";
            List<Cards> loadCards = Program.loadCards(fname3);
            foreach (Cards C in loadCards)
            {
                if (C.Status == "blocked")

                    Console.WriteLine($"  Card Number: {C.Number}, Balance: {C.Balance}, Type: {C.Type}, Status: {C.Status}");

            }
            Console.WriteLine("please Enter the card number to unblock");
            int x = Convert.ToInt32(Console.ReadLine());
            loadCards.Find(c => c.Number == x);
            foreach (Cards C in loadCards)
            {
                if (C.Number == x)
                {
                    C.Status = "unblocked";
                    Console.WriteLine("Card unblocked successfully");
                }
            }
            Program.SaveCards(fname3, loadCards);

        }
        public void viewAllCards()
        {



            string fname3 = "cards.json";
            List<Cards> loadCards = Program.loadCards(fname3);
            foreach (Cards C in loadCards)
            {

                Console.WriteLine($"  Card Number: {C.Number}, Balance: {C.Balance}, Type: {C.Type}, Status: {C.Status}");

            }



        }
        public void viewAllTransactions()
        {
            string fname = "transactions.json";
            List<Transaction> allTransactions = Program.LoadTransaction(fname);

            if (allTransactions == null || allTransactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            Console.WriteLine("All Transactions:");
            foreach (Transaction t in allTransactions)
            {
                Console.WriteLine($" Transaction ID: {t.ID}, Date: {t.Date}, Type: {t.Type}, Amount: {t.Amount}, Card Number: {t.CARDNUMBER}");
            }

        }
    }
    class Cards
    {
        private int number;
        private double balance;
        private string type;
        private string status;

        public Cards(int number, double balance, string type, string status)
        {
            this.number = number;
            this.balance = balance;
            this.type = type;
            this.status = status;
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public double Balance
        {

            get { return balance; }
            set
            {
                if (value >= 0) { balance = value; }

                else { Console.WriteLine("Balance cannot be negative"); }

            }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

    }
    class AttendanceRecord
    {
        public string courseId;
        public string date;
        public List<string> Attendees;

        public string CourseId
        {
            set
            {
                courseId = value;
            }
            get
            {
                return courseId;
            }
        }
        public string Date
        {
            set
            {
                date = value;
            }
            get
            {
                return date;
            }
        }

        public AttendanceRecord() { }
        public AttendanceRecord(string courseId, string date, List<string> Attendees)
        {
            this.courseId = courseId;
            this.date = date;
            this.Attendees = Attendees;
        }

        public void AddAttendanceStudent()
        {
            if (Attendees != null) { Attendees = new List<string>(); }

        }

    }



    internal class Program
    {

        static void chooseType(Person p)
        {
            p.rechargeCard();
        }
        static void Main(string[] args)
        {
            if (!File.Exists("students.json"))
            {


                Student s1 = new Student("S01", "Ali", "CPE100,SE400");
                Student s2 = new Student("S02", "Omar", "CPE100,NE200");
                Student s3 = new Student("S03", "Reem", "NES200,CIS300,SE400");
                Student s4 = new Student("S04", "Maher", "CPE100,SE400");



                List<Student> userID = new List<Student>();
                userID.Add(s1);
                userID.Add(s2);
                userID.Add(s3);
                string fname = "students.json";
                SaveStudent(fname, userID);
                //  Console.WriteLine(" ////////////////////////////////////////////////");

                List<Student> loadedStudents = LoadStudents(fname);
            }


            ///////////////////////////////////////////////////////////////////////

            if (!File.Exists("facultyMembers.json"))
            {
                FacultyMember f1 = new FacultyMember("F01", "Sami", "CPE100,CIS300");
                FacultyMember f2 = new FacultyMember("F02", "Eman", "NE200,SE400");
                List<FacultyMember> userID2 = new List<FacultyMember>();
                userID2.Add(f1);
                userID2.Add(f2);

                string fname2 = "facultyMembers.json";

                SaveFaculty(fname2, userID2);
                //  Console.WriteLine(" ////////////////////////////////////////////////");

                List<FacultyMember> loadFaculty1 = loadFaculty(fname2);
            }


            ////////////////////////////////////////////////////////////////
            ///
            if (!File.Exists("cards.json"))
            {


                Cards c1 = new Cards(10, 80.0, "FaculyMember", "unblocked");
                Cards c2 = new Cards(20, 110.0, "Student", "unblocked");
                Cards c3 = new Cards(30, 95.0, "Student", "blocked");
                Cards c4 = new Cards(40, 160.0, "Student", "unblocked");
                List<Cards> cardNumber = new List<Cards>();
                cardNumber.Add(c1);
                cardNumber.Add(c2);
                cardNumber.Add(c3);
                cardNumber.Add(c4);
                // Console.WriteLine(" ////////////////////////////////////////////////");
                string fname3 = "cards.json";
                SaveCards(fname3, cardNumber);

                List<Cards> loadCards1 = loadCards(fname3);
            }

            //////////////////////////////////////////////////////////////

            bool x = true;
            while (x)
            {
                Console.WriteLine("[1] Login As Admin");
                Console.WriteLine("[2] Login As Card Holder");
                Console.WriteLine("[3] Exit");

                Console.WriteLine("  ");
                Console.WriteLine(" Enter Your Choice: ");
                int y = Convert.ToInt32(Console.ReadLine());
                switch (y)
                {
                    case 1:
                        Console.WriteLine("Admin Login Selected");
                        Console.WriteLine("  ");
                        Console.WriteLine("[1] Issue Card ");
                        Console.WriteLine("[2] Block Card ");
                        Console.WriteLine("[3] Unblock Card ");
                        Console.WriteLine("[4] View all Cards ");
                        Console.WriteLine("[5] View all Transactions ");
                        Console.WriteLine("[6] Back To Main Login Screen ");

                        Console.WriteLine("  ");
                        Console.WriteLine(" Enter Your Choice: ");
                        int z = Convert.ToInt32(Console.ReadLine());

                        if (z == 1)
                        {
                            Administrator a = new Administrator();
                            a.issueCard();

                        }
                        else if (z == 2)
                        {
                            Administrator a = new Administrator();
                            a.blockCard();
                        }
                        else if (z == 3)
                        {
                            Administrator a = new Administrator();
                            a.unblockCard();
                        }
                        else if (z == 4)
                        {
                            Administrator a = new Administrator();
                            a.viewAllCards();
                        }
                        else if (z == 5)
                        {
                            Administrator a = new Administrator();
                            a.viewAllTransactions();
                        }
                        else if (z == 6)
                        {
                            Console.WriteLine("Back to main screen ");
                            break;
                        }
                        x = false;
                        break;

                    case 2:
                        bool cardHolderMenu = true;
                        while (cardHolderMenu)
                        {
                            Console.WriteLine("Card Holder Login Selected");
                            Console.WriteLine("  ");
                            Console.WriteLine("[1] Login As Student ");
                            Console.WriteLine("[2] Login As Faculty Member ");
                            Console.WriteLine("[3] Back To Main Login Screen ");
                            Console.WriteLine("  ");
                            Console.WriteLine(" Enter Your Choice: ");
                            int z2 = Convert.ToInt32(Console.ReadLine());

                            if (z2 == 1)
                            {
                                Console.WriteLine("Please enter your card number:");
                                int cardNumber = Convert.ToInt32(Console.ReadLine());
                                List<Cards> cardsList = loadCards("cards.json");
                                Cards card = cardsList.Find(c => c.Number == cardNumber && c.Type == "Student");
                                if (card == null)
                                {
                                    Console.WriteLine("Card not found or not a student card.");
                                    continue;
                                }
                                if (card.Status == "blocked")
                                {
                                    Console.WriteLine("This card is blocked. You cannot access student functions.");
                                    continue;
                                }

                                bool studentMenu = true;
                                while (studentMenu)
                                {
                                    Console.WriteLine("[1] Recharge Card ");
                                    Console.WriteLine("[2] Record lecture Attendance ");
                                    Console.WriteLine("[3] Pay for Cafeteria ");
                                    Console.WriteLine("[4] Pay for Bus ride ");
                                    Console.WriteLine("[5] View transaction history ");
                                    Console.WriteLine("[6] Logout ");

                                    Console.WriteLine("  ");
                                    Console.WriteLine(" Enter Your Choice: ");

                                    int z3 = Convert.ToInt32(Console.ReadLine());
                                    if (z3 == 1)
                                    {
                                        Student sss = new Student("", "", "");
                                        chooseType(sss);
                                    }
                                    else if (z3 == 2)
                                    {
                                        Student ss = new Student("", "", "");
                                        ss.recordLectureAttendancce();
                                    }
                                    else if (z3 == 3)
                                    {
                                        Student ss = new Student("", "", "");
                                        ss.payForCafeteria();
                                    }
                                    else if (z3 == 4)
                                    {
                                        Student ss = new Student("", "", "");
                                        ss.payForBusRide();
                                    }
                                    else if (z3 == 5)
                                    {
                                        Student ss = new Student("", "", "");
                                        ss.viewTransactionHistory();
                                    }
                                    else if (z3 == 6)
                                    {
                                        Console.WriteLine("Logout Successful");
                                        studentMenu = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input");
                                    }
                                }
                            }
                            else if (z2 == 2)
                            {
                                Console.WriteLine("Please enter your card number:");
                                int cardNumber = Convert.ToInt32(Console.ReadLine());
                                List<Cards> cardsList = loadCards("cards.json");
                                Cards card = cardsList.Find(c => c.Number == cardNumber && c.Type == "FaculyMember");
                                if (card == null)
                                {
                                    Console.WriteLine("Card not found or not a faculty card.");
                                    continue;
                                }
                                if (card.Status == "blocked")
                                {
                                    Console.WriteLine("This card is blocked. You cannot access faculty member functions.");
                                    continue;
                                }

                                bool facultyMenu = true;
                                while (facultyMenu)
                                {
                                    Console.WriteLine("[1] Recharge Card ");
                                    Console.WriteLine("[2] Access car Parking");
                                    Console.WriteLine("[3] Generate Attendance report ");
                                    Console.WriteLine("[4] Logout ");
                                    Console.WriteLine(" Enter Your Choice: ");
                                    int z3 = Convert.ToInt32(Console.ReadLine());
                                    if (z3 == 1)
                                    {
                                        FacultyMember f = new FacultyMember("", "", "");
                                        chooseType(f);
                                    }
                                    else if (z3 == 2)
                                    {
                                        FacultyMember f = new FacultyMember("", "", "");
                                        f.accessCarParking();
                                    }
                                    else if (z3 == 3)
                                    {
                                        FacultyMember f = new FacultyMember("", "", "");
                                        f.generateAttendanceReport();
                                    }
                                    else if (z3 == 4)
                                    {
                                        Console.WriteLine("Logout Successful");
                                        facultyMenu = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input");
                                    }
                                }
                            }
                            else if (z2 == 3)
                            {
                                cardHolderMenu = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input");
                            }
                        }

                        break;
                    case 3:
                        Console.WriteLine("Exiting the program!");
                        x = false;
                        break;
                }

            }

        }





        public static void SaveAttendance(string fname, List<Student> studentsAttendance)
        {
            using (StreamWriter sw = new StreamWriter(fname))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sw, studentsAttendance);
            }
        }
        public static void SaveTransactions(string fname, List<Transaction> studentsTransactions)
        {
            using (StreamWriter sw = new StreamWriter(fname))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sw, studentsTransactions);
            }
        }


        public static void SaveStudent(string fname, List<Student> students)
        {
            using (StreamWriter sw = new StreamWriter(fname))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sw, students);
            }
        }
        public static void SaveFaculty(string fname, List<FacultyMember> userID2)
        {
            using (StreamWriter sw = new StreamWriter(fname))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sw, userID2);
            }
        }
        public static void SaveCards(string fname, List<Cards> userID3)
        {
            using (StreamWriter sw = new StreamWriter(fname))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sw, userID3);
            }
        }
        public static List<Student> LoadStudentsAttendance(string fname)
        {
            if (!File.Exists(fname))
                return new List<Student>();

            try
            {
                using (StreamReader sr = new StreamReader(fname))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var result = serializer.Deserialize(sr, typeof(List<Student>));
                    return result as List<Student> ?? new List<Student>();
                }
            }
            catch
            {

                return new List<Student>();
            }
        }
        public static List<Transaction> LoadTransaction(string fname)
        {
            if (!File.Exists(fname))
                return new List<Transaction>();

            try
            {
                using (StreamReader sr = new StreamReader(fname))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var result = serializer.Deserialize(sr, typeof(List<Transaction>));
                    return result as List<Transaction> ?? new List<Transaction>();
                }
            }
            catch
            {

                return new List<Transaction>();
            }
        }

        public static List<Student> LoadStudents(string fname)
        {
            using (StreamReader sr = new StreamReader(fname))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (List<Student>)serializer.Deserialize(sr, typeof(List<Student>));
            }
        }

        public static List<FacultyMember> loadFaculty(string fname2)
        {
            using (StreamReader sr = new StreamReader(fname2))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (List<FacultyMember>)serializer.Deserialize(sr, typeof(List<FacultyMember>));
            }
        }
        public static List<Cards> loadCards(string fname3)
        {
            using (StreamReader sr = new StreamReader(fname3))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (List<Cards>)serializer.Deserialize(sr, typeof(List<Cards>));
            }
        }
    }
}