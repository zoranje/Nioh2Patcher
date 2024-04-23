using Steamless.API.Model;
using Steamless.API.Services;
using Steamless.Unpacker.Variant31.x64;


namespace Nioh2Patcher
{
    public class Patch
    {
        private int offset;
        private byte[] replacement;
        private byte[] pattern;

        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public byte[] Replacement
        {
            get { return replacement; }
            set { replacement = value; }
        }

        public byte[] Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }

        public Patch(int offset, byte[] replacement, byte[] pattern)
        {
            this.offset = offset;
            this.replacement = replacement;
            this.pattern = pattern;
        }

    }
    public class Program
    {
        private const string EXE_FILE = "nioh2.exe";
        private const string EXE_FILE_BACKUP = "nioh2.exe.backup.exe";
        private const string EXE_FILE_UNPACKED = "nioh2.exe.unpacked.exe";

        private static void Main(string[] args)
        {
            String target = EXE_FILE_UNPACKED;

            if (!File.Exists(EXE_FILE))
            {
                Console.WriteLine($"{EXE_FILE} not found !");
                Exit();
                return;
            }

            if (!UnpackExe())
            {
                Console.WriteLine($"\nFailed to unpack {EXE_FILE} !");
                Exit();
                return;
            }

            byte[] buffer = File.ReadAllBytes(target);

            List<Patch> patches = new List<Patch>();
            patches.Add(new Patch(0xB, new byte[] { 0x80, 0x18, 0x55 }, new byte[] { 0x48, 0x8B, 0xCA, 0xF3, 0x48, 0xF, 0x2C, 0xC0, 0x48, 0x5, 0x0, 0x0, 0x90, 0xC }));
            patches.Add(new Patch(0xC, new byte[] { 0x20, 0x16 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x4, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0xC0, 0xE }));
            patches.Add(new Patch(0xC, new byte[] { 0xF0, 0xF }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x5, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0xA0, 0xA }));
            patches.Add(new Patch(0xC, new byte[] { 0xA8, 0x3 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x6, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x70, 0x2 }));
            patches.Add(new Patch(0xC, new byte[] { 0x48, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x7, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x30, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0x78, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x8, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x50, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0x78, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x9, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x50, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0x20, 0x4 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0xA, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x10, 0x2 }));
            patches.Add(new Patch(0xC, new byte[] { 0xC, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0xB, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x8, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0x10, 0x2 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0xC, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x60, 0x1 }));
            patches.Add(new Patch(0xC, new byte[] { 0x78, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0xD, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x50, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0x18, 0xF }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0xE, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x10, 0xA }));
            patches.Add(new Patch(0xC, new byte[] { 0xC0, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0xF, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x80, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0x68, 0x1 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x10, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0xF0, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0x68, 0x1 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x11, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0xF0, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0xC, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x12, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x8, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0x8, 0x1 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x13, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0xB0, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0x48, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x14, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x30, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0xD0, 0x5 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x15, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0xE0, 0x3 }));
            patches.Add(new Patch(0xC, new byte[] { 0xB8, 0x1A }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x16, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0xD0, 0x11 }));
            patches.Add(new Patch(0xC, new byte[] { 0x98, 0x1C }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x17, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x10, 0x13 }));
            patches.Add(new Patch(0xC, new byte[] { 0xF0, 0x2B }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x18, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0xF8, 0x15 }));
            patches.Add(new Patch(0xC, new byte[] { 0x0, 0xC }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x19, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x0, 0x8 }));
            patches.Add(new Patch(0xC, new byte[] { 0xB8, 0x5 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x1A, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0xD0, 0x3 }));
            patches.Add(new Patch(0xC, new byte[] { 0x18, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x1B, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x10, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0xF0, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x1C, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0xA0, 0x0 }));
            patches.Add(new Patch(0xC, new byte[] { 0x18, 0x0 }, new byte[] { 0xC7, 0x44, 0x24, 0x20, 0x1D, 0x0, 0x0, 0x0, 0x41, 0xB9, 0x0, 0x0, 0x10, 0x0 }));

            try
            {
                Console.WriteLine($"\nPatching {EXE_FILE} ...");

                if (File.Exists(EXE_FILE_UNPACKED))
                {
                    if (File.Exists(EXE_FILE_BACKUP))
                    {
                        Console.WriteLine($"\nBackup {EXE_FILE_BACKUP} already exists !");
                        File.Delete(EXE_FILE_UNPACKED);
                    }
                    else
                    {
                        File.Move(EXE_FILE, EXE_FILE_BACKUP);
                        Console.WriteLine($"\nBackup {EXE_FILE_BACKUP} created !");
                        File.Move(EXE_FILE_UNPACKED, EXE_FILE);
                    }
                }

                int patchCtr = 0;
                for (int i =0; i<patches.Count; i++)
                {
                    int index = findPattern(buffer, patches[i].Pattern);
                    if (index == -1)
                    {
                        Console.WriteLine($"({i + 1}/{patches.Count}) Pattern not found: {BitConverter.ToString(patches[i].Pattern)}");
                    }
                    else
                    {
                        Array.Copy(patches[i].Replacement, 0, buffer, index + patches[i].Offset, patches[i].Replacement.Length);
                        Console.WriteLine($"({i + 1}/{patches.Count}) Patch applied: {BitConverter.ToString(patches[i].Pattern)} -> {BitConverter.ToString(patches[i].Replacement)} at 0x{index + patches[i].Offset:X}");
                        patchCtr++;
                    }
                }
                if (patchCtr == 0)
                {
                    Console.WriteLine($"\nFailed to patch {EXE_FILE} !");
                } else
                {
                    File.WriteAllBytes(EXE_FILE, buffer);
                    Console.WriteLine($"({patchCtr}/{patches.Count}) Patches successfully applied !");
                    Console.WriteLine("\nDone !");
                }

                Exit();
                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
                Exit();
                return;
            }
        }

        private static int findPattern(byte[] source, byte[] pattern, int startIndex = 0)
        {
            for (int i = startIndex; i <= source.Length - pattern.Length; i++)
            {
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                {
                    return i;
                }
            }
            return -1;
        }

        private static bool UnpackExe()
        {
            LoggingService loggingService = new LoggingService();
            loggingService.AddLogMessage += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.Message);
            };

            SteamlessPlugin plugin = new Main();
            plugin.Initialize(loggingService);

            var result = plugin.CanProcessFile(EXE_FILE);

            if (!result)
            {
                return false;
            }

            result = plugin.ProcessFile(EXE_FILE, new SteamlessOptions
            {
                VerboseOutput = false,
                KeepBindSection = true,
                DontRealignSections = true,
                ZeroDosStubData = true
            });

            if (!result)
            {
                Console.WriteLine($"-> Processing {EXE_FILE} failed (file might not be encrypted)!");

                return false;
            }

            return true;
        }
        private static void Exit()
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

    }
}
