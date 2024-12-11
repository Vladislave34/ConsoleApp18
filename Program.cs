using System;
using System.Threading;

class Animal
{
    public string Breed { get; set; }
    public string Type { get; set; }
    public double Weight { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }

    public Animal(string breed, string type, double weight, int age, string name)
    {
        Breed = breed;
        Type = type;
        Weight = weight;
        Age = age;
        Name = name;
    }

    public override string ToString()
    {
        return $"{Name}, {Type} ({Breed}), {Weight}kg, {Age} years old";
    }
}

class VeterinaryReception
{
    public event Action AllAnimalsExamined;

    private Random random = new Random();

    public void ProcessAnimals(Animal[] animals)
    {
        int totalTime = 0;

        foreach (var animal in animals)
        {
            int examinationTime = random.Next(5, 16); 
            Console.WriteLine($"Огляд тварини {animal.Name} триває {examinationTime} хвилин...");
            totalTime += examinationTime;

            
        }

        Console.WriteLine($"\nВсі тварини були оглянуті! Загальний час прийому: {totalTime} хвилин.");
        OnAllAnimalsExamined();
    }

    protected virtual void OnAllAnimalsExamined()
    {
        AllAnimalsExamined?.Invoke();
    }
}

class Program
{
    static Random random = new Random();

    static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Animal[] animals = new Animal[10];

        
        GenerateRandomAnimals(animals);

        Console.WriteLine("Список тварин для прийому у ветеринара:");
        foreach (var animal in animals)
        {
            Console.WriteLine(animal);
        }

        Console.WriteLine("\nПрийом тварин у ветеринара починається...\n");

        
        VeterinaryReception reception = new VeterinaryReception();

        
        reception.AllAnimalsExamined += () =>
        {
            Console.WriteLine("\n Всі тварини були оглянуті. Прийом завершено!");
        };

        
        reception.ProcessAnimals(animals);
    }

    static void GenerateRandomAnimals(Animal[] animals)
    {
        string[] breeds = { "Лабрадор", "Сіамська", "Пітбуль", "Хаскі", "Бульдог", "Шотландська", "Мейн-кун", "Коргі", "Такса", "Доберман" };
        string[] types = { "Собака", "Кіт", "Птах", "Хом'як", "Кролик" };
        string[] names = { "Барсик", "Шарик", "Мурка", "Рекс", "Сніжок", "Бім", "Руда", "Лаки", "Чарлі", "Белла" };

        for (int i = 0; i < animals.Length; i++)
        {
            string breed = breeds[random.Next(breeds.Length)];
            string type = types[random.Next(types.Length)];
            double weight = Math.Round(random.NextDouble() * 30 + 1, 1); 
            int age = random.Next(1, 16); 
            string name = names[random.Next(names.Length)];

            animals[i] = new Animal(breed, type, weight, age, name);
        }
    }
}
