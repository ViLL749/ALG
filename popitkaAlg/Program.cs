using System;
using System.Linq;

namespace Project
{
    class Program
    {
        static string[][] userAnswersArray; // Ступенчатый массив для хранения ответов пользователя

        static void Main(string[] args)
        {
            RunTest();
        }

        static void RunTest()
        {
            Console.WriteLine("Добро пожаловать в тестирование студентов!");

            string[] questions = new string[]
            {
                "Какое это число? 2 + 2 = ? \n1) 3 \n2) 4 \n3) 5 \n4) 6",
                "Сколько планет в Солнечной системе? \n1) 7 \n2) 8 \n3) 9 \n4) 10",
                "Какие цвета есть на флаге России? (Выберите все подходящие варианты)\n1) Красный\n2) Синий\n3) Зеленый\n4) Желтый",
                "Какой столицей является Москва?\n1) Париж\n2) Лондон\n3) Берлин\n4) Москва",
                "Какие из перечисленных чисел являются простыми? (Выберите все подходящие варианты)\n1) 7\n2) 12\n3) 17\n4) 21"
                // Добавить еще вопросы 
            };

            string[][] correctAnswers = new string[][]
            {
                new string[] { "2" },
                new string[] { "3" },
                new string[] { "1", "2" },
                new string[] { "4" },
                new string[] { "1", "3" }
            };

            userAnswersArray = new string[questions.Length][]; // Инициализируем ступенчатый массив

            Shuffle(questions, correctAnswers); // Перемешиваем вопросы и ответы

            int correctAnswersCount = TakeTest(questions, correctAnswers);

            char grade = CalculateGrade(correctAnswersCount, questions.Length);

            PrintResults(correctAnswersCount, questions.Length, grade);

            ShowMenu(correctAnswersCount, questions.Length, questions, correctAnswers);
        }

        static void Shuffle(string[] questions, string[][] answers)
        {
            Random random = new Random();

            // Перемешиваем вопросы и ответы одновременно, чтобы они оставались согласованными
            for (int i = 0; i < questions.Length; i++)
            {
                int randomIndex = random.Next(i, questions.Length);

                // Меняем местами вопросы
                string tempQuestion = questions[i];
                questions[i] = questions[randomIndex];
                questions[randomIndex] = tempQuestion;

                // Меняем местами ответы
                string[] tempAnswers = answers[i];
                answers[i] = answers[randomIndex];
                answers[randomIndex] = tempAnswers;
            }
        }

        static int TakeTest(string[] questions, string[][] correctAnswers)
        {
            int correctAnswersCount = 0;

            for (int i = 0; i < questions.Length; i++)
            {
                string question = questions[i];
                string[] correctAnswer = correctAnswers[i];

                Console.WriteLine($"\nВопрос {i + 1}: {question}");

                string[] userAnswers = GetUserAnswers();

                userAnswersArray[i] = userAnswers; // Добавляем ответы пользователя в ступенчатый массив

                Console.WriteLine($"Ваш ответ: {string.Join(", ", userAnswers)}");

                if (AreAnswersCorrect(userAnswers, correctAnswer))
                {
                    correctAnswersCount++;
                }
            }

            return correctAnswersCount;
        }

        static string[] GetUserAnswers()
        {
            bool isValidInput;
            string[] userAnswers;

            do
            {
                Console.Write("Введите номера правильных ответов через запятую (например, '2,3'): ");
                string userAnswersInput = Console.ReadLine();
                userAnswers = userAnswersInput.Split(',', ' ');

                isValidInput = userAnswers.All(answer => int.TryParse(answer, out int _) &&
                    int.Parse(answer) >= 1 && int.Parse(answer) <= 4);

                if (!isValidInput)
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите числа от 1 до 4 через запятую.");
                }
            } while (!isValidInput);

            return userAnswers;
        }

        static bool AreAnswersCorrect(string[] userAnswers, string[] correctAnswers)
        {
            return userAnswers.Length == correctAnswers.Length &&
                userAnswers.All(answer => correctAnswers.Contains(answer));
        }

        static char CalculateGrade(int correctAnswersCount, int totalQuestions)
        {
            double mark = (double)correctAnswersCount / totalQuestions * 100;
            char grade;

            if (mark >= 82)
                grade = '5';
            else if (mark >= 65)
                grade = '4';
            else if (mark >= 45)
                grade = '3';
            else
                grade = '2';

            return grade;
        }

        static void PrintResults(int correctAnswersCount, int totalQuestions, char grade)
        {
            Console.WriteLine($"\nВы ответили правильно на {correctAnswersCount}/{totalQuestions} вопросов, ваша оценка '{grade}'.");
            Console.WriteLine("Спасибо за участие в тестировании!");
        }

        static void ShowMenu(int correctAnswersCount, int totalQuestions, string[] questions, string[][] correctAnswers)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Выход");
            Console.WriteLine("2. Еще раз");
            Console.WriteLine("3. Результаты для преподавателя");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("До свидания!");
                    break;
                case "2":
                    RunTest(); // Restart the test
                    break;
                case "3":
                    ShowTeacherResults(correctAnswersCount, totalQuestions, questions, correctAnswers); // Pass user answers list
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите одно из предложенных действий.");
                    ShowMenu(correctAnswersCount, totalQuestions, questions, correctAnswers);
                    break;
            }
        }

        static void ShowTeacherResults(int correctAnswersCount, int totalQuestions, string[] questions, string[][] correctAnswers)
        {
            Console.WriteLine("\nДля доступа к результатам для преподавателя требуется ввести пароль.");

            // Пароль/кодовое слово для доступа к результатам преподавателя
            string password = "teacher123";

            Console.Write("Введите пароль: ");
            string input = Console.ReadLine();

            // Проверяем введенный пароль
            if (input == password)
            {
                Console.WriteLine("\nДоступ разрешен. Результаты для преподавателя:");

                // Выводим общее количество правильных ответов и общее количество вопросов
                Console.WriteLine($"Вы ответили правильно на {correctAnswersCount}/{totalQuestions} вопросов.");

                // Выводим дополнительную информацию, например, вопросы, в которых были допущены ошибки
                Console.WriteLine("\nРасширенные результаты:");

                for (int i = 0; i < questions.Length; i++)
                {
                    string question = questions[i];
                    string[] userAnswers = userAnswersArray[i]; // Получаем ответы пользователя
                    string[] correct = correctAnswers[i]; // Получаем правильные ответы

                    Console.WriteLine($"\nВопрос {i + 1}: {question}");

                    // Выводим ответы пользователя и правильные ответы
                    Console.WriteLine("Ваш ответ: " + string.Join(", ", userAnswers));
                    Console.WriteLine("Правильный ответ(ы): " + string.Join(", ", correct));
                }
            }
            else
            {
                Console.WriteLine("Неверный пароль. Доступ к результатам для преподавателя запрещен.");
            }
        }
    }
}
