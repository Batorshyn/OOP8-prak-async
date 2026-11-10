using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

var stopwatch = Stopwatch.StartNew();

string filePath = "input.txt";
string? directoryPath = Path.GetDirectoryName(filePath);
if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
{
    await File.WriteAllTextAsync(filePath, "Тихе місто спить у млі," +
                                           "\r\nЗірка світить у зеніті." +
                                           "\r\nКращої землі на світі" +
                                           "\r\nНіколи не знайти мені.");
}

Console.WriteLine("Асинхронне виконання:");

Task<int> fileTask = Task.Run(async () =>
{
    string fileContent = await File.ReadAllTextAsync(filePath);
    return fileContent.Length;
});

Task consoleTask = Task.Run(async () =>
{
    Console.WriteLine("Введіть текст з клавіатури (для завершення натисніть Enter на порожньому рядку):");
    string? input;
    while (!string.IsNullOrEmpty(input = await Console.In.ReadLineAsync()))
    {
        Console.WriteLine($"Ви ввели: {input}");
    }
});

await Task.WhenAll(fileTask, consoleTask);

int charCount = await fileTask;
Console.WriteLine($"Кількість символів у файлі: {charCount}");

stopwatch.Stop();
Console.WriteLine($"Час виконання: {stopwatch.ElapsedMilliseconds} мс");