using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using ZumasRevenge; // Если твой основной класс игры лежит в другом пространстве имен, поменяй это

namespace ZumasRevengePort
{
    public static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // 1. Регистрируем читатели XNB до любой загрузки контента
            ContentReaderRegistration.RegisterAll();

            // 2. Патчим все ресурсы перед запуском игры
            PatchXnbFiles();

            // 3. Чиним поиск библиотеки SexyFramework (используем явный System.AppDomain)
            System.AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                if (args.Name.Contains("SexyFramework"))
                {
                    return System.Reflection.Assembly.GetExecutingAssembly();
                }
                return null;
            };

            // 4. Запускаем игру с жестким перехватом вылетов
            try
            {
                using var game = new GameMain();
                game.Run();
            }
            catch (Exception ex)
            {
                // Если игра вылетит, эта штука выведет всю причину в консоль отладчика
                Debug.WriteLine("=============== КРИТИЧЕСКАЯ ОШИБКА ПРИ ЗАПУСКЕ ===============");
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("================================================================");
                throw; // Останавливаем Visual Studio, чтобы ты увидел ошибку
            }
        }

        // =====================================================================
        // 4. АВТОМАТИЧЕСКИЙ ПАТЧЕР РЕСУРСОВ
        // =====================================================================
        static void PatchXnbFiles()
        {
            // Программа сама находит папку Content прямо рядом с запущенным .exe
            string contentDir = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Content");

            if (!Directory.Exists(contentDir))
            {
                Debug.WriteLine("=============== ВНИМАНИЕ: Папка Content не найдена! ===============");
                return;
            }

            byte[] searchBytes = Encoding.UTF8.GetBytes("Zuma's Revenge!");
            byte[] replaceBytes = Encoding.UTF8.GetBytes("ZumasRevengeApp");

            // ИЩЕМ ВООБЩЕ ВСЕ ФАЙЛЫ (*.*) во всех вложенных папках
            string[] files = Directory.GetFiles(contentDir, "*.*", SearchOption.AllDirectories);
            int patchedCount = 0;

            foreach (var file in files)
            {
                // Пропускаем картинки и звуки, чтобы не тратить время и память
                if (file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".pam", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".pie", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".pax", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".dat", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".psd", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".pfx", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".font", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".xnb", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".bin", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".ico", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                byte[] fileBytes = File.ReadAllBytes(file);
                bool modified = false;

                // Ищем байты старого названия сборки
                for (int i = 0; i <= fileBytes.Length - searchBytes.Length; i++)
                {
                    bool match = true;
                    for (int j = 0; j < searchBytes.Length; j++)
                    {
                        if (fileBytes[i + j] != searchBytes[j])
                        {
                            match = false;
                            break;
                        }
                    }

                    // Если нашли - аккуратно заменяем на ZumasRevengeApp
                    if (match)
                    {
                        for (int j = 0; j < replaceBytes.Length; j++)
                        {
                            fileBytes[i + j] = replaceBytes[j];
                        }
                        modified = true;
                    }
                }

                // Перезаписываем файл только в том случае, если мы его изменили
                if (modified)
                {
                    File.WriteAllBytes(file, fileBytes);
                    patchedCount++;
                }
            }

            Debug.WriteLine($"=============== ПАТЧ ГОТОВ! Изменено файлов: {patchedCount} ===============");
        }
    }
}