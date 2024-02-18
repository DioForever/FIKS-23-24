using System;
using System.Runtime.InteropServices;
using System.Text;

class Program
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr GetStdHandle(uint nStdHandle);

    [DllImport("kernel32.dll")]
    public static extern bool ReadFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);

    [DllImport("kernel32.dll")]
    public static extern bool WriteConsole(IntPtr hConsoleOutput, string lpBuffer, uint nNumberOfCharsToWrite, out uint lpNumberOfCharsWritten, IntPtr lpReserved);

    [DllImport("kernel32.dll")]
    public static extern void Sleep(uint dwMilliseconds);

    static void FDelka(byte[] lpBuffer)
    {
        int var_4 = 0;
        for (int i = 0; i < lpBuffer.Length; i++)
        {
            if (lpBuffer[i] == 0 || lpBuffer[i] == 0x0D)
                break;
            var_4++;
        }
    }

    static void NacistHeslo(out byte[] lpBuffer)
    {
        IntPtr hFile = GetStdHandle(0xFFFFFFF6); // nStdHandle
        uint nNumberOfBytesRead;
        lpBuffer = new byte[256];
        ReadFile(hFile, lpBuffer, 255, out nNumberOfBytesRead, IntPtr.Zero);
    }

    static void Vypsat(byte[] lpBuffer)
    {
        IntPtr hConsoleOutput = GetStdHandle(0xFFFFFFF5); // nStdHandle
        FDelka(lpBuffer);
        string text = System.Text.Encoding.ASCII.GetString(lpBuffer);
        WriteConsole(hConsoleOutput, text, (uint)text.Length, out _, IntPtr.Zero);
    }

    static int Cist(byte[] lpBuffer)
    {
        int var_8 = 0;
        int var_4 = 0;
        for (int i = 0; i < lpBuffer.Length; i++)
        {
            if (lpBuffer[i] == 0)
                break;

            int value = lpBuffer[i];
            if (value == 0x0A)
                break;

            var_8 = var_8 * 10 + (value - '0');
        }

        return var_8;
    }

    static void SpatnaDelka()
    {
        Vypsat(Encoding.ASCII.GetBytes("\nSpatna delka hesla!\n"));
    }

    static void SpatneHeslo()
    {
        Vypsat(Encoding.ASCII.GetBytes("\nSpatne heslo!\n"));
    }

    static void DobreHeslo(byte[] lpBuffer)
    {
        Vypsat(Encoding.ASCII.GetBytes("\nGratulujeme! Tajemstvi je\n"));
        FDelka(lpBuffer);
        int var_C = Cist(lpBuffer);
        int var_4 = 0;

        while (var_4 < 21)
        {
            int value = lpBuffer[var_4];
            var_C = var_C * 10 + (value - '0');
            var_4++;
        }

        var_C = (var_C + 0x17) & 0xFF;

        int var_10 = 0;
        var_4 = 0;

        while (var_4 < 21)
        {
            int value = lpBuffer[var_4];
            var_10 = (var_10 << 8) | value;
            var_4++;
        }

        var_10 = (var_10 >> 9) & 0xFF;

        int var_4_2 = 0;
        while (var_4_2 < 21)
        {
            int value = lpBuffer[var_4_2];
            var_10 ^= value;
            var_4_2++;
        }

        Cist(Encoding.ASCII.GetBytes("96"));

        int result = var_10 ^ 0x96;
        Cist(BitConverter.GetBytes(result));
        Sleep(250);
    }

    static void start()
    {
        byte[] Buffer = new byte[256];
        Vypsat(Encoding.ASCII.GetBytes("V tomhle programu se schovava tajemstvi\n"
                                         + "Reverse engineering! Dobry zacatek je prozkoumat program hezky instrukci po instrukci.\n"
                                         + "Pomoci ti mohou nastroje jako x64dbg, Ida Free, Radare2 nebo Ghidra.\n"
                                         + "--------------------------------------------\n"
                                         + "Zadej heslo: "));

        NacistHeslo(out Buffer);
        if (Buffer[0] == 0 || Buffer[0] == 0x0D)
        {
            SpatnaDelka();
            return;
        }

        Sleep(250);

        FDelka(Buffer);
        if (Buffer[0] > 10)
        {
            Vypsat(Encoding.ASCII.GetBytes("\nCCC\n"));
            SpatnaDelka();
            return;
        }

        int result = (Buffer[8] << 3) & 0xFF;

        if (Buffer[result] != 0)
        {
            SpatneHeslo();
            return;
        }

        result = (Buffer[6] << 2) & 0xFF;
        result = ~result;
        int ecx = (Buffer[5] * 5) & 0xFF;
        int esi = (Buffer[7] * 7) & 0xFF;
        int eax = Buffer[9] & 0xFF;

        int edx;
        if (ecx != 0)
        {
            eax += Buffer[0];
            eax ^= ecx;
            eax ^= 0x6;
            eax *= 0;
            Buffer[eax] = (byte)esi;
        }

        result = (Buffer[5] << 2) & 0xFF;
        esi = (Buffer[7] * 7) & 0xFF;
        esi >>= 5;
        esi <<= 5;
        result += esi;

        esi = (Buffer[5] * 5) & 0xFF;
        esi >>= 5;
        esi <<= 5;
        result += esi;
        result = result & 0xFF;

        result = (Buffer[9] << 1) & 0xFF;
        int ebx = (Buffer[2] << 2) & 0xFF;
        ebx <<= 2;
        result = result - ebx;

        result = (Buffer[7] * 7) & 0xFF;
        result >>= 5;
        result <<= 5;
        int var_10 = result;

        result = (Buffer[9] << 1) & 0xFF;
        int var_C = (Buffer[4] << 2) & 0xFF;
        var_C <<= 2;
        result = result - var_C;

        result = (Buffer[3] << 2) & 0xFF;
        result <<= 2;
        result = result & 0xFF;

        int var_4 = 0;

        while (var_4 < 21)
        {
            Buffer[var_4] = 0;
            var_4++;
        }

        Buffer[5] = (byte)var_10;
        DobreHeslo(Buffer);
    }

    static void Main()
    {
        while (true)
            start();
    }
}
